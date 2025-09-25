using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using UnityEngine;
using XLua;
public class WeaponLayout : MonoBehaviour
{
    private static WeaponLayout instance;
    public static WeaponLayout Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WeaponLayout>();
            }
            return instance;
        }
    }
    void OnEnable()
    {
        EventCenter.Instance.AddListener("ResetLayout", ResetWeaponLayout);
    }
    void OnDisable()
    {
        EventCenter.Instance.RemoveListener("ResetLayout", ResetWeaponLayout);
    }
    public float radius = 0.2f; // �������ֵİ뾶
    public List<Weapon> weapons; // �����б�
    public float health = 100; //��ǰĿ�������ֵ

    public void ResetWeaponLayout()
    {
        // ������������
        weapons.Clear();
        int weaponIndex = 0;
        float angleStep = 360f / transform.childCount; // ÿ������֮��ĽǶȼ��
        float startAngle = angleStep/2; // ��ʼ�Ƕ�
        foreach (Transform child in transform)
        {
            float currentAngle = (startAngle + weaponIndex * angleStep)*Mathf.Deg2Rad;
            float x = radius * Mathf.Sin(currentAngle);
            float y = radius * Mathf.Cos(currentAngle);
            weaponIndex++;
            child.position = new Vector3(transform.position.x + x,transform.position.y-y,transform.position.z);
            weapons.Add(child.GetComponent<Weapon>());
        }
    }
    public void AddWeapon() //GameDataCenter中用字典存储名字为键
    {
        Debug.Log("添加武器");
        EventCenter.Instance.EventTrigger("AddWeapon");
        EventCenter.Instance.EventTrigger("ResetLayout");
    }

    private void Update()
    {
        //Attack();
    }
    // private void Attack()
    // {
    //     DynamicBuffer<WeaponTargetBuffer> buffer = World.DefaultGameObjectInjectionWorld.EntityManager.GetBuffer<WeaponTargetBuffer>(SharedData.singtonEntity2.Data);
    //     int targetIndex = 0; // Ŀ������
    //     if(buffer.Length == 0) return; // ���û��Ŀ�ֱ꣬�ӷ���
    //     foreach (var weapon in weapons)
    //     {
    //         if(weapon.isAttack) continue; // ����������ڹ���������
    //         var direction = (Vector3)buffer[targetIndex].position - weapon.transform.position; // ����Ŀ�귽��
    //         float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 45; // ������ת�Ƕ�
    //         weapon.transform.rotation = Quaternion.AngleAxis(angle + 180, Vector3.forward);
    //         if(health <= weapon.weaponData.AttackPower)
    //         {
    //             weapon.isAttack = true; // �����������ڹ���״̬
    //             weapon.GenerateBullet(angle); // �����ӵ�
    //             targetIndex++;
    //             health = 100;
    //         }
    //         else
    //         {
    //             weapon.isAttack = true; // �����������ڹ���״̬
    //             health -= weapon.weaponData.AttackPower;
    //             weapon.GenerateBullet(angle); // �����ӵ�
    //         }
    //     }
    //     buffer.Clear(); // ���Ŀ�껺����
    // }

}
