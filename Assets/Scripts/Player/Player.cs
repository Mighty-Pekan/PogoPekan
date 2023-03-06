using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    //serialized fields
    [Header("Properties")]
    [SerializeField] private float rotationSpeed = 2f;

    [SerializeField] private float buttHitRotationSpeed = 600f;
    [SerializeField] private float buttHitSpeed = 10f;
    [SerializeField] private float buttHitTimer = 2f;

    [SerializeField] private float superjumpParticlesDistance = 1f;
    [SerializeField] private float superjumpParticlesMinSpeed = 8;

    [Header("References")]
    [SerializeField] private GameObject superjumpParticlesObj;
    [SerializeField] private GameObject superjumpTray;
    [SerializeField] private GameObject superjumpActLight;
    [SerializeField] private GameObject superjumpActBackLight;
    [SerializeField] private ParticleSystem buttHitParticles;
    [SerializeField] private Animator blinkAnimator;

    [Header("Sprites")]
    [SerializeField] private Sprite gameoverSprite;
    [SerializeField] private Sprite upSprite;
    [SerializeField] private Sprite downSprite;

    [Header("Settings")]
    [SerializeField] private bool superjumpTrailEnabled;
    [SerializeField] private bool superjumpSpeedEnabled;
    [SerializeField] private bool superjumpActLightEnabled;
    [SerializeField] private bool kevinBlinkAnimationEnabled;

    [Header("AudioSources")]
    [SerializeField] GenericAudioSource culataAudioSource;
    [SerializeField] GenericAudioSource blinkAudioSource;
    [SerializeField] GenericAudioSource hammerHitAudioSource;

    [SerializeField] public UnityEvent onSuperJump;

    private float bounceForce;

    private Vector3 initialPosition;

    private TricksDetector tricksDetector;
    private Rigidbody2D rb;
    private Animator animator;

    private Vector2? buttHitStartingPos = null;

    private bool isPerformingButtHitRotation = false;
    private bool isPerformingButtHitDive = false;
    private bool canPerformSuperJump = false;
    private ParticleSystem superjumpParticles;

    private bool isAlive;
    public bool IsAlive
    {
        get
        {
            return isAlive;
        }
        set
        {
            isAlive = value;
            if (value == false)
            {
                StopAllSounds();
                SuperJumpFailed();
                EndButtHit();
                animator.SetTrigger("Death");
                GetComponent<SpriteRenderer>().sprite = gameoverSprite;

            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        tricksDetector = new TricksDetector(this);
    }

    private void Start()
    {
        IsAlive = true;

        GameController.Instance.RegisterPlayer(this);
        initialPosition = transform.position;

        superjumpParticles = superjumpParticlesObj.GetComponent<ParticleSystem>();
        superjumpTray.SetActive(false);
        superjumpParticles.Stop();
        superjumpActLight.SetActive(false);
        superjumpActBackLight.SetActive(false);
    }


    private void Update()
    {
        if (!isAlive) return;

        if (tricksDetector.TrickDetected())
        {
            PerformSuperJump();
        }

        HandleRotation();
        HandleSuperjumpParticles();
    }
    private void HandleSuperjumpParticles()
    {
        if (canPerformSuperJump)
        {
            superjumpParticlesObj.transform.position = transform.position + (Vector3)rb.velocity.normalized * superjumpParticlesDistance;
            superjumpParticlesObj.transform.up = rb.velocity.normalized;

            if (rb.velocity.y < superjumpParticlesMinSpeed) superjumpParticles.Stop();

            //PARTICLES ALWAYS ACTIVE
            //if (rb.velocity.magnitude < superjumpParticlesMinSpeed)
            //    superjumpParticles.Stop();
            //else if (superjumpParticles.isStopped)
            //    superjumpParticles.Play();
        }
    }
    private void HandleRotation()
    {
        if (InputManager.Instance.IsDoubleHold())
        {
            animator.SetBool("GoUp", false);
            DoButtHit();
        }
        else
        {
            if (isPerformingButtHitDive || isPerformingButtHitRotation)
            {
                EndButtHit();
            }

            if (rb.velocity.y > 0)
                animator.SetBool("GoUp", true);
            else
                animator.SetBool("GoUp", false);

            switch (InputManager.Instance.GetRotationDirection())
            {
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
    private bool touchedGroundAfterButtHit = true;
    private void DoButtHit()
    {
        if (Time.time - lastButtHitEndTime < buttHitTimer || !touchedGroundAfterButtHit) return;

        // ============================rotation
        // done only first time
        if (!isPerformingButtHitDive && !isPerformingButtHitRotation)
        {
            isPerformingButtHitRotation = true;
            buttHitStartingPos = transform.position;
            rb.velocity = Vector2.zero;
            SuperJumpFailed();
            tricksDetector.Reset();
        }

        float myRotation = transform.rotation.eulerAngles.z;
        if (myRotation > 2 && myRotation < 358)
        {

            float rotationSpeed = buttHitRotationSpeed;
            if (myRotation < 20 || myRotation > 340) rotationSpeed = buttHitRotationSpeed / 4;

            //Debug.Log("rotation: " + myRotation);
            if (myRotation > 180)
                rb.angularVelocity = rotationSpeed;
            //transform.Rotate(Vector3.forward * buttHitRotationSpeed * Time.deltaTime);

            else
                rb.angularVelocity = -rotationSpeed;
            //transform.Rotate(Vector3.back * buttHitRotationSpeed * Time.deltaTime);

            if (buttHitStartingPos != null)
                transform.position = (Vector2)buttHitStartingPos;
        }
        // ==========================dive
        else
        {
            // done only first time
            if (!isPerformingButtHitDive)
            {
                isPerformingButtHitDive = true;
                isPerformingButtHitRotation = false;
                touchedGroundAfterButtHit = false;
                StopAllSounds();
                culataAudioSource.Play();
                buttHitStartingPos = null;
                rb.angularVelocity = 0;
            }
            transform.up = Vector2.up;
            rb.angularVelocity = 0;
            rb.velocity = new Vector2(0, -buttHitSpeed);
            if (!buttHitParticles.isPlaying) buttHitParticles.Play();
        }
    }

    public void ContinueDive()
    {
        transform.up = Vector2.up;
        rb.angularVelocity = 0;
        rb.velocity = new Vector2(0, -buttHitSpeed);
    }

    private void StopAllSounds()
    {
        //superjumpAudioSource.Stop();
        culataAudioSource.Stop();
    }

    private void EndButtHit()
    {
        rb.angularVelocity = 0;
        lastButtHitEndTime = Time.time;
        isPerformingButtHitDive = false;
        isPerformingButtHitRotation = false;
        buttHitStartingPos = null;
        buttHitParticles.Stop();
        culataAudioSource.Stop();
    }

    public void ApplyBounceForce(float force)
    {
        bounceForce = force;
    }

    public void Bounce()
    {
        if (!IsAlive) return;

        if (IsPerformingButtHitDive() || IsPerformingButtHitRotation())
        {
            if (isPerformingButtHitDive)
            {
                hammerHitAudioSource.Play();
            }
            EndButtHit();
        }

        rb.velocity = transform.up * bounceForce;

      /*  if (!(other.gameObject.tag == "BreakablePlatform" && IsPerformingButtHitDive()))
         {
             touchedGroundAfterButtHit = true;
         } */

        tricksDetector.Reset();
    }

    public void ResetInitialPosition()
    {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        rb.velocity = Vector3.zero;
        tricksDetector.Reset();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.contacts[0].point.y < transform.position.y)
        {
            SuperJumpFailed();
            Debug.Log("Super Jump Failed");
        }
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        tricksDetector.Reset();
    }

    public bool IsPerformingButtHitDive()
    {
        return isPerformingButtHitDive;
    }
    public bool IsPerformingButtHitRotation()
    {
        return isPerformingButtHitRotation;
    }

    private void PerformSuperJump()
    {
        animator.SetBool("SuperJump", true);
        canPerformSuperJump = true;
        if (superjumpTrailEnabled) superjumpTray.SetActive(true);
        if (superjumpSpeedEnabled) superjumpParticles.Play();
    }

    private void SuperJumpFailed()
    {
        animator.SetBool("SuperJump", false);
        StopAllSounds();
        canPerformSuperJump = false;
        tricksDetector.Reset();

        if (superjumpTrailEnabled) superjumpTray.SetActive(false);
        if (superjumpSpeedEnabled) superjumpParticles.Stop();
    }

    public void ActivateSuperjumpDetectedParticles()
    {
        if (superjumpActLightEnabled) StartCoroutine(blink());
        if (kevinBlinkAnimationEnabled)
        {
            blinkAudioSource.Play();
            blinkAnimator.SetTrigger("Blink");
        }
    }
    private IEnumerator blink()
    {
        superjumpActLight.SetActive(true);
        superjumpActBackLight.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        superjumpActLight.SetActive(false);
        superjumpActBackLight.SetActive(false);
    }

    public bool HasSuperJump()
    {
        return canPerformSuperJump;
    }
}