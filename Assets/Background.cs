using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Background : MonoBehaviour
{
    [SerializeField] Camera cam;
    void Start()
    {
        transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = cam.transform.position;
    }
}
