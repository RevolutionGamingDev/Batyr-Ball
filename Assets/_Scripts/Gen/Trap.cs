using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public SpriteRenderer spr;
    public ParticleSystem idlePrt;
    public ParticleSystem jumpPrt;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Controll.S.MakeJump(1.5f);
        spr.enabled = false;
        idlePrt.Stop();
        jumpPrt.Play();
    }
}
