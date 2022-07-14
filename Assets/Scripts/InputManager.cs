using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoSingleton<InputManager>
{
    Player player;
    private void Start() {
        player = FindObjectOfType<Player>();
    }

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
        if (leftPressed && !rightPressed) {
            player.rotationDirection = -1;
        }
        else if (rightPressed && !leftPressed) {
            player.rotationDirection = 1;
        }
        else if (!leftPressed && !rightPressed) {
            player.rotationDirection = 0;
        }
        else {  //both pressed at the same time
            if (lastPressed == eDirections.left) player.rotationDirection = -1;
            else player.rotationDirection = 1;
        }

    }
}
