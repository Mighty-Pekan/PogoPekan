using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadePanel : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 0.1f;
    [SerializeField] TextMeshProUGUI levelTextIndication;
    CanvasGroup canvasGroup;
    private void Start() {
        GameController.Instance.RegisterFadePanel(this);
        canvasGroup = GetComponent<CanvasGroup>();

        if (!GameController.Instance.isStartMenu()) {
            canvasGroup.alpha = 1;
            StartCoroutine(Disapear());
        }
    }

    public IEnumerator Apear(string levelName) {
        levelTextIndication.text = "LEVEL " + levelName;
        GameController.Instance.canPauseBeCalled = false;
        while (canvasGroup.alpha < 1) {
            canvasGroup.alpha += 0.03f;
            yield return new WaitForSeconds(1 / fadeSpeed);
            if (canvasGroup == null) yield break;
        }
        canvasGroup.alpha = 1;
        yield return new WaitForSeconds(1);
    }

    public IEnumerator Disapear() {
        levelTextIndication.text = "";
        GameController.Instance.canPauseBeCalled = false;
        while (canvasGroup.alpha > 0) {
            canvasGroup.alpha -= 0.03f;
            yield return new WaitForSeconds(1/fadeSpeed);
            if (canvasGroup == null) yield break;
        }
        canvasGroup.alpha = 0;
        GameController.Instance.canPauseBeCalled = true;
    }
}
