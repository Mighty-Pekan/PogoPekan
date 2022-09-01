using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnToLevelSelectionButton : MonoBehaviour
{
    public void ReturnToLevelSelection() {
        GameController.Instance.ReturnToMainMenu(true);
    }
}
