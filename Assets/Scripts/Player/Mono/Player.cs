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
    [SerializeField] Slider hpSlider;//生命值滑动条
    [SerializeField] ParticleSystem bleeding;//流血特效
    [SerializeField] GameObject GameOverWindow;//游戏结束窗口
    float attackSpeed;
    float expAdditional;
    int luck;
    bool isColliding;
    [SerializeField] float detectionRadius = 5f; // 敌人检测半径
    [SerializeField] Collider2D[] colliders; // 用于存储检测到的敌人
    [SerializeField] int weaponCount = 1; // 武器数量,用来控制每次检测敌人的数量
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
            // 触发攻击事件,监听在Weapon类中
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
        colliders = new Collider2D[weaponCount];//第一次初始化，后面可以在武器增加的时候再修改
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
        Time.timeScale = 0f; // 停止游戏时间
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
                //添加武器
                break;
            case CharacterType.Bandit:
                break;
        }
    }
}
