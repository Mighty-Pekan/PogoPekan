using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomMenuItems : Editor
{
    [MenuItem("Custom Debug/Clear PlayerPrefs")]
    static void DeletePlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
