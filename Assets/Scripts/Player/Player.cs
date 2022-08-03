using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    //public
    public bool PerformingButtHit { get; private set; } = false;

    //serialized fields
    [Header("Properties")]
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float baseBounceSpeed;
    [SerializeField] private float boostBounceSpeed;
    [SerializeField] private float buttHitSpeed = 10f;

    [Header("References")]
    [SerializeField] private BouncingPart bouncingPart;

    [Header("Sprites")]
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;

    //private

    private bool superJumpActivatedThisFrame;
    private Vector3 initialPosition;
    private SpriteRenderer mySpriteRenderer;

    private TricksDetector tricksDetector;
    private Rigidbody2D myRigidbody;
    private Animator animator;

    private Vector2 buttHitStartingPos;

    

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tricksDetector = new TricksDetector();
    }


    private void Start()
    {
        GameController.RegisterPlayer(this);
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }

    private void Update() {

        if (InputManager.IsDoubleHold()||PerformingButtHit) {
            DoButtHit();
            mySpriteRenderer.sprite = downSprite;
        }
        else{

            if (myRigidbody.velocity.y > 0)
                mySpriteRenderer.sprite = upSprite;
            else
                mySpriteRenderer.sprite = downSprite;

            transform.Rotate(InputManager.GetRotationDirection() * rotationSpeed * Time.deltaTime);
        }

        tricksDetector.registerRotation(transform.rotation.eulerAngles.z);
    }

    bool buttHitCorStarted = false;
    private void DoButtHit() {

        //done only on first call
        if (!PerformingButtHit) {
            buttHitStartingPos = transform.position;
            myRigidbody.velocity = Vector2.zero;
            PerformingButtHit = true;
            tricksDetector.Reset();
        }

        float myRotation = transform.rotation.eulerAngles.z;
        
        // DONT USE MATHF.EPSILON !!! its too slow.
        // checks if paddle is in vertical position
        if (myRotation > 0 + 0.1f && myRotation < 360 - 0.1f) {

            if (myRotation > 180) transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            else transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            transform.position = buttHitStartingPos;

        }
        else {
            if (!buttHitCorStarted) {
                buttHitCorStarted = true;
            }
            else myRigidbody.velocity = new Vector2(0, -buttHitSpeed);
        }
    }

    private void LateUpdate() {
        superJumpActivatedThisFrame = false;
    }

    public void Bounce() {
        if (tricksDetector.TrickDetected()) {
            Debug.Log("trick detected");
            superJumpActivatedThisFrame = true;
            myRigidbody.velocity = transform.up * boostBounceSpeed;
            StartCoroutine(ExpandCamera());
        }
        else {
            myRigidbody.velocity = transform.up * baseBounceSpeed;
            if(bouncingPart.transform.position.y < transform.position.y)
                animator.SetBool("SuperJump", false);
            Debug.Log("trick not detected");
        }
        tricksDetector.Reset();
    }

    private IEnumerator ExpandCamera() {
        yield return new WaitForEndOfFrame();
        animator.SetBool("SuperJump", true);
    }

    public void ResetInitialPosition() {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        myRigidbody.velocity = Vector3.zero;
        tricksDetector.Reset();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!superJumpActivatedThisFrame && !other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("BouncingTip"))
        {
            if (other.contacts[0].point.y < transform.position.y)
            {
                animator.SetBool("SuperJump", false);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision) {
        PerformingButtHit = false;
        buttHitCorStarted = false;
    }


}
