using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct AttackTargetTag : IComponentData,IEnableableComponent
{
    public double lastAttackedTimer;//倒计时
    public bool isAttacked;
    public float health;
}
