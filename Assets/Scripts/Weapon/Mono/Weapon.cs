using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;
    private bool isHaveTarget = false; // 是否有目标
    private bool isAttack = false; // 是否正在攻击
    private float attackTimer = 0f; // 攻击计时器
    public Transform target;
    private void OnEnable()
    {
        attackTimer = weaponData.AttackSpeed; // 初始化攻击计时器
        EventCenter.Instance.AddListener<Collider2D[], int[]>("WeaponAttack", Attack);
    }
    private void Update()
    {
        if(isAttack)
        {
            if(attackTimer>0)
            {
                attackTimer -= Time.deltaTime; // 减少攻击计时器
            }
            else
            {
                isAttack = false; // 攻击结束
                attackTimer = weaponData.AttackSpeed; // 重置攻击计时器
            }
        }
    }
    public void Attack(Collider2D[] colliders,int[] targetIndex)
    {
        //旋转角度
        if(isAttack) return; // 如果正在攻击，直接返回
        if (targetIndex[0]<colliders.Length) isHaveTarget = true;
        else isHaveTarget = false;
        if (!isHaveTarget) return; // 如果没有目标，直接返回
        //武器旋转方向
        var direction = colliders[targetIndex[0]].transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -45;
        transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
        Debug.Log("targetIndex: " + targetIndex[0] + " extralHealth: " + colliders[targetIndex[0]].gameObject.GetComponent<Character>().TempHealthPoint());
        if (colliders[targetIndex[0]].gameObject.GetComponent<Character>().TempHealthPoint() - weaponData.AttackPower > 0)
        {
            isAttack = true; // 设置正在攻击状态
            colliders[targetIndex[0]].gameObject.GetComponent<Character>().ReduceTempHealthPoint(weaponData.AttackPower);
            GenerateBullet(angle); // 生成子弹
        }
        else
        {
            isAttack = true; // 设置正在攻击状态
            GenerateBullet(angle); // 生成子弹
            targetIndex[0]++;
        }
        //生成子弹

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, target.position);
    }
    public void GenerateBullet(float angle)
    {
        DynamicBuffer<BulletCreateInfo> buffer = World.DefaultGameObjectInjectionWorld.EntityManager.GetBuffer<BulletCreateInfo>(SharedData.singtonEntity.Data);
        buffer.Add(new BulletCreateInfo()
        {
            position = transform.position,
            rotation = Quaternion.AngleAxis(angle, Vector3.forward)
        });
    }
}
