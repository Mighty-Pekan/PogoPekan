using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float baseBounceSpeed;
    [SerializeField] float boostBounceSpeed;

    float lastRotation;

    private Vector3 initialPosition;

    TricksDetector tricksDetector;
    Rigidbody2D myRigidbody;
    Animator animator;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
        tricksDetector = new TricksDetector();
    }

    private void Start()
    {
        initialPosition = transform.position;
    }

    private void Update()
    {
        transform.Rotate(InputManager.RotationDirection() * rotationSpeed * Time.deltaTime);
        tricksDetector.registerRotation(transform.rotation.eulerAngles.z);
    }

    public void Bounce() {
        if (tricksDetector.TrickDetected()) {
            animator.SetTrigger("SuperJump");
            myRigidbody.velocity = transform.up * boostBounceSpeed;
        }
        else 
        {
            myRigidbody.velocity = transform.up * baseBounceSpeed;
        }
        
        tricksDetector.Reset();
    }

    public void ResetInitialPosition()
    {
        transform.position = initialPosition;
        transform.rotation = Quaternion.identity;
        myRigidbody.velocity = Vector3.zero;
        tricksDetector.Reset();
    }

    //                                      used for debug
    //public void BounceTest() { 
    //    tricksDetector.Reset();
    //}


}
