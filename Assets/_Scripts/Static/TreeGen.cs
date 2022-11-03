using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TreeGen : MonoBehaviour
{
    public List<Trees> trees;
    public int count;
    private float _radius;

    private void Start()
    {
        _radius = SpikeGen.S.GetRadius();

        float angleDelay = 360.0f / count;
        float ang = angleDelay;
        for (int i = 0; i < count; i++)
        {
            int ind = Random.Range(0, trees.Count);
            
            Spawn(ind, ang * Mathf.Deg2Rad);
            ang += angleDelay + Random.Range(-angleDelay / 3.0f, angleDelay / 3.0f);
        }
    }

    private void Spawn(int i, float angle)
    {
        Vector3 position = Utils.GetPosition(angle) * (_radius + 1f) + transform.position;
        GameObject go = Instantiate(trees[i].prefab, position, Quaternion.Euler(0, 0, angle*Mathf.Rad2Deg - 90),transform);
    }
}
