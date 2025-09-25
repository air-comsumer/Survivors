using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
//[UpdateAfter(typeof(EnemySystem))]
public partial struct WeaponSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {
        SharedData.singtonEntity2.Data = state.EntityManager.CreateEntity(typeof(WeaponTargetBuffer));//创建一个单例实体，用于存储武器目标缓冲区
        
    }
    public void OnUpdate(ref SystemState state)
    {
        Weapon[] weapons = GameObject.FindObjectsByType<Weapon>(FindObjectsSortMode.None);
        NativeList<DistanceHit> overlapHits = new NativeList<DistanceHit>(100, state.WorldUpdateAllocator);
        var physicsWorldSingleton = SystemAPI.GetSingleton<PhysicsWorldSingleton>();
        if (!physicsWorldSingleton.OverlapBox(SharedData.playerPos.Data, quaternion.identity, SharedData.playerColliderInfo.Data.detectionSize,
            ref overlapHits, SharedData.playerColliderInfo.Data.collisionFilter))
        {
            overlapHits.Dispose();
            return;
        }
        var sortJob = new DistanceSortJob
        {
            hits = overlapHits
        }.Schedule();
        sortJob.Complete();
        int weaponIndex = 0;
        foreach (var overlapHit in overlapHits)
        {
            var tempEntity = overlapHit.Entity;
            var tag = state.EntityManager.GetComponentData<AttackTargetTag>(tempEntity);
            while (tag.health > 0 && weaponIndex < weapons.Length)
            {
                if (weapons[weaponIndex].isCold)
                {
                    weaponIndex++;
                    continue;//武器正在冷却
                }
                var angleToCloasestEnemy = weapons[weaponIndex].RotateWeapon(overlapHit.Position);
                tag.health -= weapons[weaponIndex].weaponData.AttackPower;
                state.EntityManager.SetComponentData(tempEntity, tag);
                weapons[weaponIndex].GenerateBullet(angleToCloasestEnemy);
                weaponIndex++;
            }
        }
    }
    [BurstCompile]
    public partial struct  DistanceSortJob : IJob
    {
        public NativeList<DistanceHit> hits; 
        
        public void Execute()
        {
            for (int i = 0; i < hits.Length; i++)
            {
                for (int j = i + 1; j < hits.Length; j++)
                {
                    if (hits[i].Distance > hits[j].Distance)  // 比较distance字段，按升序排序
                    {
                        // 交换元素
                        DistanceHit temp = hits[i];
                        hits[i] = hits[j];
                        hits[j] = temp;
                    }
                }
            }
        }
    }
}
