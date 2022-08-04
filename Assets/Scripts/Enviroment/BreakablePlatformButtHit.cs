using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatformButtHit : MonoBehaviour
{
    [SerializeField] int life = 4;
    [SerializeField] int tickDamage = 1;
    [SerializeField] float tickRateo = 0.5f;

    private bool canTakeDamage = true;
    private SpriteRenderer mySpriteRenderer;
    private int quarterOfLife;

    private void Start() {
        quarterOfLife = life / 4;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        DecideColor();
    }

    public void TakeDamage()
    {
        if (canTakeDamage)
        {
            canTakeDamage = false;
            life -= tickDamage;

            DecideColor();

            if (life <= 0)
            {
                Destroy(gameObject);
            }
            StartCoroutine(EnableTakingDamage());
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
