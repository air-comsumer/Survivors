using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponDataSO weaponData;
    private bool isHaveTarget = false; // 是否有目标

    private void OnEnable()
    {
        EventCenter.Instance.AddListener<Collider2D[], int[]>("WeaponAttack", Attack);
    }
    public void Attack(Collider2D[] colliders,int[] targetIndex)
    {
        //旋转角度
        if (targetIndex[0]<colliders.Length) isHaveTarget = true;
        else isHaveTarget = false;
        if (!isHaveTarget) return; // 如果没有目标，直接返回
        Vector2 direction = new Vector2(colliders[0].transform.position.x - transform.position.x, colliders[0].transform.position.y - transform.position.y);
        float angle = Vector2.Angle(Vector2.right, direction);
        transform.eulerAngles = new Vector3(0, 0, (direction.y > 0 ? angle : -angle)+120);
        Debug.Log("targetIndex: " + targetIndex[0] + " extralHealth: " + colliders[targetIndex[0]].gameObject.GetComponent<Character>().TempHealthPoint());
        if (colliders[targetIndex[0]].gameObject.GetComponent<Character>().TempHealthPoint()-weaponData.AttackPower>0)
        {
            colliders[targetIndex[0]].gameObject.GetComponent<Character>().ReduceTempHealthPoint(weaponData.AttackPower);
        }
        else
        {
            Destroy(colliders[targetIndex[0]].gameObject);
            targetIndex[0]++;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.forward);
    }

}
