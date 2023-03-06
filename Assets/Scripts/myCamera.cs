using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myCamera : MonoBehaviour
{
    private void Start() {
        GameController.Instance.RegisterCameraAnimator(GetComponent<Animator>());   
    }
}
