using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BulletSharedData : ISharedComponentData
{
    public float moveSpeed;
    public float destroyTime;
    public float2 colliderOffset;
    public float3 colliderHalfExtents;
}
