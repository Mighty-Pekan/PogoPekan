using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPlatform : GenericSlicedPlatform
{
    [SerializeField] float rotatingSpeed;
    Rigidbody2D rb;

    protected void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (rotatingSpeed != 0) {
            rb.angularVelocity = rotatingSpeed;
        }
    }
}
