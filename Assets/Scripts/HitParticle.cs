using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitParticle : MonoBehaviour
{
    public ParticleSystem hit;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void PlayHitParticle()
    {
        hit.Play();
    }
}