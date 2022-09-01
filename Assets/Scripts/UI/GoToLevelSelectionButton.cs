using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToLevelSelectionButton : MonoBehaviour
{
    public void GoToLevelSelection() {
        GameController.Instance.ShowLevelsPanel = true;
    }
}
