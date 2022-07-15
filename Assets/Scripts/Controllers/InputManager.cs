using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] bool touchInputEnabled;

    int rotationDirection = 0;

    private bool leftPressed = false;
    private bool rightPressed = false;

    private enum eDirections { left, right }
    eDirections lastPressed = eDirections.left;

    public static void OnButtonLeftDown() {
        instance.leftPressed = true;
        instance.lastPressed = eDirections.left;
    }
    public static void OnButtonLeftUp() {
        instance.leftPressed = false;
    }
    public static void OnButtonRightDown() {
        instance.rightPressed = true;
        instance.lastPressed = eDirections.right;
    }
    public static void OnButtonRighttUp() {
        instance.rightPressed = false;
    }

    private void Update() {
        if(touchInputEnabled)
            ReadTouchInput();
        else
            ReadKeyboardInput();
    }

    private void ReadTouchInput() {
        if (leftPressed && !rightPressed) {
            rotationDirection = -1;
        }
        else if (rightPressed && !leftPressed) {
            rotationDirection = 1;
        }
        else if (!leftPressed && !rightPressed) {
            rotationDirection = 0;
        }
        else {  //both pressed at the same time
            if (lastPressed == eDirections.left) rotationDirection = -1;
            else rotationDirection = 1;
        }
    }

    private void ReadKeyboardInput() {
        if (Input.GetKey(KeyCode.LeftArrow)) rotationDirection = -1;
        else if (Input.GetKey(KeyCode.RightArrow)) rotationDirection = 1;
        else rotationDirection = 0;
    }

    public static int GetRotation() {
        return instance.rotationDirection;
    }
}
