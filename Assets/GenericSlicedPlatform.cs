using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSlicedPlatform : MonoBehaviour
{

    protected SpriteRenderer mySpriteRenderer;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<BoxCollider2D>().size = new Vector2(mySpriteRenderer.size.x, mySpriteRenderer.size.y);
    }
}
