using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyButton : MonoBehaviour
{
    [SerializeField] string url;
    public void OnClick() {
        Application.OpenURL(url);
    }
}
