using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] public WeaponDataSO weaponData;
    public bool isHaveTarget = false; // �Ƿ���Ŀ��
    public bool isCold = false; //开始没有在冷却
    private float attackTimer = 0f; // ������ʱ��
    DynamicBuffer<WeaponTargetBuffer> buffer;
    private Vector3 tempVector;
    private void OnEnable()
    {
        attackTimer = weaponData.AttackSpeed; //初始化冷却倒计时
        //EventCenter.Instance.AddListener<Collider[], int[]>("WeaponAttack", Attack);//������д���
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        
        if(isCold)
        {
            if(attackTimer>0)//倒计时没结束
            {
                attackTimer -= Time.deltaTime;//倒计时
            }
            else
            {
                Debug.Log("冷却结束");
                isCold = false; //冷却结束
                attackTimer = weaponData.AttackSpeed; //重置倒计时时间
            }
        }
    }
    private void LateUpdate()
    {
        
    }
    //public void Attack(Collider[] colliders,int[] targetIndex)
    //{
    //    //��ת�Ƕ�
    //    if(isAttack) return; // ������ڹ�����ֱ�ӷ���
    //    if (targetIndex[0]<colliders.Length) isHaveTarget = true;
    //    else isHaveTarget = false;
    //    if (!isHaveTarget) return; // ���û��Ŀ�ֱ꣬�ӷ���
    //    //������ת����
    //    var direction = colliders[targetIndex[0]].transform.position - transform.position;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg -45;
    //    transform.rotation = Quaternion.AngleAxis(angle+180, Vector3.forward);
    //    Debug.Log("targetIndex: " + targetIndex[0] + " extralHealth: " + colliders[targetIndex[0]].gameObject.GetComponent<Character>().TempHealthPoint());
    //    if (colliders[targetIndex[0]].gameObject.GetComponent<Character>().TempHealthPoint() - weaponData.AttackPower > 0)
    //    {
    //        isAttack = true; // �������ڹ���״̬
    //        colliders[targetIndex[0]].gameObject.GetComponent<Character>().ReduceTempHealthPoint(weaponData.AttackPower);
    //        GenerateBullet(angle); // �����ӵ�
    //    }
    //    else
    //    {
    //        isAttack = true; // �������ڹ���״̬
    //        GenerateBullet(angle); // �����ӵ�
    //        targetIndex[0]++;
    //    }
    //}
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        //Quaternion rotation = Quaternion.AngleAxis(135, transform.forward);
        Gizmos.DrawLine(transform.position,tempVector);
    }
    public Quaternion RotateWeapon(float3 curEnemyPosition)
    {
        var vectorToClosestEnemy = curEnemyPosition - new float3(transform.position.x, transform.position.y, 0);
        tempVector = vectorToClosestEnemy;
        var angleToCloasestEnemy = Mathf.Atan2(vectorToClosestEnemy.y, vectorToClosestEnemy.x)*Mathf.Rad2Deg;
        var rotate = Quaternion.AngleAxis(angleToCloasestEnemy, Vector3.forward);
        var rotate2 = Quaternion.AngleAxis(135, Vector3.forward);
        var rotate3 = Quaternion.AngleAxis(180, Vector3.forward);
        transform.rotation = rotate*rotate2;// 
        return rotate3*rotate*rotate2;
    }
    public void GenerateBullet(Quaternion angle)
    {
        DynamicBuffer<BulletCreateInfo> buffer = World.DefaultGameObjectInjectionWorld.EntityManager.GetBuffer<BulletCreateInfo>(SharedData.singtonEntity.Data);
        buffer.Add(new BulletCreateInfo()
        {
            position = transform.position,
            rotation = angle
        });
        isCold = true; //攻击结束进入冷却
    }
}
