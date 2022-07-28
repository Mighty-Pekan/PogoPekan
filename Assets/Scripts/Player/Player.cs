using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float baseBounceSpeed;
    [SerializeField] float boostBounceSpeed;
    [SerializeField] float buttHitSpeed = 10f;

    bool superJumpActivatedThisFrame;

    private Vector3 initialPosition;

    TricksDetector tricksDetector;
    Rigidbody2D myRigidbody;
    Animator animator;

    private bool performingButtHit = false;
    private Vector2 buttHitStartingPos;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tricksDetector = new TricksDetector();
    }


    private void Start()
    {
        GameController.RegisterPlayer(this);

        initialPosition = transform.position;
    }

    private void Update() {
        if (InputManager.IsDoubleHold()||performingButtHit) {
            DoButtHit();
        }
        else{
            transform.Rotate(InputManager.GetRotationDirection() * rotationSpeed * Time.deltaTime);
        }

        tricksDetector.registerRotation(transform.rotation.eulerAngles.z);
    }
    bool buttHitCorStarted = false;
    private void DoButtHit() {

        //done only on first call
        if (!performingButtHit) {
            buttHitStartingPos = transform.position;
            myRigidbody.velocity = Vector2.zero;
            performingButtHit = true;
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
    public bool isDoingButtHit() { return performingButtHit; }

    private void LateUpdate() {
        superJumpActivatedThisFrame = false;
    }

    public void Bounce() {
        if (tricksDetector.TrickDetected()) {
            superJumpActivatedThisFrame = true;
            myRigidbody.velocity = transform.up * boostBounceSpeed;
            StartCoroutine(ShrinkCamera());
        }
        else {
            myRigidbody.velocity = transform.up * baseBounceSpeed;
        }
        tricksDetector.Reset();
    }

    private IEnumerator ShrinkCamera() {
        yield return new WaitForEndOfFrame();
        animator.SetBool("SuperJump", true);
    }

    public void ResetInitialPosition() {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        myRigidbody.velocity = Vector3.zero;
        tricksDetector.Reset();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!superJumpActivatedThisFrame && !other.gameObject.CompareTag("Player") && !other.gameObject.CompareTag("BouncingTip")) {
            if (other.contacts[0].point.y < transform.position.y) {
                animator.SetBool("SuperJump", false);
                //Debug.Log("DEACTIVATING SUPERJUMP");
            }
            else {
                //Debug.Log("NO DEACTIVATION: othertag = " + other.gameObject.tag + ", collision Y: " + other.contacts[0].point.y + ", my y:" + transform.position.y);

            }
        }
        //else Debug.Log("NO DEACTIVATION: superjumpThisFrame = " + superJumpActivatedThisFrame + ", othertag = " + other.gameObject.tag);
    }

    private void OnCollisionStay2D(Collision2D collision) {
        performingButtHit = false;
        buttHitCorStarted = false;
    }


}
