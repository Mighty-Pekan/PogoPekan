using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorns : MonoBehaviour
{
    [SerializeField] private Sprite DeathSprite;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.tag == "Player")
        {
            //GameObject.Find("Ground").GetComponent<Collider2D>().enabled = false;
            //this.GetComponent<Collider2D>().enabled = false;
            GameController.Instance.GameOver();
            GameObject player = GameObject.Find("Player");
            GameObject hammer = GameObject.Find("Hammer");
            player.GetComponent<Collider2D>().enabled = false;
            hammer.GetComponent<Collider2D>().enabled = false;
            GameObject.Find("BouncingTip").GetComponent<Collider2D>().enabled = false;
            ////player.GetComponent<Rigidbody2D>().gravityScale = 0;
            //player.GetComponent<Rigidbody2D>().rotation = 5;
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(3, 5);
            //player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //player.GetComponent<Animator>().SetBool("IsDead", true);
            player.GetComponent<Animator>().enabled = false;
            hammer.GetComponentInParent<Animator>().enabled = false;
            player.GetComponent<SpriteRenderer>().sprite = DeathSprite;
            ////StartCoroutine(OnStartFall(player));
        }
    }
    IEnumerator OnStartFall(GameObject gameObject)
    {
        var maxTrembleTime = 0.05f;
        var startRotation = gameObject.transform.rotation.z;
        var rotation = 0.1f;
        var currentTime = 0.0f;

        ////gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        ////gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        ////GameObject.Find("Ground").GetComponent<Collider2D>().enabled = false;
        while (currentTime < maxTrembleTime)
        {
            Debug.Log("Rotation " + gameObject.GetComponent<Rigidbody2D>().rotation);
            if (gameObject.transform.rotation.z >= startRotation + rotation ||
                gameObject.transform.rotation.z <= startRotation - rotation)
                rotation *= -1;
            currentTime = Mathf.Clamp(currentTime + Time.deltaTime, 0, maxTrembleTime);
            gameObject.transform.rotation = new Quaternion(gameObject.transform.rotation.x, gameObject.transform.rotation.y,
                gameObject.transform.rotation.z + rotation, gameObject.transform.rotation.w);

            yield return new WaitForSeconds(0.1F); // wait until next frame
        }
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 1;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(3, 5);

        // Begin normal falling code

        //while (true)
        //{
        //    var frameMovement = fallSpeed * Time.deltaTime;
        //    var targetPosition = target.transform.position;
        //    transform.position = Vector2.MoveTowards(transform.position, targetPosition, frameMovement);

        //    if (transform.position == targetPosition)
        //    {
        //        gameObject.layer = LayerMask.NameToLayer("Ground");
        //        boxCollider.enabled = true;
        //        break;
        //    }

        //    yield return null; // wait until next frame.
        //}
    }
}
