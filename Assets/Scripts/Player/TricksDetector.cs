using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TricksDetector {

    float TrickTollerance = 80;     //edit this to change how easy it is to make a trick
    float? startingRot;
    float? previousRot;
    float? triggerAngle = null;
    bool rotationVerse = true;
    bool triggerActivated = false;
    public TricksDetector() {
        Reset();
    }

    public void registerRotation(float _rotation) {
        if (startingRot == null) {
            startingRot = _rotation;
            previousRot = _rotation;
            SetTriggerAntiorario();
        }
        else {
            float convertedRot = _rotation - (float)startingRot;
            if (convertedRot < 0) {
                convertedRot = 360 - (float)startingRot + _rotation;
            }

            Debug.Log(convertedRot);

            if (!triggerActivated) {
                if (previousRot < 30 && convertedRot > 330)
                    SetTriggerOrario();

                else if (previousRot > 330 && convertedRot < 30)
                    SetTriggerAntiorario();

                if ((rotationVerse == false && convertedRot < triggerAngle)
                || (rotationVerse == true && convertedRot > triggerAngle)) {
                    triggerActivated = true;
                    Debug.Log("trigger activated:");
                }
            }
            previousRot = convertedRot;
        }

    }

    private void SetTriggerAntiorario() {
        rotationVerse = true;
        triggerAngle = 360 - TrickTollerance;
        triggerActivated = false;
        Debug.Log("trigger angle set to:" + triggerAngle);
    }
    private void SetTriggerOrario() {
        rotationVerse = false;
        triggerAngle = TrickTollerance;
        triggerActivated = false;
        Debug.Log("trigger angle set to:" + triggerAngle);
    }

    public bool TrickDetected() {
        return triggerActivated;
    }

    public void Reset() {
        startingRot = null;
        previousRot = null;
        triggerAngle = null;
        rotationVerse = true;
        rotationVerse = true;
        triggerActivated = false;
    }
}