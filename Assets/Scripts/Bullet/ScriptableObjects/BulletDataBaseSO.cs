using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BulletDataBase", menuName = "ScriptableObjects/BulletDataSO", order = 1)]
public class BulletDataBaseSO : ScriptableObject
{
    [SerializeField] private Sprite bulletSprite; //�ӵ�����
    [SerializeField] private float bulletSpeed; //�ӵ��ٶ�
    public Sprite BulletSprite => bulletSprite; //��ȡ�ӵ�����
    public float BulletSpeed => bulletSpeed; //��ȡ�ӵ��ٶ�
}
