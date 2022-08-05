using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InputManager : MonoSingleton<InputManager>
{
    [SerializeField] bool touchInputEnabled;
    [SerializeField] float butHittStartDelay = 0.1f;    //prevents accidental double hold

    float halfScreenWidth = Screen.width / 2;
    Vector3 rotationDirection;

    private Touch touch;

    private bool isDoubleHold = false;
    float? doubleHoldStartTime = null;

    private void Start() {
        GameController.Instance.RegisterInputManager(this);
    }

    private void Update()
    {
        if (touchInputEnabled)
            MobileInput();
        else
            KeyboardInput();

        
    }

    bool leftHold = false;
    bool rightHold = false;

    private void MobileInput()
    {
        //Checks if is not UI click
        if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        //if (!EventSystem.current.IsPointerOverGameObject(touch.fingerId))
        {
            if (Input.touchCount > 0)       //nella vecchia versione qui {tutto il resto}
                touch = Input.GetTouch(0);

            //---------------------double hold handling
            for (int i = 0; i < Input.touchCount; i++)
            {
                Vector3 touchPosition = Input.touches[i].position;

                if (touchPosition.x < halfScreenWidth)
                    leftHold = true;
                if (touchPosition.x > halfScreenWidth)
                    rightHold = true;
            }

            if (leftHold && rightHold)
            {
                rotationDirection = Vector3.zero;

                if (doubleHoldStartTime == null)
                {
                    doubleHoldStartTime = Time.time;
                }
                else if (Time.time - doubleHoldStartTime >= butHittStartDelay)
                {
                    isDoubleHold = true;
                }
            }
            //----------------------rotation handling
            else
            {
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
        if(!leftHold && !rightHold){
            isDoubleHold = false;
            doubleHoldStartTime = null;
        }
        leftHold = false;
        rightHold = false;
    }
    private void KeyboardInput() {

        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if(GameController.Instance.IsPause)
            {
                UIManager.Instance.OpenPausePanel(false);
            }
            else
            {
                UIManager.Instance.OpenPausePanel(true);
            }
        }

        //---------------------double hold handling
        if( 
            (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) &&
            (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            ) {
            rotationDirection = Vector3.zero;

            if (doubleHoldStartTime == null) {
                doubleHoldStartTime = Time.time;
            }
            else if(Time.time - doubleHoldStartTime >= butHittStartDelay) {
                isDoubleHold = true;
            }
        }
        //---------------------rotation handling
        else {
            isDoubleHold = false;
            doubleHoldStartTime = null;          
        
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            rotationDirection = Vector3.forward;

        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            rotationDirection = Vector3.back;

        else rotationDirection = Vector3.zero;
        }
    }

    public Vector3 GetRotationDirection()
    {
        return rotationDirection;
    }

    
    public bool IsDoubleHold() {
        return isDoubleHold;
    }

    public void ToggleTouchInput()
    {
        touchInputEnabled = !touchInputEnabled;
        Debug.Log("touch input switched: " + touchInputEnabled);
    }
}
