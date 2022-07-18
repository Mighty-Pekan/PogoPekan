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
            if (Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                for (int i = 0; i < Input.touchCount; i++)
                {
                    Vector3 touchPosition = Input.touches[i].position;

                    if (touchPosition.x < halfScreenWidth)
                        rotationDirection = Vector3.forward;
                    else
                        rotationDirection = Vector3.back;

                    if (touch.phase == TouchPhase.Ended)
                        rotationDirection = Vector3.zero;
                }
            }
        }
    }

    private void KeyboardInput() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            rotationDirection = Vector3.forward;
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            rotationDirection = Vector3.back;
        else rotationDirection = Vector3.zero;
    }

    public static Vector3 RotationDirection()
    {
        return instance.rotationDirection;
    }

    public void EnableTouchInput()
    {
        touchInputEnabled = !touchInputEnabled;
    }
}
