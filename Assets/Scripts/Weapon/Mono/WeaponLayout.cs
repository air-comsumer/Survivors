using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

public class WeaponLayout : MonoBehaviour
{
    public float radius = 0.2f; // 武器布局的半径
    [ContextMenu("ResetWeaponLayout")]
    private void ResetWeaponLayout()
    {
        // 重置武器布局
        Debug.Log(transform.childCount);
        int weaponIndex = 0;
        float angleStep = 360f / transform.childCount; // 每个武器之间的角度间隔
        float startAngle = angleStep/2; // 起始角度
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
