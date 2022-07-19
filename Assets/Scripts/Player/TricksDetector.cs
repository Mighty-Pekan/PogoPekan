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
        //first evaluation after bounce
        if (startingRot == null) {

            startingRot = _rotation;
            previousRot = convertRotation(_rotation, (float)startingRot);

            SetTriggerAntiorario();
                
        }
        //successive calculations
        else {
            float convertedRot = convertRotation(_rotation,(float)startingRot);

            if (!triggerActivated) {
                if (previousRot < 30 && convertedRot > 330)
                    SetTriggerOrario();

                else if (previousRot > 330 && convertedRot < 30)
                    SetTriggerAntiorario();

                else if ((rotationVerse == false && convertedRot < triggerAngle)
                || (rotationVerse == true && convertedRot > triggerAngle)) {
                    triggerActivated = true;
                    //Debug.Log("TRIGGER ACTIVATED, rotation verse = "+rotationVerse+", trigger angle: "+triggerAngle+",actual angle: "+convertedRot);
                }
            }
            previousRot = convertedRot;
        }

    }

    private float convertRotation(float _originalRot, float _startingRot) {
        float ris = _originalRot - _startingRot;
        if (ris < 0) {
            ris = 360 - _startingRot + _originalRot;
        }
        return ris;
    }

    private void SetTriggerAntiorario() {
        rotationVerse = true;
        triggerAngle = 360 - TrickTollerance;
        triggerActivated = false;
        //Debug.Log("trigger angle set to:" + triggerAngle);
    }
    private void SetTriggerOrario() {
        rotationVerse = false;
        triggerAngle = TrickTollerance;
        triggerActivated = false;
        //Debug.Log("trigger angle set to:" + triggerAngle);
    }

    public bool TrickDetected() {
        return triggerActivated;
    }

    public void Reset() {
        startingRot = null;
        previousRot = null;
        triggerAngle = null;
        triggerActivated = false;
        rotationVerse = true;
    }
}