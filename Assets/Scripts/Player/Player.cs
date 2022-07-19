using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float baseBounceSpeed;
    [SerializeField] float boostBounceSpeed;

    float lastRotation;
    bool superJumpActivatedThisFrame;

    private Vector3 initialPosition;

    TricksDetector tricksDetector;
    Rigidbody2D myRigidbody;
    Animator animator;

    private void Awake() {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tricksDetector = new TricksDetector();
    }

    private void Start() {
        initialPosition = transform.position;
    }

    private void Update() {
        transform.Rotate(InputManager.RotationDirection() * rotationSpeed * Time.deltaTime);
        tricksDetector.registerRotation(transform.rotation.eulerAngles.z);
    }

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
                Debug.Log("DEACTIVATING SUPERJUMP");
            }
            else {
                Debug.Log("NO DEACTIVATION: othertag = " + other.gameObject.tag + ", collision Y: " + other.contacts[0].point.y + ", my y:" + transform.position.y);

            }
        }
        else Debug.Log("NO DEACTIVATION: superjumpThisFrame = " + superJumpActivatedThisFrame + ", othertag = " + other.gameObject.tag);
    }


}
