using System.Numerics;
using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;

public static class SharedData
{
    public static readonly SharedStatic<Entity> singtonEntity = SharedStatic<Entity>.GetOrCreate<keyClass1>();
    public static readonly SharedStatic<GameSharedInfo> gameSharedInfo = SharedStatic<GameSharedInfo>.GetOrCreate<GameSharedInfo>();
    public static readonly SharedStatic<float3> playerPos = SharedStatic<float3>.GetOrCreate<keyClass2>();
    public static readonly SharedStatic<Entity> singtonEntity2  = SharedStatic<Entity>.GetOrCreate<keyClass3>();
    public static readonly SharedStatic<PlayerAABBInfo> playerColliderInfo = SharedStatic<PlayerAABBInfo>.GetOrCreate<PlayerAABBInfo>();
    public static readonly SharedStatic<int> gameScore = SharedStatic<int>.GetOrCreate<keyClass4>();
    public struct keyClass1 { };
    public struct keyClass2 { };
    public struct keyClass3 { };
    public struct keyClass4 { };


}



public struct PlayerAABBInfo
{
    public float3 detectionSize;
    public CollisionFilter collisionFilter;
}
public struct GameSharedInfo
{
    public int deadCount;
    public float spawnInterval;
    public int spawnCount;
}
