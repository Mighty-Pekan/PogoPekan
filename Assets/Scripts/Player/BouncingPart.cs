using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPart : MonoBehaviour
{
    [SerializeField] private Player player;

    private float resetBounceTime = 0.1f;
    private bool canBounce = true;

    //private void OnCollisionStay2D(Collision2D collision)
    //{
    //    TipBounce(collision.gameObject.tag);
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    TipBounce(collision.gameObject.tag);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TipBounce(collision.gameObject.tag);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        TipBounce(collision.gameObject.tag);
    }

    private void TipBounce(string otherTag)
    {
        if (otherTag != "Player" && canBounce)
        {
            canBounce = false;
            player.Bounce();
            StartCoroutine(resetBounce());
        }
        
    }
    private IEnumerator resetBounce()
    {
        yield return new WaitForSeconds(resetBounceTime);
        canBounce = true;
    }

}
