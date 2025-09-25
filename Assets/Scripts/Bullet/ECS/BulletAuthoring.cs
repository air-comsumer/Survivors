using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class BulletAuthoring : MonoBehaviour
{
    public float moveSpeed;
    public float attackPower;
    public float destroyTime;
    public class BulletBaker : Baker<BulletAuthoring>
    {
        public override void Bake(BulletAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<RenderSortTag>(entity); //������Ⱦ�����ǩ
            SetComponentEnabled<RenderSortTag>(entity, true); //������Ⱦ�����ǩ
            AddComponent<BulletData>(entity, new BulletData()
            {
                attackPower = authoring.attackPower,
                destroyTime = authoring.destroyTime
            });
            SetComponentEnabled<BulletData>(entity, true);
            Vector2 collidersize = authoring.GetComponent<BoxCollider2D>().size/2;
            AddSharedComponent<BulletSharedData>(entity, new BulletSharedData()
            {
                moveSpeed = authoring.moveSpeed,
                destroyTime = authoring.destroyTime,
                colliderOffset = authoring.GetComponent<BoxCollider2D>().offset,//��ײ��ƫ��
                colliderHalfExtents = new Unity.Mathematics.float3(collidersize.x,collidersize.y,10000)//��ײ���С
            });
        }
    }
}
