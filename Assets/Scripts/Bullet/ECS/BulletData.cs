using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct BulletData : IComponentData,IEnableableComponent
{
    public float destroyTime;
}
