using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] Collider2D[] colliders; // ���ڴ洢��⵽�ĵ���
    [SerializeField] int weaponCount = 1; // ��������,��������ÿ�μ����˵�����
    private Player() { }
    void Awake()
    {
        Initialize();
    }
    private void Update()
    {
        EnemyDetection();
    }
    public void EnemyDetection()
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius,1<<3);
        if (colliders.Length > 0)
        {
            Debug.Log(colliders[0].gameObject.GetComponent<Character>().GetHealthPoint());
            int[] a = new int[1];
            a[0] = 0;
            // ���������¼�,������Weapon����
            EventCenter.Instance.EventTrigger<Collider2D[], int[]>("WeaponAttack", colliders,a);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y,transform.position.z+10));
    }
    protected override void Initialize()
    {
        colliders = new Collider2D[weaponCount];//��һ�γ�ʼ��������������������ӵ�ʱ�����޸�
        base.Initialize();
        //GameOverWindow.SetActive(false);
        instance = this;
        attackSpeed = 100f;
        expAdditional = 100f;
        luck = 0;
        hpSlider.maxValue = GetHealthPoint();
        hpSlider.value = GetHealthPoint();
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
                //�������
                break;
            case CharacterType.Bandit:
                break;
        }
    }
}
