using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float bounciness;
    [SerializeField] AudioClip BounceSound;
    [SerializeField] float animationMaxScale = 12;
    [SerializeField] float animationSpeed = 1f;

    private bool DoingAnimation = false;
    private float originalScale;

    private void Start() {
        originalScale = transform.localScale.x;

        if (transform.localScale.x != transform.localScale.y)
            Debug.LogWarning("mushroom x and y scale must be the same");
    }

    public float GetBounciness() {
        return bounciness;
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "Player") {
            AudioManager.Instance.PlaySound(BounceSound);
            StartCoroutine(DoAnimation());
        }
    }
    private IEnumerator DoAnimation() {
        if (!DoingAnimation) {
            DoingAnimation = true;

            while(transform.localScale.x < animationMaxScale) {
                float newVal = transform.localScale.x + animationSpeed;
                transform.localScale = new Vector3(newVal,newVal,newVal);
                yield return 0;
            }

            while(transform.localScale.x > originalScale) {
                float newVal = transform.localScale.x - animationSpeed;
                transform.localScale = new Vector3(newVal, newVal, newVal);
                yield return 0;
            }

            DoingAnimation = false;
        }
    }

}
