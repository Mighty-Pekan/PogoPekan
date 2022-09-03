using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    //serialized fields
    [Header("Properties")]
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float baseBounceSpeed;
    [SerializeField] private float boostBounceSpeed;
    [SerializeField] private float buttHitRotationSpeed = 600f;
    [SerializeField] private float buttHitSpeed = 10f;
    [SerializeField] private float buttHitTimer = 2f;

    [SerializeField] private float superjumpParticlesDistance = 1f;
    [SerializeField] private float superjumpParticlesMinSpeed = 8;

    [Header("References")]
    [SerializeField] private BouncingPart bouncingPart;
    [SerializeField] private GameObject superjumpParticlesObj;
    [SerializeField] private GameObject superjumpTray;
    [SerializeField] private GameObject superjumpActLight;
    [SerializeField] private GameObject superjumpActBackLight;
    [SerializeField] private ParticleSystem buttHitParticles;
    [SerializeField] private Animator blinkAnimator;

    [Header("Sprites")]
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;

    [Header("Settings")]
    [SerializeField] private bool superjumpTrailEnabled;
    [SerializeField] private bool superjumpSpeedEnabled;
    [SerializeField] private bool superjumpActLightEnabled;
    [SerializeField] private bool kevinBlinkAnimationEnabled;

    [Header("Sounds")]
    [SerializeField] AudioClip blinkAudioClip;
    [SerializeField] AudioClip superjumpAudioClip;
    [SerializeField] AudioClip culataSound;

    //private
    private Vector3 initialPosition;
    private SpriteRenderer mySpriteRenderer;

    private TricksDetector tricksDetector;
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2 buttHitStartingPos;

    private bool isPerformingButtHitRotation = false;
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
        superjumpTray.SetActive(false);
        superjumpParticles.Stop();
        superjumpActLight.SetActive(false);
        superjumpActBackLight.SetActive(false);
        //AudioManager.Instance.initSuperjumpAudioClip(superjumpAudioClip);
    }

    public bool isAlive { get; set; } = true;
    private void Update()
    {
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
        if (isAlive) {
            if (InputManager.Instance.IsDoubleHold()) {
                animator.SetBool("GoUp", false);
                DoButtHit();
            }
            else {

                if (isPerformingButtHit) {
                    EndButtHit();
                }

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
    }

    private float lastButtHitEndTime;
    private void DoButtHit() {
        if (Time.time - lastButtHitEndTime < buttHitTimer) return;
        Debug.Log("DoingButtHit");

        //done only on first call
        if (!isPerformingButtHit && !isPerformingButtHitRotation) {
            isPerformingButtHitRotation = true;
            buttHitStartingPos = transform.position;
            rb.velocity = Vector2.zero;
            DeactivateSuperjump();
            tricksDetector.Reset();
        }

        float myRotation = transform.rotation.eulerAngles.z;

        if (myRotation > 2 && myRotation < 358) {
            if (myRotation > 180) transform.Rotate(Vector3.forward * buttHitRotationSpeed * Time.deltaTime);
            else transform.Rotate(Vector3.back * buttHitRotationSpeed * Time.deltaTime);
            transform.position = buttHitStartingPos;
        }
        else {
            if (!isPerformingButtHit) {
                isPerformingButtHit = true;
                isPerformingButtHitRotation = false;
                AudioManager.Instance.StopInterruptableSound();
                AudioManager.Instance.PlayInterruptableSound(culataSound);
            }
            transform.up = Vector2.up;
            rb.velocity = new Vector2(0, -buttHitSpeed);
            rb.angularVelocity = 0;
            if (!buttHitParticles.isPlaying) buttHitParticles.Play();
        }
    }

    private void EndButtHit() {
        rb.angularVelocity = 0;
        lastButtHitEndTime = Time.time;
        isPerformingButtHit = false;
        isPerformingButtHitRotation = false;
        buttHitParticles.Stop();
    }

    public void Bounce(Collision2D other) {
        if (isAlive) {
            // handling mushrooms bounciness directly
            float mushroomBounceForce;
            if (other.gameObject.tag == "Mushroom") {
                mushroomBounceForce = other.gameObject.GetComponent<Mushroom>().GetBounciness();
            }
            else {
                mushroomBounceForce = 0f;
            }

            //============================================================================

            if (tricksDetector.TrickDetected()) {
                ActivateSuperjump(mushroomBounceForce);
            }
            else {
                rb.velocity = transform.up * (baseBounceSpeed + mushroomBounceForce);
                if (bouncingPart.transform.position.y < transform.position.y) {
                    DeactivateSuperjump();
                }
            }

            if (isPerformingButtHit) {
                EndButtHit();
            }
            tricksDetector.Reset();
        }
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

    private void OnCollisionStay2D(Collision2D other) {
        //prevents superjump charge when touching ground with pekan
        if (other.otherCollider.gameObject.tag == "Player") {
            //preventing collisiions between parts of player 
            if (other.gameObject.tag != "Player" && other.gameObject.tag != "BouncingTip") {
                tricksDetector.Reset();
            }
        }
    }

    public bool IsPerformingButtHit() {
        return isPerformingButtHit || isPerformingButtHitRotation;
    }
    //================================================ activations/deactivations

    private void ActivateSuperjump(float speedupFactor = 1f) {
        rb.velocity = transform.up * (boostBounceSpeed + speedupFactor);
        animator.SetBool("SuperJump", true);
        isSuperjumpActive = true;
        if (superjumpTrailEnabled) superjumpTray.SetActive(true);
        if (superjumpSpeedEnabled) superjumpParticles.Play();
        AudioManager.Instance.PlayInterruptableSound(superjumpAudioClip);
    }
    private void DeactivateSuperjump() {
        animator.SetBool("SuperJump", false);
        AudioManager.Instance.StopInterruptableSound();
        isSuperjumpActive = false;

        if(superjumpTrailEnabled)superjumpTray.SetActive(false);
        if(superjumpSpeedEnabled)superjumpParticles.Stop();
    }

    public void ActivateSuperjumpDetectedParticles() {
        if (superjumpActLightEnabled) StartCoroutine(blink());
        if(kevinBlinkAnimationEnabled)
        {
            AudioManager.Instance.PlaySound(blinkAudioClip);
            blinkAnimator.SetTrigger("Blink");
        }
    }
    private IEnumerator blink() {
        superjumpActLight.SetActive(true);
        superjumpActBackLight.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        superjumpActLight.SetActive(false);
        superjumpActBackLight.SetActive(false);
    }

}



