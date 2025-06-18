using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public struct GameConfigData : IComponentData
{
    public Entity bulletPortotype;
    public Entity enemyPortotype;
}