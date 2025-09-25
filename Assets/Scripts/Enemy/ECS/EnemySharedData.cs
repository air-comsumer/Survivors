using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct EnemySharedData : ISharedComponentData
{
    public float moveSpeed;
    public float healthPoint;
    public float2 scale;
}
