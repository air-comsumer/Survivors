using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] CharacterData characterData;//��ɫ����
    Sprite sprite;//��ɫ����
    RuntimeAnimatorController controller;//��ɫ����������
    Animator animator;//��ɫ�������
    float healthPoint;//����ֵ
    int attackPower;//������
    int defencePower;//������
    int speed;//�ٶ�
    float maxHealth;//�������ֵ
    float tempHealth;//��ʱ����ֵ��������������ǰ�ж��Ƿ��ܻ�ɱ����
    internal Coroutine hitCoroutine;//�ܻ�Э��
    internal SpriteRenderer spriteRenderer;//������Ⱦ��

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
        tempHealth = healthPoint; // ��ʼ����ʱ����ֵ
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
            return tempHealth; // ����ʣ������ֵ
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
