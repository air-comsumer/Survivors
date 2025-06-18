using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

public class WeaponLayout : MonoBehaviour
{
    public float radius = 0.2f; // �������ֵİ뾶
    [ContextMenu("ResetWeaponLayout")]
    private void ResetWeaponLayout()
    {
        // ������������
        Debug.Log(transform.childCount);
        int weaponIndex = 0;
        float angleStep = 360f / transform.childCount; // ÿ������֮��ĽǶȼ��
        float startAngle = angleStep/2; // ��ʼ�Ƕ�
        foreach (Transform child in transform)
        {
            float currentAngle = (startAngle + weaponIndex * angleStep)*Mathf.Deg2Rad;
            float x = radius * Mathf.Sin(currentAngle);
            float y = radius * Mathf.Cos(currentAngle);
            Debug.Log(currentAngle+" "+x+" "+y);
            weaponIndex++;
            child.position = new Vector3(transform.position.x + x,transform.position.y-y,transform.position.z);
        }
    }
}
