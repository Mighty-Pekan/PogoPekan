using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float baseBounceSpeed;
    [SerializeField] float boostBounceSpeed;
    TricksDetector tricksDetector;

    //modified by InputManger to determine rotation direction
    public int rotationDirection { get; set; }

    Rigidbody2D myRigidbody;

    private void Awake()
    {
        //myRigidbody = null; // delete this row
        myRigidbody = GetComponent<Rigidbody2D>();
        tricksDetector = new TricksDetector();
    }

    private void Update()
    {
        comandaCharacterConTastiera();

        transform.Rotate(new Vector3(0, 0, -rotationDirection * rotationSpeed * Time.deltaTime));
        tricksDetector.registerRotation(transform.rotation.z);
    }

    public void Bounce() {
        if (tricksDetector.TrickDetected()) {
            myRigidbody.velocity = transform.up * boostBounceSpeed;
        }
        else {
            myRigidbody.velocity = transform.up * baseBounceSpeed;
        }
        
        tricksDetector.Reset();

        // could be fun, but can create bugs
        //myRigidbody.AddForce(transform.up * baseBounceSpeed, ForceMode2D.Impulse);
    }

    private void comandaCharacterConTastiera() {
        if (Input.GetKey(KeyCode.LeftArrow)) rotationDirection = -1;
        else if (Input.GetKey(KeyCode.RightArrow)) rotationDirection = 1;
        else rotationDirection = 0;
    }

}
