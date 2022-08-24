using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 0.1f;
    CanvasGroup canvasGroup;
    private void Start() {
        GameController.Instance.RegisterFadePanel(this);
        canvasGroup = GetComponent<CanvasGroup>();

        if (!GameController.Instance.isStartMenu()) {
            canvasGroup.alpha = 1;
            StartCoroutine(Disapear());
        }
    }

    public IEnumerator Apear() {
        while (canvasGroup.alpha < 1) {
            canvasGroup.alpha += 0.03f;
            yield return new WaitForSeconds(1 / fadeSpeed);
            if (canvasGroup == null) yield break;
        }
        canvasGroup.alpha = 1;
    }

    public IEnumerator Disapear() {
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= 0.03f;
            yield return new WaitForSeconds(1/fadeSpeed);
            if (canvasGroup == null) yield break;
        }
        canvasGroup.alpha = 0;
    }
}
