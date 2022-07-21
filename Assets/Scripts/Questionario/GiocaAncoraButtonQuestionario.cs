using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiocaAncoraButtonQuestionario : MonoBehaviour
{
    public void giocaAncora() {
        GameController.GameOver();
        transform.parent.gameObject.SetActive(false);
    }
}
