using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadLevelButton : MonoBehaviour
{
    public void Reload() {
        GameController.Instance.ReloadLevel();
    }
}
