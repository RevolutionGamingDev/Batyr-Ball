using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultiTargetCam : MonoBehaviour
{
    public static MultiTargetCam S;
    public List<Transform> targets;
    public float smoothTime;
    public Vector3 offset;

    public bool allowFollow;
    private Vector3 _velocity;

    private void Awake()
    {
        S = this;
    }

    public void Play()
    {
        StartCoroutine(OnPlay());
    }

    IEnumerator OnPlay()
    {
        yield return new WaitForSeconds(2);
        allowFollow = true;
    }
    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref _velocity, smoothTime);
    }
   
    private void LateUpdate()
    {
        if (!allowFollow)
            return;
        if (targets.Count == 0)
            return;
        
        
        Move();
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for(int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.center;
    }
}
