using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksDetector
{

    bool [] rotationSpots;

    public TricksDetector() {
        rotationSpots = new bool[3];
        Reset();
    }

    public void registerRotation(float _rotation) {

        //Debug.Log("received rotation: "+ _rotation);

        int spot;
        if (_rotation < 0.5 && _rotation > -0.5)
            spot = 0;
        else if (_rotation > 0.5)
            spot = 1;
        else
            spot = 2;

        rotationSpots[spot] = true;

       if(spot == 0) {
            if (!rotationSpots[1]) rotationSpots[2] = false;
            if (!rotationSpots[2]) rotationSpots[1] = false;
        }

        Debug.Log(rotationSpots[0] + "|" + rotationSpots[1] + "|" + rotationSpots[2]);

    }

    public void Reset() {
        for(int i = 0; i < rotationSpots.Length; i++)
            rotationSpots[i] = false;
    }

    public bool TrickDetected() {
        return rotationSpots[0] && rotationSpots[1] && rotationSpots[2];
    }
}
