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

    [SerializeField] private float superjumpParticlesDistance = 1f;
    [SerializeField] private float superjumpParticlesMinSpeed = 8;

    [Header("References")]
    [SerializeField] private BouncingPart bouncingPart;
    [SerializeField] private GameObject superjumpParticlesObj;
    [SerializeField] private ParticleSystem superjumpActPart1;

    [Header("Sprites")]
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;

    //private
    private Vector3 initialPosition;
    private SpriteRenderer mySpriteRenderer;

    private TricksDetector tricksDetector;
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 buttHitStartingPos;

    private bool isPerformingButtHit = false;
    private bool isSuperjumpActive = false;
    private ParticleSystem superjumpParticles;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        tricksDetector = new TricksDetector(this);
    }


    private void Start() {
        GameController.Instance.RegisterPlayer(this);
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        initialPosition = transform.position;
        superjumpParticles = superjumpParticlesObj.GetComponent<ParticleSystem>();
        superjumpParticles.Stop();
    }

    private void Update() {
        HandleRotation();
        HandleSuperjumpParticles();
    }
    private void HandleSuperjumpParticles() {
        if (isSuperjumpActive) {

            superjumpParticlesObj.transform.position = transform.position + (Vector3)rb.velocity.normalized * superjumpParticlesDistance;
            superjumpParticlesObj.transform.up = rb.velocity.normalized;

            if(rb.velocity.y < superjumpParticlesMinSpeed) superjumpParticles.Stop();

            //PARTICLES ALWAYS ACTIVE
            //if (rb.velocity.magnitude < superjumpParticlesMinSpeed)
            //    superjumpParticles.Stop();
            //else if (superjumpParticles.isStopped)
            //    superjumpParticles.Play();
        }
    }
    private void HandleRotation() {
        if (InputManager.Instance.IsDoubleHold() || isPerformingButtHit) {
            animator.SetBool("GoUp", false);
            DoButtHit();
        }
        else {

            if (rb.velocity.y > 0)
                animator.SetBool("GoUp", true);
            else
                animator.SetBool("GoUp", false);

            switch (InputManager.Instance.GetRotationDirection()) {
                case 1:
                    rb.angularVelocity = -rotationSpeed;
                    break;
                case -1:
                    rb.angularVelocity = rotationSpeed;
                    break;
                case 0:
                default:
                    rb.angularVelocity = 0;
                    break;
            }
        }

        tricksDetector.registerRotation(transform.rotation.eulerAngles.z);
    }

    private float lastButtHitEndTime;
    private void DoButtHit() {
        if (Time.time - lastButtHitEndTime < buttHitTimer) return;

        //done only on first call
        if (!isPerformingButtHit) {
            mySpriteRenderer.sprite = downSprite;
            buttHitStartingPos = transform.position;
            rb.velocity = Vector2.zero;
            isPerformingButtHit = true;
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
            rb.velocity = new Vector2(0, -buttHitSpeed);
        }
    }

    public void Bounce() {
        Vector3 tipPos = GameObject.Find("BouncingTip").gameObject.GetComponentInChildren<CapsuleCollider2D>().transform.position;
        if (tricksDetector.TrickDetected()) {
            ActivateSuperjump();
        }
        else {
            rb.velocity = transform.up * baseBounceSpeed;
            if (bouncingPart.transform.position.y < transform.position.y) {
                DeactivateSuperjump();
            }
        }
        if (isPerformingButtHit) {
            lastButtHitEndTime = Time.time;
            isPerformingButtHit = false;
        }
        tricksDetector.Reset();
    }

    public void ResetInitialPosition() {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        tricksDetector.Reset();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        //if the pekan touches the ground
        if (other.otherCollider.gameObject.tag == "Player") {
            //preventing collisiions between parts of player 
            if (other.gameObject.tag != "Player" && other.gameObject.tag != "BouncingTip") {
                if (other.contacts[0].point.y < transform.position.y) {
                    DeactivateSuperjump();
                }
            }
        }
    }
    public bool IsPerformingButtHit() {
        return isPerformingButtHit;
    }
    //================================================ activations/deactivations

    private void ActivateSuperjump() {
        rb.velocity = transform.up * boostBounceSpeed;
        animator.SetBool("SuperJump", true);
        isSuperjumpActive = true;
        superjumpParticles.Play();
    }
    private void DeactivateSuperjump() {
        animator.SetBool("SuperJump", false);
        isSuperjumpActive = false;
        superjumpParticles.Stop();
    }

    public void ActivateSuperjumpDetectedParticles() {
        superjumpActPart1.Play();
    }

}



