using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDataCenter : SingletonMono<GameDataCenter>
{
    public int a;
    public List<WeaponDataSO> allWeaponData;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        a = 10;
    }
}
