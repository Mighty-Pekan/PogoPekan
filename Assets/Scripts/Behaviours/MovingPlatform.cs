using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] GameObject WayPoint1;
    [SerializeField] GameObject WayPoint2;
    [SerializeField] float Speed;
    bool lerp = false;

    GameObject nextWayPoint;

    private void Awake()
    {
        transform.position = WayPoint1.transform.position;
        nextWayPoint = WayPoint2;
    }

    private void Update()
    {

        if (lerp) {
                transform.position = Vector2.Lerp(transform.position, nextWayPoint.transform.position, Speed * Time.deltaTime);
        }
        else {
            transform.position = Vector2.MoveTowards(transform.position, nextWayPoint.transform.position, Speed*Time.deltaTime);
        }

        if (Mathf.Abs(transform.position.y - nextWayPoint.transform.position.y) < 0.2f) {
            if (nextWayPoint == WayPoint1)
                nextWayPoint = WayPoint2;
            else nextWayPoint = WayPoint1;

            Debug.Log("waypoint reached");
        }
    }
}
