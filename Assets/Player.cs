using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float bounceSpeed;
    Rigidbody2D myRigidbody;
    CapsuleCollider2D collider;



    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        collider = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        HandleMovement(); 
    }

    int rotationDirection = 0;
    void HandleMovement()
    {

        if (Input.GetKey(KeyCode.LeftArrow))
            rotationDirection = 1;
        else if(Input.GetKey(KeyCode.RightArrow))
            rotationDirection = -1;
        else
            rotationDirection = 0;

        transform.Rotate(new Vector3(0, 0, rotationDirection * rotationSpeed));
    }

    public void Bounce() {
        myRigidbody.velocity = transform.up * bounceSpeed;
    }

}
