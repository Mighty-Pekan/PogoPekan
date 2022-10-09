using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ringraziamenti : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(ReturnToMainMenu());
    }

    private IEnumerator ReturnToMainMenu() {
        yield return new WaitForSeconds(10f);
        GameController.Instance.ReturnToMainMenu();
    }
}
