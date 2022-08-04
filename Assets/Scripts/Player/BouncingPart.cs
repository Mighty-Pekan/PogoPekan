using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPart : MonoBehaviour
{
    [SerializeField] private Player player;

    private float resetBounceTime = 0.1f;
    private bool canBounce = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TipBounce(collision);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        TipBounce(collision);
        if(collision.gameObject.tag == "BreakablePlatformButtHit")
        {
            
            BreakablePlatformButtHit breakablePlatform = collision.GetComponent<BreakablePlatformButtHit>();
            breakablePlatform.TakeDamage();
        }
    }

    private void TipBounce(Collider2D other)
    {
        if(other.gameObject.tag != "Player" && !other.isTrigger)
        {
            player.Bounce();
        }
        
    }
}
