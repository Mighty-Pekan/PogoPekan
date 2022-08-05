using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] GameObject[] Waypoints;
    [SerializeField] float Speed;

    int nextWayPoint;

    private void Awake()
    {

        if (Waypoints.Length < 2) Debug.LogError("MovingPlatform: HAI SCORDATO DI INSERIRE I WAYPOINT (i way points devono essere almeno 2");
        transform.position = Waypoints[0].transform.position;
        nextWayPoint = 1;
    }

    private void Update()
    {

        float step = Speed * Time.deltaTime;

        transform.position = Vector2.MoveTowards(transform.position, Waypoints[nextWayPoint].transform.position, step);

        if (Mathf.Abs(transform.position.x - Waypoints[nextWayPoint].transform.position.x) < Mathf.Epsilon
            || Mathf.Abs(transform.position.y - Waypoints[nextWayPoint].transform.position.y) < Mathf.Epsilon)
        {
            if (nextWayPoint + 1 < Waypoints.Length)
            {
                nextWayPoint++;
            }
            else
            {
                nextWayPoint = 0;
            }
            Debug.Log(nextWayPoint);
        }

    }

}
