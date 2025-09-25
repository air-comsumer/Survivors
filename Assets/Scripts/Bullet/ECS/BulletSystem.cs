using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using Unity.Physics;
using Unity.Mathematics;
using System.Diagnostics;
using Unity.VisualScripting;
using System.Linq;

public partial struct BulletSystem : ISystem
{
    public readonly static SharedStatic<int> createBulletCount = SharedStatic<int>.GetOrCreate<BulletSystem>();
    public void OnCreate(ref SystemState state)
    {
        createBulletCount.Data = 0;
        SharedData.singtonEntity.Data = state.EntityManager.CreateEntity(typeof(BulletCreateInfo));
    }
    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer.ParallelWriter ecb = SystemAPI.GetSingleton<EndSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
        DynamicBuffer<BulletCreateInfo> bulletCreateInfoBuffer = SystemAPI.GetSingletonBuffer<BulletCreateInfo>();
        createBulletCount.Data = bulletCreateInfoBuffer.Length;
        new BulletJob()
        {
            enemyLayerMask = 1 << 3,
            ecb = ecb,
            deltaTime = SystemAPI.Time.DeltaTime,
            bulletCreateInfoBuffer = bulletCreateInfoBuffer,
            collisionWorld = SystemAPI.GetSingleton<PhysicsWorldSingleton>().CollisionWorld,
            enemyDataLookup = SystemAPI.GetComponentLookup<EnemyData>()
        }.ScheduleParallel();
        state.CompleteDependency();
        if (createBulletCount.Data > 0)
        {
            NativeArray<Entity> newBullets = new NativeArray<Entity>(createBulletCount.Data, Allocator.Temp);
            ecb.Instantiate(int.MinValue,SystemAPI.GetSingleton<GameConfigData>().bulletPortotype,newBullets);
            for(int i=0;i<newBullets.Length;i++)
            {
                BulletCreateInfo info = bulletCreateInfoBuffer[i];
                ecb.SetComponent<LocalTransform>(newBullets[i].Index, newBullets[i], new LocalTransform()
                {
                    Position = info.position,
                    Rotation = info.rotation,
                    Scale = 1
                });
            }
            newBullets.Dispose();
        }
        
        bulletCreateInfoBuffer.Clear();//这里会清空缓冲区
    }
    [WithOptions(EntityQueryOptions.IgnoreComponentEnabledState)]//�������������״̬
    [BurstCompile]
    public partial struct BulletJob : IJobEntity
    {
        public EntityCommandBuffer.ParallelWriter ecb;
        public uint enemyLayerMask;
        public float deltaTime;
        [ReadOnly] public DynamicBuffer<BulletCreateInfo> bulletCreateInfoBuffer;
        [ReadOnly] public CollisionWorld collisionWorld;

        [ReadOnly] public ComponentLookup<EnemyData> enemyDataLookup;
        public void Execute(EnabledRefRW<BulletData> bulletEnableState, EnabledRefRW<RenderSortTag> sortEnableState,
            in BulletSharedData bulletSharedData, ref LocalTransform localTransform, ref BulletData bulletData)
        {
            if (bulletEnableState.ValueRO == false)
            {//�ӵ�δ����
                if (BulletSystem.createBulletCount.Data > 0)
                {
                    int index = createBulletCount.Data -= 1;
                    bulletEnableState.ValueRW = true;//�����ӵ�
                    localTransform.Position = bulletCreateInfoBuffer[index].position;
                    localTransform.Rotation = bulletCreateInfoBuffer[index].rotation;
                    localTransform.Scale = 1;
                    bulletData.destroyTime = bulletSharedData.destroyTime;
                }
                return;
            }
            localTransform.Position += bulletSharedData.moveSpeed * deltaTime * (localTransform.Right() + localTransform.Up());
            //���ټ���
            bulletData.destroyTime -= deltaTime;
            if (bulletData.destroyTime <= 0)
            {
                bulletEnableState.ValueRW = false;
                sortEnableState.ValueRW = false;
                localTransform.Scale = 0;
                return;
            }
            //��ײ���
            NativeList<DistanceHit> hits = new NativeList<DistanceHit>(Allocator.Temp);
            CollisionFilter filter = new CollisionFilter()
            {
                BelongsTo = ~0u, // �ӵ����ڵĲ�
                CollidesWith = enemyLayerMask, // �ӵ�����˲���ײ
                GroupIndex = 0 // ��ײ������
            };
            if (collisionWorld.OverlapBox(localTransform.Position, localTransform.Rotation, bulletSharedData.colliderHalfExtents, ref hits, filter))
            {
                for (int i = 0; i < hits.Length; i++)
                {
                    Entity temp = hits[i].Entity;
                    var enemyData = enemyDataLookup.GetRefRO(temp);
                    float health = enemyData.ValueRO.health- bulletData.attackPower;
                    bulletData.destroyTime = 0;
                    if (health <= 0)
                    {
                        SharedData.gameScore.Data++;
                        ecb.SetComponent<EnemyData>(i, temp, new EnemyData()
                        {
                            health = health,
                            die = true
                        });

                    }
                    else
                    {
                        ecb.SetComponent<EnemyData>(i, temp, new EnemyData()
                        {
                            health = health,
                            die = false
                        });
                    }

                }
            }
            hits.Dispose();
        }
    }
}
