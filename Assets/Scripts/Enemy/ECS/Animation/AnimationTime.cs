using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Rendering;
using UnityEngine;
[MaterialProperty("_AnimationTime")]
public struct AnimationTime : IComponentData,IEnableableComponent
{
    public float value;
}
