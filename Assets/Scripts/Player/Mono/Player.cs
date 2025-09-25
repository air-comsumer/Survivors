using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Physics;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Player : Character 
{
    static Player instance;
    public static Player Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Player>();
            }
            return instance;
        }
    }
    [SerializeField] Slider hpSlider;//����ֵ������
    [SerializeField] ParticleSystem bleeding;//��Ѫ��Ч
    [SerializeField] GameObject GameOverWindow;//��Ϸ��������
    float attackSpeed;
    float expAdditional;
    int luck;
    bool isColliding;
    [SerializeField] float detectionRadius = 5f; // ���˼��뾶
    [SerializeField] UnityEngine.Collider[] colliders = new UnityEngine.Collider[10]; // ���ڴ洢��⵽�ĵ���
    [SerializeField] int weaponCount = 1; // ��������,��������ÿ�μ����˵�����
    [Header("�����ײ������")]
    public Vector3 detectionSize = new Vector3(5f, 5f, 0.1f);
    public float spawnInterval;
    public int spawnCount;
    private Player() { }
    void Awake()
    {
        Initialize();
        //�����ʱдһЩ��ʼ���Ĵ��룬����Ѵ���ת�Ƶ�ϵͳ��д
        SharedData.gameSharedInfo.Data.spawnInterval = spawnInterval; // ���õ������ɼ��
        SharedData.gameSharedInfo.Data.spawnCount = spawnCount; // ����ÿ�����ɵ��˵�����
        SharedData.playerColliderInfo.Data.detectionSize = detectionSize;
        var enemyLayer = LayerMask.NameToLayer("Enemy");
        var enemyLayerMask = (uint)math.pow(2, enemyLayer);
        var attackCollisionFiltter = new CollisionFilter
        {
            BelongsTo = uint.MaxValue,
            CollidesWith = enemyLayerMask
        };
        SharedData.playerColliderInfo.Data.collisionFilter = attackCollisionFiltter;
    }
    private void Start()
    {
        WeaponLayout.Instance.ResetWeaponLayout();
    }
    private void Update()
    {
        SharedData.gameSharedInfo.Data.spawnCount = spawnCount;
        SharedData.playerColliderInfo.Data.detectionSize = detectionSize;
        //EnemyDetection();
    }
    public void EnemyDetection()
    {
        //Physics.OverlapSphereNonAlloc(transform.position, detectionRadius,colliders,1 << 3);
        //colliders = Physics.OverlapCircleAll(transform.position, detectionRadius,1<<3);
        Debug.Log(colliders.Length);
        for(int i = 0; i < colliders.Length; i++)
        {
            //if (colliders[i] == null || colliders[i].gameObject == null)
            //{
            //    continue; // ������Ч����ײ��
            //}
            Debug.Log(colliders[i].transform.position);
            //Debug.Log("��⵽����: " + colliders[i].gameObject.name);
        }
 
        if (colliders.Length > 0)
        {
            int[] a = new int[1];
            a[0] = 0;//Ϊ���ܹ������أ���Ϊд�õ�EventTriggerû���ܹ�ʹ��ref����չ��������һ�� int���������ݣ���֤���¼�ִ�е�ʱ���ܹ������޸����ֵ
            // ���������¼�,������Weapon����
            //EventCenter.Instance.EventTrigger<Collider[], int[]>("WeaponAttack", colliders,a);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, detectionSize*2);  // 绘制立方体
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,Vector3.forward);
    }
    protected override void Initialize()
    {
        colliders = new UnityEngine.Collider[weaponCount];//��һ�γ�ʼ��������������������ӵ�ʱ�����޸�
        base.Initialize();
        //GameOverWindow.SetActive(false);
        instance = this;
        attackSpeed = 100f;
        expAdditional = 100f;
        luck = 0;
        //hpSlider.maxValue = GetHealthPoint();
        //hpSlider.value = GetHealthPoint();
        isColliding = false;
        GetFirstWeapon();
    }
    public float GetAttackSpeed()
    {
        return attackSpeed;
    }

    public float GetExpAdditional()
    {
        return expAdditional;
    }

    public int GetLuck()
    {
        return luck;
    }

    public void DecreaseAttackSpeed(float value)
    {
        attackSpeed -= value;
    }

    public void IncreaseExpAdditional(float value)
    {
        expAdditional += value;
    }

    public void IncreaseLuck(int value)
    {
        luck += value;
    }

    public override void Die()
    {
        PlayerMove.Instance.isDead = true;
        StartCoroutine(DieAnimation());
    }

    protected override IEnumerator DieAnimation()
    {
        GetAnimator().SetBool("Death", true);
        yield return new WaitForSeconds(1f);
        GameOverWindow.SetActive(true);
        Time.timeScale = 0f; // ֹͣ��Ϸʱ��
    }
    public override void ReduceHealthPoint(int damage)
    {
        base.ReduceHealthPoint(damage);
        hpSlider.value = GetHealthPoint();
        bleeding.Play();
        isColliding = true;
        if(hitCoroutine==null)
            hitCoroutine = StartCoroutine(UnderAttack());
    }

    protected override IEnumerator UnderAttack()
    {
        spriteRenderer.color = Color.red;
        do
        {
            isColliding = false;
            yield return new WaitForSeconds(0.2f);
        }
        while(isColliding);
        spriteRenderer.color = Color.white;
        hitCoroutine = null;
    }

    void GetFirstWeapon()
    {
        switch(GetComponentInParent<Player>().GetCharacterType())
        {
            case CharacterType.Knight:
                //��������
                break;
            case CharacterType.Bandit:
                break;
        }
    }
}
