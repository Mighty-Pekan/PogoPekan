using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncingPart : MonoBehaviour
{
    [SerializeField] private Player player;

    private float resetBounceTime = 0.02f;
    private bool canBounce = true;

    private void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag != "Player" && !(other.gameObject.tag == "BreakablePlatform" && player.IsPerformingButtHit()) && canBounce) {
            
            player.Bounce();

            HandleParticlesGeneration(other);

            canBounce = false;
        }
        StartCoroutine(enableBounceCor());
    }

    private void HandleParticlesGeneration(Collision2D other) {

        ParticlesEmitterIfHit otherEmitter = other.gameObject.GetComponent<ParticlesEmitterIfHit>();
        if(otherEmitter != null) {
            GameObject particles = Instantiate(otherEmitter.getParticles(), transform.position, Quaternion.identity);
            particles.transform.up = transform.up;
            particles.GetComponent<ParticleSystem>().Play();
        }
    }

    private void OnCollisionExit2D(Collision2D collision) {
        canBounce = true;
    }

    private IEnumerator enableBounceCor() {
        yield return new WaitForSeconds(resetBounceTime);
        canBounce = true;
    }
}
