using Unity.Burst;
using Unity.Entities;

public static class SharedData
{
    public static readonly SharedStatic<Entity> singtonEntity = SharedStatic<Entity>.GetOrCreate<keyClass1>();
    public struct keyClass1 { };
    public struct keyClass2 { };
}
