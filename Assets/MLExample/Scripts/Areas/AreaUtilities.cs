using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AreaUtilities
{
    public static Vector3 GiveRandomSpawnPosition(Vector3 center, float minAngle, float maxAngle, float minRadius, float maxRadius)
    {
        float radius = minRadius;
        if(maxRadius > minRadius)
        {
            radius = Random.Range(minRadius, maxRadius);
        }


        return center + Quaternion.Euler(0f, Random.Range(minAngle, maxAngle), 0f) * Vector3.forward * radius;
    }
}
