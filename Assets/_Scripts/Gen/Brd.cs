using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brd : MonoBehaviour
{
    public Transform vfx;
    private void Awake()
    {
        vfx.localPosition = Vector3.up * SpikeGen.S.GetRadius();
    }

}
