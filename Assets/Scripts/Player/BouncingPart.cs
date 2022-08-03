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
        TipBounce(collision.gameObject.tag);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        TipBounce(collision.gameObject.tag);
    }

    private void TipBounce(string otherTag)
    {
        if(otherTag != "Player")
        {
            player.Bounce();
        }
        
    }
}
