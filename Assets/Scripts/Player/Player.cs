using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //serialized fields
    [Header("Properties")]
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float baseBounceSpeed;
    [SerializeField] private float boostBounceSpeed;
    [SerializeField] private float buttHitSpeed = 10f;
    [SerializeField] private float buttHitTimer = 2f;

    [Header("References")]
    [SerializeField] private BouncingPart bouncingPart;

    [Header("Sprites")]
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;

    //private
    private bool performingButtHit = false;
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
        GameController.Instance.RegisterPlayer(this);
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
    }

    private void Update() {
        if (InputManager.Instance.IsDoubleHold()||performingButtHit) {
            animator.SetBool("GoUp", false);
            DoButtHit();  
        }
        else{

            if (myRigidbody.velocity.y > 0)
                animator.SetBool("GoUp", true);
            else
                animator.SetBool("GoUp", false);


            transform.Rotate(InputManager.Instance.GetRotationDirection() * rotationSpeed * Time.deltaTime * Vector3.back);
        }

        tricksDetector.registerRotation(transform.rotation.eulerAngles.z);
    }

    //private void FixedUpdate() {
    //    myRigidbody.AddTorque(rotationSpeed, ForceMode2D.Force);
    //}

    float lastButtHitEndTime;

    private void DoButtHit() {
        if (Time.time - lastButtHitEndTime < buttHitTimer) return;

        //done only on first call
        if (!performingButtHit) {
            mySpriteRenderer.sprite = downSprite;
            buttHitStartingPos = transform.position;
            myRigidbody.velocity = Vector2.zero;
            performingButtHit = true;
            tricksDetector.Reset();
        }

        float myRotation = transform.rotation.eulerAngles.z;
        
        if (myRotation > 2 && myRotation < 358) {

            if (myRotation > 180) transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            else transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
            transform.position = buttHitStartingPos;

        }
        else {
            transform.up = Vector2.up;
            myRigidbody.velocity = new Vector2(0, -buttHitSpeed);
        }
    }

    private void LateUpdate() {
        superJumpActivatedThisFrame = false;
    }

    public void Bounce()
    {
        Vector3 tipPos = GameObject.Find("BouncingTip").gameObject.GetComponentInChildren<CapsuleCollider2D>().transform.position;
        if (tricksDetector.TrickDetected())
        {
            superJumpActivatedThisFrame = true;
            myRigidbody.velocity = transform.up * boostBounceSpeed;
            StartCoroutine(ExpandCamera());
        }
        else {
            myRigidbody.velocity = transform.up * baseBounceSpeed;
            if(bouncingPart.transform.position.y < transform.position.y)
                animator.SetBool("SuperJump", false);
        }
        if (performingButtHit)
        {
            lastButtHitEndTime = Time.time;
            performingButtHit = false;
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

    //private void OnCollisionStay2D(Collision2D collision) {
    //    performingButtHit = false;
    //}

    public bool IsPerformingButtHit()
    {
        return performingButtHit;
    }

}
