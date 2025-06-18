using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    private void Awake()
    {
        InitHealthPoint();
    }
    public override void Die()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator DieAnimation()
    {
        throw new System.NotImplementedException();
    }

    protected override IEnumerator UnderAttack()
    {
        throw new System.NotImplementedException();
    }
    
}
