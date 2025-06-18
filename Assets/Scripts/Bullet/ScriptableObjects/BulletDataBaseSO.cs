using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BulletDataBase", menuName = "ScriptableObjects/BulletDataSO", order = 1)]
public class BulletDataBaseSO : ScriptableObject
{
    [SerializeField] private Sprite bulletSprite; //子弹精灵
    [SerializeField] private float bulletSpeed; //子弹速度
    public Sprite BulletSprite => bulletSprite; //获取子弹精灵
    public float BulletSpeed => bulletSpeed; //获取子弹速度
}
