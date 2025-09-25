using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;

public class AnimationAuthoring : MonoBehaviour
{
    public float frameRate;
    public int frameCount;
    public class AnimationBaker:Baker<AnimationAuthoring>
    {
        public override void Bake(AnimationAuthoring authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            AddComponent<AnimationTime>(entity);
        }
    }
}
