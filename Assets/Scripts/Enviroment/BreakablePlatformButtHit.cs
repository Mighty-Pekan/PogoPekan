using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatformButtHit : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AudioClip BreakSound;

    int life = 4;
    int tickDamage = 1;
    //float tickRateo = 0f; //increase to break platform slower

    private SpriteRenderer mySpriteRenderer;
    private int quarterOfLife;
    private float? lastTimeTookDamage = null;

    [SerializeField] GameObject destructionParticles;

    private void Start() {
        quarterOfLife = life / 4;
        mySpriteRenderer = GetComponent<SpriteRenderer>();

        GetComponent<BoxCollider2D>().size = new Vector2(mySpriteRenderer.size.x, mySpriteRenderer.size.y);
    }


    private void OnCollisionStay2D(Collision2D collision) {
        if (collision.collider.gameObject.tag == "BouncingTip" && GameController.Instance.GetPlayer().IsPerformingButtHit()) {
            //TakeDamage();
            Instantiate(destructionParticles, collision.contacts[0].point,Quaternion.identity);
            AudioManager.Instance.PlaySound(BreakSound);
            Destroy(gameObject);
        }

    }

    //public void TakeDamage()
    //{
    //    if(tickRateo == 0f)Destroy(gameObject);

    //    if (lastTimeTookDamage == null) lastTimeTookDamage = Time.time;
    //    if (Time.time - lastTimeTookDamage > tickRateo)
    //    {
    //        lastTimeTookDamage = Time.time;
    //        life -= tickDamage;

    //        if (life <= 0)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
