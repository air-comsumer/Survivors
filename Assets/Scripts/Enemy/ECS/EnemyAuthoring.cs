using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using Unity.Transforms;
using Unity.VisualScripting;

public class EnemyAuthoring : MonoBehaviour
{
    public float moveSpeed = 4;
    public GameObject[] Children;
    public Vector3 scale = Vector3.one;
    public class EnemyBaker : Baker<EnemyAuthoring>
    {
        public override void Bake(EnemyAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<EnemyData>(entity);
            SetComponentEnabled<EnemyData>(entity, true);
            SetComponent<EnemyData>(entity, new EnemyData
            {
                health = 100f,
                die = false
            });
            AddComponent<RendererSortTag>(entity);
            SetComponentEnabled<RendererSortTag>(entity, true);
            AddSharedComponent<EnemySharedData>(entity,new EnemySharedData 
            {
                moveSpeed = authoring.moveSpeed,
                scale = (Vector2)authoring.transform.localScale,
                healthPoint = 100f,
            });
            AddComponent<AttackTargetTag>(entity,new AttackTargetTag{lastAttackedTimer = 2f,health=100f});
            SetComponentEnabled<AttackTargetTag>(entity, true);
        }
    }
}

