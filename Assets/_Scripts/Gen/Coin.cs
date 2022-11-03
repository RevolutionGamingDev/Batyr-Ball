 using System;
 using System.Collections;
using System.Collections.Generic;
 using TMPro;
 using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject spr;
    public ParticleSystem particles;
    private void OnTriggerEnter2D(Collider2D other)
 {
     particles.Play();
     spr.SetActive(false);
     GameManager.S.IncreaseScore();
     Destroy(gameObject);
 }
}
