using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public partial struct RendererSortSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        new RendererSortJob() { }.ScheduleParallel();
    }
    [BurstCompile]
    public partial struct  RendererSortJob: IJobEntity
    {
        public void Execute(in RendererSortTag sortTag,ref LocalTransform localTransform)
        {
            localTransform.Position.z = localTransform.Position.y;
        }
    }
}
