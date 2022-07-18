using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] bool touchInputEnabled;

    float halfScreenWidth = Screen.width / 2;
    int rotationDirection = 0;

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
                    rotationDirection = -1;
                }
                else
                {
                    Debug.Log("Sto premendo a Destra");
                    rotationDirection = 1;
                }
              
                if (touch.phase == TouchPhase.Ended)
                {
                    Debug.Log("Rilascio");
                    rotationDirection = 0;
                }
            }
            else
            {
                rotationDirection = 0;
            }
        }
    }
    private void KeyboardInput() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            Debug.Log("Clicco a sinistra con la tastiera");
            rotationDirection = -1;
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            Debug.Log("Clicco a destra con la tastiera");
            rotationDirection = 1;
        }
        else rotationDirection = 0;
    }

    public static int GetRotation() {
        return instance.rotationDirection;
    }

    public void EnableTouchInput()
    {
        touchInputEnabled = !touchInputEnabled;
    }
}
