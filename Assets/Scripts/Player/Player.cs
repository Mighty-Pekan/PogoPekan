using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float baseBounceSpeed;
    [SerializeField] float boostBounceSpeed;

    TricksDetector tricksDetector;
    Rigidbody2D myRigidbody;
    Animator animator;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();    
        tricksDetector = new TricksDetector();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, -InputManager.GetRotation() * rotationSpeed * Time.deltaTime));
        tricksDetector.registerRotation(transform.rotation.z);

       
    }

    public void Bounce() {
        if (tricksDetector.TrickDetected()) {
            animator.SetTrigger("SuperJump");
            myRigidbody.velocity = transform.up * boostBounceSpeed;
        }
        else {
            myRigidbody.velocity = transform.up * baseBounceSpeed;
        }
        
        tricksDetector.Reset();

        // could be fun, but can create bugs
        //myRigidbody.AddForce(transform.up * baseBounceSpeed, ForceMode2D.Impulse);
    }

}
