using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] bool touchInputEnabled;

    float halfScreenWidth = Screen.width / 2;
    Vector3 rotationDirection;

    private Touch touch;

    private void Update()
    {
        if(touchInputEnabled)
            MobileInput();
        else
            KeyboardInput();
    }

    private void MobileInput()
    {
        //Checks if is not UI click
        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            if (Input.touchCount == 1)
            {
                touch = Input.GetTouch(0);
                Debug.Log("Sto Premendo");

                Vector3 touchPosition = Input.touches[0].position;

                if (touchPosition.x < halfScreenWidth)
                {
                    Debug.Log("Sto premendo a Sinistra");
                    rotationDirection = Vector3.back;
                }
                else
                {
                    Debug.Log("Sto premendo a Destra");
                    rotationDirection = Vector3.forward;
                }
              
                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("Rilascio");
                    rotationDirection = Vector3.zero;
                }
            }
            else
            {
                rotationDirection = Vector3.zero;
            }
        }
    }
    private void KeyboardInput() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            rotationDirection = Vector3.back;

        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            rotationDirection = Vector3.forward;

        }
        else rotationDirection = Vector3.zero;

    }

    public static Vector3 GetRotationDirection()
    {
        return instance.rotationDirection;
    }

    public void EnableTouchInput()
    {
        touchInputEnabled = !touchInputEnabled;
    }
}
