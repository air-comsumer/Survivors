using System.Collections;
using System.Collections.Generic;
using Unity.Transforms;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    static PlayerMove instance;
    Animator animator;
    SpriteRenderer spriteRenderer;
    Player character;
    float horizontal;
    float vertical;
    bool lookingLeft;
    public bool isDead;
    public Weapon weapon;
    public static PlayerMove Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerMove>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        character = GetComponent<Player>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isDead = false;
        lookingLeft = false;
        instance = this;
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        // Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // if (Input.GetMouseButtonDown(0))
        // {
        //     var direction = (Vector3)mousePos - weapon.transform.position; // ����Ŀ�귽��
        //      float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 45; // ������ת�Ƕ�
        //     weapon.GenerateBullet(angle);
        // }
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal) >= 0.7f && Mathf.Abs(vertical) >= 0.7f)//限制对角线
        {
            horizontal = Mathf.Clamp(horizontal, -0.7f, 0.7f);
            vertical = Mathf.Clamp(vertical, -0.7f, 0.7f);
        }
        if (horizontal != 0f || vertical != 0f)//翻转
        {
            animator.SetInteger("AnimState", 1);
            if (horizontal > 0f)
            {
                spriteRenderer.flipX = false;
                lookingLeft = false;
            }
            else if (horizontal < 0f)
            {
                spriteRenderer.flipX = true;
                lookingLeft = true;
            }
        }
        else
        {
            animator.SetInteger("AnimState", 0);
        }
        if (!isDead)
        {
            transform.Translate(Vector2.right * horizontal * character.GetSpeed() / 10f * Time.deltaTime);
            transform.Translate(Vector2.up * vertical * character.GetSpeed() / 10f * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y);
            SharedData.playerPos.Data = transform.position;
        }
    }
    public bool GetLookingLeft()
    {
        return lookingLeft;
    }
    public float GetHorizontal()
    {
        return horizontal;
    }
    public float GetVertical()
    {
        return vertical;
    }
}
