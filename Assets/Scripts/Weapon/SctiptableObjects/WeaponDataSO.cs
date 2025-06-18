using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponDataSO", order = 1)]
public class WeaponDataSO : ScriptableObject
{
    //�������������ݣ�ʣ�µ��������ݿ�����Weapon��������
    [SerializeField] private Sprite weaponSprite;
    [SerializeField] private BulletDataBaseSO bulletData; //�ӵ�����
    [SerializeField] private float attackPower;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackDistance;
    [SerializeField] private WeaponType weaponType;
    [SerializeField] private damageType damageType;

    public Sprite WeaponSprite => weaponSprite;
    public BulletDataBaseSO BulletData => bulletData; //��ȡ�ӵ�����
    public float AttackPower => attackPower;
    public float AttackSpeed => attackSpeed;
    public float AttackDistance => attackDistance;
    public WeaponType WeaponType => weaponType;
    public damageType DamageType => damageType;

}

