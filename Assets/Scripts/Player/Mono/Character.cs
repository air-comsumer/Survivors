using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] CharacterData characterData;//角色数据
    Sprite sprite;//角色精灵
    RuntimeAnimatorController controller;//角色动画控制器
    Animator animator;//角色动画组件
    float healthPoint;//生命值
    int attackPower;//攻击力
    int defencePower;//防御力
    int speed;//速度
    float maxHealth;//最大生命值
    float tempHealth;//临时生命值，用来给武器提前判断是否能击杀敌人
    internal Coroutine hitCoroutine;//受击协程
    internal SpriteRenderer spriteRenderer;//精灵渲染器

    protected virtual void Initialize()
    {
        healthPoint = characterData.GetHealthPoint();
        attackPower = characterData.GetAttackPower();
        defencePower = characterData.GetDefencePower();
        speed = characterData.GetSpeed();
        maxHealth = characterData.GetHealthPoint();
        sprite = characterData.GetSprite();
        controller = characterData.GetController();
        spriteRenderer = GetComponent<SpriteRenderer>();
        GetComponent<SpriteRenderer>().sprite = GetSprite();
        animator = GetComponent<Animator>();
        animator.runtimeAnimatorController = GetController();
        hitCoroutine = null;
    }
    protected void InitHealthPoint()
    {
        healthPoint = characterData.GetHealthPoint();
        tempHealth = healthPoint; // 初始化临时生命值
    }
    public float TempHealthPoint()
    {
        return tempHealth;
    }
    public float ReduceTempHealthPoint(float damage)
    {
        if (tempHealth <= damage)
        {
            tempHealth = 0;
            return 0; 
        }
        else
        {
            tempHealth -= damage;
            return tempHealth; // 返回剩余生命值
        }
    }
    public float GetHealthPoint()
    {
        return healthPoint;
    }
    public Sprite GetSprite()
    {
        return sprite;
    }
    public int GetAttackPower()
    {
        return attackPower;
    }

    public int GetDefencePower()
    {
        return defencePower;
    }

    public int GetSpeed()
    {
        return speed;
    }
    public RuntimeAnimatorController GetController()
    {
        return controller;
    }
    public Vector3 GetPosition()
    {
        return transform.position;
    }

    protected Animator GetAnimator()
    {
        return animator;
    }
    public virtual void ReduceHealthPoint(int damage)
    {
        if(healthPoint<=damage)
        {
            healthPoint = 0;
            Die();
        }
        else
        {
            healthPoint -= damage;
        }
    }
    public void RecoverHealthPoint(int amount)
    {
        if (healthPoint + amount > maxHealth)
        {
            healthPoint = maxHealth;
        }
        else
        {
            healthPoint += amount;
        }
    }
    public void IncreaseAttackPower(int value)
    {
        attackPower += value;
    }

    public void IncreaseDefencePower(int value)
    {
        defencePower += value;
    }

    public void IncreaseSpeed(int value)
    {
        speed += speed * value / 100;
    }
    public CharacterType GetCharacterType()
    {
        return characterData.GetCharacterType();
    }

    public abstract void Die();

    protected abstract IEnumerator DieAnimation();

    protected abstract IEnumerator UnderAttack();
}
