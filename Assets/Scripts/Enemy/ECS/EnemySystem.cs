 using ProjectDawn.Navigation;
using System.Diagnostics;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using Unity.VisualScripting;

public partial struct EnemySystem : ISystem
{
    public struct keyClass1 { };
    public struct keyClass2 { };
    public struct keyClass3 { };
    public readonly static SharedStatic<int> createdCount = SharedStatic<int>.GetOrCreate<keyClass1>();//�Ѿ����ɵ�������
    public readonly static SharedStatic<int> createCount = SharedStatic<int>.GetOrCreate<keyClass2>();//����Ҫ���ɵ�������
    public readonly static SharedStatic<Random> random = SharedStatic<Random>.GetOrCreate<keyClass3>();//�����
    public float spawnEnemyTime;
    public const int maxEnemys = 10000;
    public void OnCreate(ref SystemState state)
    {
        createdCount.Data = 0;
        createCount.Data = 0;
        random.Data = new Random((uint)System.DateTime.Now.GetHashCode());
        SharedData.gameSharedInfo.Data.deadCount = 0;
        state.RequireForUpdate<GameConfigData>();
        
    }
    public void OnUpdate(ref SystemState state)
    {
        spawnEnemyTime -= SystemAPI.Time.DeltaTime;
        if (spawnEnemyTime <= 0)
        {
            spawnEnemyTime = SharedData.gameSharedInfo.Data.spawnInterval;
            createCount.Data += SharedData.gameSharedInfo.Data.spawnCount;
        }
        EntityCommandBuffer.ParallelWriter ecb = SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged).AsParallelWriter();
        float3 playerPos = SharedData.playerPos.Data;
        //武器目标缓冲区
        //DynamicBuffer<WeaponTargetBuffer> weaponTargetBuffer = SystemAPI.GetSingletonBuffer<WeaponTargetBuffer>();
        new EnemyJob
        {
            deltaTime = SystemAPI.Time.DeltaTime,
            playerPos = SharedData.playerPos.Data,
            time = SystemAPI.Time.ElapsedTime,
            ecb = ecb,
            //weaponTargetBuffer = weaponTargetBuffer,
            jobIndex = 1,
            detectionTime = 2f
        }.ScheduleParallel();
        state.CompleteDependency();
        if (createCount.Data > 0 && createdCount.Data <= maxEnemys)
        {
            NativeArray<Entity> enemys = new NativeArray<Entity>(createCount.Data, Allocator.Temp);
            ecb.Instantiate(int.MinValue, SystemAPI.GetSingleton<GameConfigData>().enemyPortotype, enemys);
            for (int i = 0; i < enemys.Length && createdCount.Data < maxEnemys; i++)
            {
                createdCount.Data += 1;
                float2 offset = random.Data.NextFloat2Direction() * random.Data.NextFloat(5f, 10f);
                ecb.SetComponent<LocalTransform>(enemys[i].Index, enemys[i], new LocalTransform()
                {
                    Position = new float3(playerPos.x + offset.x, playerPos.y + offset.y, 0),
                    Rotation = quaternion.identity,
                    Scale = 1
                });
                ecb.SetComponent<AttackTargetTag>(enemys[i].Index, enemys[i], new AttackTargetTag
                {
                    health = 100f
                });
                ecb.SetComponent<AnimationTime>(enemys[i].Index, enemys[i], new AnimationTime()
                {
                    value = (float)SystemAPI.Time.ElapsedTime
                });

            }
            createCount.Data = 0;
            enemys.Dispose();
        }
    }
    [WithOptions(EntityQueryOptions.IgnoreComponentEnabledState)]//�������������״̬
    [BurstCompile]
    public partial struct EnemyJob : IJobEntity
    {
        public float deltaTime;
        public float3 playerPos;
        public float detectionTime;//用来纠正血量的倒计时，如果在这个时间内没有被攻击，则恢复attackTargetTag的血量
        public double time;
        public EntityCommandBuffer.ParallelWriter ecb;
        //public DynamicBuffer<WeaponTargetBuffer> weaponTargetBuffer;
        public int jobIndex;
        public void Execute(EnabledRefRW<EnemyData> enableState, EnabledRefRW<RendererSortTag> renderSortEnableState, ref EnemyData enemyData,
            in EnemySharedData enemySharedData, ref LocalTransform localTransform, ref AnimationTime animationTime, ref AgentBody agentBody,
            ref LocalToWorld localToWorld, in Entity enemyEntity, ref AttackTargetTag attackTargetTag)
        {
            if (enableState.ValueRO == false)
            {
                if (createCount.Data > 0)
                {
                    createCount.Data -= 1;//����һ��
                    float2 offset = random.Data.NextFloat2Direction() * random.Data.NextFloat(5f, 10f);
                    localTransform.Position = new float3(playerPos.x + offset.x, playerPos.y + offset.y, 0);
                    enableState.ValueRW = true;
                    renderSortEnableState.ValueRW = true;
                    localTransform.Scale = 1;
                    animationTime.value = (float)time;
                    attackTargetTag.health = 100f;
                    enemyData.health = 100f;
                }
                return;
            }
            if (enemyData.die)
            {
                SharedData.gameSharedInfo.Data.deadCount += 1;
                enemyData.die = false;
                enableState.ValueRW = false;
                renderSortEnableState.ValueRW = false;
                attackTargetTag.health = 0f;
                localTransform.Scale = 0.001f;
                agentBody.Stop();
                return;
            }
            if (attackTargetTag.lastAttackedTimer > 0)
            {
                attackTargetTag.lastAttackedTimer -= deltaTime;
            }
            else
            {
                attackTargetTag.health = enemyData.health;
                attackTargetTag.lastAttackedTimer = detectionTime;
            }

            agentBody.SetDestination(new float3(playerPos.x, playerPos.y, 0));
            localToWorld.Value.c0.x = localTransform.Position.x < playerPos.x ? -enemySharedData.scale.x : enemySharedData.scale.x;
            localToWorld.Value.c1.y = enemySharedData.scale.y;
        }
    }
}
