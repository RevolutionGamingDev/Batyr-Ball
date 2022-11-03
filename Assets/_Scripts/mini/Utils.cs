using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Obstacles
{
    public string id;
    public GameObject prefab;
    [Range(0, 100)]
    public float probability;

    public int spawnAfterTicks;
}

[System.Serializable]
public static class Utils
{
    public static Vector3 GetPosition(float angle)
    {
        Vector3 dir = new Vector3(
            Mathf.Cos(angle),
            Mathf.Sin(angle),
            0);
        return dir;
    }

    public static float plankDist = 1.11f;
}

[System.Serializable]
public class Orbit
{
    public Transform transform;
    [Range(0, 1.0f)] public float parallaxFraction;
}

[System.Serializable]
public class Trees
{
    public GameObject prefab;
    [Range(0, 1.0f)] public float probability;
}


