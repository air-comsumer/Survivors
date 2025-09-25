using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class GameManagerAuthoring : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject enemyPrefab;
    public class GameManagerBaker : Baker<GameManagerAuthoring>
    {
        public override void Bake(GameManagerAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<GameConfigData>(entity, new GameConfigData()
            {
                bulletPortotype = GetEntity(authoring.bulletPrefab, TransformUsageFlags.Dynamic),
                enemyPortotype = GetEntity(authoring.enemyPrefab, TransformUsageFlags.Dynamic)
            });
        }
    }
}
