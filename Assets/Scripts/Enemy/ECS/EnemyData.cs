using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct EnemyData : IComponentData,IEnableableComponent
{
    public float health;
    public bool die;
}
