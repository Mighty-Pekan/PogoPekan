using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ParallaxScript : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera cam;

    [SerializeField] Vector2 backgroundSpeed;
    Vector3 lastCameraPosition;

    float length;

    void Start()
    {
        transform.parent.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, transform.parent.transform.position.z);
        lastCameraPosition = cam.transform.position;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = cam.transform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * backgroundSpeed.x, deltaMovement.y * backgroundSpeed.y);
        lastCameraPosition = cam.transform.position;

        //Sposta il background a Destra o a Sinistra in base alla posizione della telecamera
        if (cam.transform.position.x - transform.position.x > length / 2)
        {
            transform.position = new Vector3(transform.position.x + length, transform.position.y);
        }
        else if (cam.transform.position.x - transform.position.x < -length / 2)
        {
            transform.position = new Vector3(transform.position.x - length, transform.position.y);

        }
    }
}
