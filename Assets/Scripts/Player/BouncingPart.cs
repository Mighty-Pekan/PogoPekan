using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPart : MonoBehaviour
{
    [SerializeField] GameObject PlayerObj;
    Player player;

    private void Awake() {
        player = PlayerObj.GetComponent<Player>();
    }

    //private void OnCollisionEnter2D(Collision2D collision) {
    //    if(collision.gameObject.tag != "Player")
    //        player.Bounce();
    //}

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag != "Player")
            player.Bounce();
    }
}
