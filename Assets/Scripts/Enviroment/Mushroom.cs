using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] float bounciness;
    public float GetBounciness() {
        return bounciness;
    }
}
