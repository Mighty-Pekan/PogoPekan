using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatformButtHit : MonoBehaviour
{
    int life = 4;
    int tickDamage = 1;
    float tickRateo = 0f; //increase to break platform slower

    private SpriteRenderer mySpriteRenderer;
    private int quarterOfLife;
    private float? lastTimeTookDamage = null;

    private void Start() {
        quarterOfLife = life / 4;
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        DecideColor();
    }


    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "BouncingTip" && GameController.Instance.GetPlayer().IsPerformingButtHit())
            TakeDamage();
    }


    public void TakeDamage()
    {
        if(tickRateo == 0f)Destroy(gameObject);

        if (lastTimeTookDamage == null) lastTimeTookDamage = Time.time;
        if (Time.time - lastTimeTookDamage > tickRateo)
        {
            lastTimeTookDamage = Time.time;
            life -= tickDamage;

            DecideColor();

            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void DecideColor() {
        if (life > 3 * quarterOfLife) mySpriteRenderer.color = Color.yellow;
        else if (life > 2 * quarterOfLife) mySpriteRenderer.color = new Color(1, 0.6f, 0, 1);
        else if (life > quarterOfLife) mySpriteRenderer.color = new Color(1, 0.3f, 0, 1); 
        else mySpriteRenderer.color = Color.red;
    }
}
