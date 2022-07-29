using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatformButtHit : MonoBehaviour
{
    [SerializeField] int life = 4;
    [SerializeField] int tickDamage = 1;
    [SerializeField] float tickRateo = 0.5f;

    bool canTakeDamage = true;
    SpriteRenderer mySpriteRenderer;
    int quarterOfLife;

    private void Start() {
        quarterOfLife = life / 4;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        DecideColor();
    }

    private void OnCollisionStay2D(Collision2D other) {
        if (canTakeDamage) {
            if (other.contacts[0].collider.gameObject.tag == "BouncingTip") {

                if (other.gameObject.GetComponent<Player>().isDoingButtHit()) {
                    canTakeDamage = false;
                    life -= tickDamage;

                    DecideColor();

                    if (life <= 0) {
                        Destroy(gameObject);
                    }
                    StartCoroutine(EnableTakingDamage());
                }
            }
        }
    }

    private void DecideColor() {
        if (life > 3 * quarterOfLife) mySpriteRenderer.color = Color.yellow;
        else if (life > 2 * quarterOfLife) mySpriteRenderer.color = new Color(1, 0.6f, 0, 1);
        else if (life > quarterOfLife) mySpriteRenderer.color = new Color(1, 0.3f, 0, 1); 
        else mySpriteRenderer.color = Color.red;
    }

    private IEnumerator EnableTakingDamage() {
        yield return new WaitForSeconds(tickRateo);
        canTakeDamage = true;
    }

}
