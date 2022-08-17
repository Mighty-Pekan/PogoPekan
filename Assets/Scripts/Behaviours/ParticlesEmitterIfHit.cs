using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesEmitterIfHit : MonoBehaviour
{
    [SerializeField] GameObject particles;

    public GameObject getParticles() {
        return particles;
    }
}
