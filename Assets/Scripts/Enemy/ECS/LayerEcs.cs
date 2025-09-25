using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct LayerEcs : IComponentData
{
    public int Value;  // 用于存储层级值，例如 3
}
