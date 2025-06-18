using System.Collections;
using System.Collections.Generic;
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
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (Mathf.Abs(horizontal) >= 0.7f && Mathf.Abs(vertical) >= 0.7f)
        {
            horizontal = Mathf.Clamp(horizontal, -0.7f, 0.7f);
            vertical = Mathf.Clamp(vertical, -0.7f, 0.7f);
        }
        if (horizontal != 0f || vertical != 0f)
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
