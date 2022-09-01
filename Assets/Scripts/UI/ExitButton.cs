using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void MainMenu() {
        GameController.Instance.ReturnToMainMenu();
    }
}
