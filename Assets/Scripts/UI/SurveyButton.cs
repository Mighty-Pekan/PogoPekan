using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveyButton : MonoBehaviour
{
    public void OnClick() {
        Application.OpenURL("https://docs.google.com/forms/d/12fkjzmodjoWwn9AU-eaTYuJbe0MvCX7OkqNEg7HGbhw/edit");
    }
}
