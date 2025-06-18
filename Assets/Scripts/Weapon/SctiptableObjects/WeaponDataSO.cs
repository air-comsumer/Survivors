using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponDataSO", order = 1)]
public class WeaponDataSO : ScriptableObject
{
    //基本的武器数据，剩下的特殊数据可以在Weapon类中设置
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private BulletDataBaseSO bulletData; //子弹数据
    [SerializeField] private float attackPower;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDistance;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private damageType damageType;

    public Sprite WeaponSprite => weaponSprite;
    public BulletDataBaseSO BulletData => bulletData; //获取子弹数据
    public float AttackPower => attackPower;
    public float AttackSpeed => attackSpeed;
    public float AttackDistance => attackDistance;
    public WeaponType WeaponType => weaponType;
    public damageType DamageType => damageType;

}

