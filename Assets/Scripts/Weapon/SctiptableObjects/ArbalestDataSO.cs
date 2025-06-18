using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ArbalestData", menuName = "ScriptableObjects/WeaponDataSO/ArbalestData", order = 1)]
public class ArbalestDataSO : WeaponDataSO
{
    //�����������
    [SerializeField] private float chargeTime; // �������ʱ��
    [SerializeField] private float penertrationForce; // ��Ĵ�͸��
    public float ChargeTime => chargeTime; // ��ȡ�������ʱ��
    public float PenertrationForce => penertrationForce; // ��ȡ��Ĵ�͸��
}
