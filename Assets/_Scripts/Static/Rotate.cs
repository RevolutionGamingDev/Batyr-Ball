using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    public List<Orbit> orbits;

    public float speed;
    public float modifier;
    private void Awake()
    {
        modifier = 1;
    }

    private void FixedUpdate()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime * modifier);

        foreach (var orb in orbits)    
        {
            orb.transform.Rotate(0, 0, speed * Time.deltaTime * modifier * orb.parallaxFraction);
        }
    }

}
