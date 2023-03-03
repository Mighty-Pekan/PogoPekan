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

    [MenuItem("Custom Debug/Unlock All Levels")]
    static void UnlockAllLevels()
    {
        for (int level = 0; level <= GameController.Instance.NumLevelsPerWorld; level++)
        {
            for (int world = 0; world <= GameController.Instance.NumWorlds; world++)
            {
                LevelsDataManager.Instance.UnlockLevel(world, level);
            }
        }
    }
}
