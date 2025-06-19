
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct BulletCreateInfo : IBufferElementData
{
    public float3 position; 
    public Quaternion rotation;
}
