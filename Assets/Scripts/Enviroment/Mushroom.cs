using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float bounciness;
    [SerializeField] Animator anim;

    public float GetBounciness() {
        return bounciness;
    }

}
