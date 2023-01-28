using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{

    [SerializeField] float timeBetweenFlameBursts, flameBurstLength;
    float timer;
    ParticleSystem flameParticles;

    private void Start()
    {
        timer = flameBurstLength;
    }

    private void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.CompareTag("Player"))
        {
            //damage player
        }
    }
}
