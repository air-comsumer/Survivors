using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;
    private bool isHaveTarget = false; // �Ƿ���Ŀ��
    private bool isAttack = false; // �Ƿ����ڹ���
    private float attackTimer = 0f; // ������ʱ��
    public Transform target;
    private void OnEnable()
    {
        attackTimer = weaponData.AttackSpeed; // ��ʼ��������ʱ��
        EventCenter.Instance.AddListener<Collider2D[], int[]>("WeaponAttack", Attack);
    }
    private void Update()
    {
        if(isAttack)
        {
            if(attackTimer>0)
            {
                attackTimer -= Time.deltaTime; // ���ٹ�����ʱ��
            }
            else
            {
                isAttack = false; // ��������
                attackTimer = weaponData.AttackSpeed; // ���ù�����ʱ��
            }
        }
    }
    public void Attack(Collider2D[] colliders,int[] targetIndex)
    {
        //��ת�Ƕ�
        if(isAttack) return; // ������ڹ�����ֱ�ӷ���
        if (targetIndex[0]<colliders.Length) isHaveTarget = true;
        else isHaveTarget = false;
        if (!isHaveTarget) return; // ���û��Ŀ�ֱ꣬�ӷ���
        //������ת����
        var direction = colliders[targetIndex[0]].transform.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -45;
        transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
        Debug.Log("targetIndex: " + targetIndex[0] + " extralHealth: " + colliders[targetIndex[0]].gameObject.GetComponent<Character>().TempHealthPoint());
        if (colliders[targetIndex[0]].gameObject.GetComponent<Character>().TempHealthPoint() - weaponData.AttackPower > 0)
        {
            isAttack = true; // �������ڹ���״̬
            colliders[targetIndex[0]].gameObject.GetComponent<Character>().ReduceTempHealthPoint(weaponData.AttackPower);
            GenerateBullet(angle); // �����ӵ�
        }
        else
        {
            isAttack = true; // �������ڹ���״̬
            GenerateBullet(angle); // �����ӵ�
            targetIndex[0]++;
        }
        //�����ӵ�

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
