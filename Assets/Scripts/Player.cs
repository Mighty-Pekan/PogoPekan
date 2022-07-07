using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 2f;
    [SerializeField] float baseBounceSpeed;
    [SerializeField] float boosBounceSpeed;

    //modified by InputManger to determine rotation direction
    public int rotationDirection { get; set; }

    Rigidbody2D myRigidbody;

    private void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 0, -rotationDirection * rotationSpeed * Time.deltaTime));
    }

    public void Bounce() {
        //myRigidbody.AddForce(transform.up * baseBounceSpeed, ForceMode2D.Impulse);
        myRigidbody.velocity = transform.up * baseBounceSpeed;
    }
}
