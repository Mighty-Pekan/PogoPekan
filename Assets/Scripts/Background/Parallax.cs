using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public float multiplier;
    public bool horizontalOnly;
    public bool calculateInfiniteHorizontalPosition;
    public bool calculateInfiniteVerticalPosition;
    public bool isInfinite;

    private Camera cam;
    private Vector3 startPosition;
    private Vector3 startCameraPosition;
    private float length;

    private void Start()
    {
        cam = Camera.main;
        startPosition = transform.position;
        startCameraPosition = cam.transform.position;

        if (isInfinite)
            length = GetComponent<SpriteRenderer>().bounds.size.x;

        CalculateStartPosition();

    }

    void CalculateStartPosition()
    {
        float distX = (cam.transform.position.x - startPosition.x) * multiplier;
        float distY = (cam.transform.position.y - startPosition.y) * multiplier;

        Vector3 temp = new Vector3(startPosition.x, startPosition.y);

        if (calculateInfiniteHorizontalPosition)
            temp.x = transform.position.x * distX;
        if (calculateInfiniteVerticalPosition)
            temp.y = transform.position.y * distY;

        startPosition = temp;
    }

    private void FixedUpdate()
    {
        Vector3 position = startPosition;

        if (horizontalOnly)
            position.x += multiplier * (cam.transform.position.x - startCameraPosition.x);
        else
            position += multiplier * (cam.transform.position - startCameraPosition);

        transform.position = position;

        if(isInfinite)
        {
            float temp = cam.transform.position.x * (1 - multiplier);
            if (temp > startPosition.x * length)
                startPosition.x += length;
            else if(temp < startPosition.x - length)
                startPosition.x -= length;
        }
    }
}
