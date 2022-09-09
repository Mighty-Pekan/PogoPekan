using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyButton : MonoBehaviour
{
    public void OnClick() {
        Application.OpenURL("https://docs.google.com/forms/d/1Jdntp2Cx8-1V-_RIvJqM5gd-JBbO5aWKb7KaBevvvgw/prefill");
    }
}
