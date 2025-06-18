using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ArbalestData", menuName = "ScriptableObjects/WeaponDataSO/ArbalestData", order = 1)]
public class ArbalestDataSO : WeaponDataSO
{
    //弩的特殊数据
    [SerializeField] private float chargeTime; // 弩的蓄力时间
    [SerializeField] private float penertrationForce; // 弩的穿透力
    public float ChargeTime => chargeTime; // 获取弩的蓄力时间
    public float PenertrationForce => penertrationForce; // 获取弩的穿透力
}
