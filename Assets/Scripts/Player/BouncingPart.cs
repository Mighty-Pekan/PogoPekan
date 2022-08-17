using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPart : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject groundParticles;

    private float resetBounceTime = 0.02f;
    private bool canBounce = true;

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag != "Player" && !(other.gameObject.tag == "BreakablePlatform" && player.IsPerformingButtHit()) && canBounce) {
            player.Bounce();
            Instantiate(groundParticles, transform.position, Quaternion.identity);
            canBounce = false;
        }
        StartCoroutine(enableBounceCor());
    }

    private void OnCollisionExit2D(Collision2D collision) {
        canBounce = true;
    }

    private IEnumerator enableBounceCor() {
        yield return new WaitForSeconds(resetBounceTime);
        canBounce = true;
    }
}
