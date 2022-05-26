using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    public ParticleSystem hit;

    public void PlayHitParticle()
    {
        hit.Play();
    }
}