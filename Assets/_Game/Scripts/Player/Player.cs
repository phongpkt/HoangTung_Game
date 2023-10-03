using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] Rigidbody2D rb;
    private float speed;
    [SerializeField] private float horizontal;
    private float jumpForce;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isAttack;
    [SerializeField] private Kunai KunaiProjectile;
    [SerializeField] private Transform firePosition;
    private bool doubleJump;
    private float cooldown = 0.5f; //TODO: sua lai cooldown cho attackcombo cham hon 1
    private int maxCombo = 3;
    private int comboAttack = 0;
    private float attackTimer;

    private int kunaiCarry;
    public override void OnInit()
    {
        speed = 200;
        jumpForce = 300;
        kunaiCarry = 10;
        base.OnInit();
    }
    void Update()
    {
        // -1 < 0 < 1
        horizontal = Input.GetAxisRaw("Horizontal");
        isGrounded = CheckGround();
        
        if (isGrounded)
        {
            doubleJump = false;
            if (isJumping)
            {
                return;
            }
            //Jump
            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
                return;
            }
            //Move
            if (Mathf.Abs(horizontal) > 0.1f)
            {
                Run();
                return;
            }
            //attack
            if (Input.GetKeyDown(KeyCode.J))
            {
                rb.velocity = Vector2.zero;
                NormalCombo();
                return;
            }
            //UltimateAttack
            if (Input.GetKeyDown(KeyCode.K))
            {
                rb.velocity = Vector2.zero;
                UltiCombo();
                return;
            }
            //Shoot
            if (Input.GetKeyDown(KeyCode.I))
            {
                rb.velocity = Vector2.zero;
                Shoot();
                return;
            }
            if (isAttack)
            {
                if ((Time.time - attackTimer) > cooldown)
                {
                    isAttack = false;
                    comboAttack = 0;
                }
                return;
            }
        }
        else
        {
            if (!doubleJump && Input.GetKeyDown(KeyCode.W))
            {
                doubleJump = true;
                DoubleJump();
            }
            //Falling
            if(rb.velocity.y < 0)
            {   
                Fall();
            }
        }
    }
    private void FixedUpdate()
    {
        if (isDead) return;
        //Move on ground
        if (Mathf.Abs(horizontal) > 0.1f)
        {
            rb.velocity = new Vector2(horizontal * speed * Time.fixedDeltaTime, rb.velocity.y);
            transform.rotation = Quaternion.Euler(new Vector3(0, horizontal > 0 ? 0 : 180, 0));
        }
        else if (isGrounded && !isJumping && !isAttack)
        {
            Idle();
        }
    }

    //=============Idle=================
    #region Idle
    private void Idle()
    {
        ChangeAnim(Constants.ANIM_IDLE);
        rb.velocity = Vector2.up * rb.velocity.y;
    }
    #endregion
    //=============Run=================
    #region Run
    private void Run()
    {
        ChangeAnim(Constants.ANIM_RUN);
    }
    #endregion
    //=============Jump=================
    #region Jump
    private void Jump()
    {
        isGrounded = false;
        isJumping = true;
        ChangeAnim(Constants.ANIM_JUMP);
        rb.AddForce(jumpForce * Vector2.up);
    }
    #endregion
    //===========DoubleJump==============
    private void DoubleJump()
    {
        isGrounded = false;
        isJumping = true;
        ChangeAnim(Constants.ANIM_SOMERSAULT);
        rb.AddForce(jumpForce * Vector2.up);
    }
    //=============Fall=================
    #region Fall
    private void Fall()
    {
        isJumping = false;
        ChangeAnim(Constants.ANIM_FALL);
    }
    #endregion
    //=============CheckGround=================
    private bool CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 1.2f, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.2f, groundLayer);
        return hit.collider != null;
    }

    //=============Attack=================
    private void NormalAttack(int combo)
    {
        ChangeAnim(Constants.ANIM_ATTACK + combo);
    }
    private void NormalCombo()
    {
        isAttack = true;
        if (attackTimer < Time.time && comboAttack < maxCombo)
        {
            comboAttack++;
            attackTimer = Time.time + cooldown;
        }
        switch (comboAttack)
        {
            case 1:
                NormalAttack(comboAttack);
                break;
            case 2:
                NormalAttack(comboAttack);
                break;
            case 3:
                NormalAttack(comboAttack);
                break;
        }
    }
    //=============Ultimate=================
    private void UltimateAttack(int combo)
    {
        ChangeAnim(Constants.ANIM_ULTI + combo);
    }
    private void UltiCombo()
    {
        isAttack = true;
        if (attackTimer < Time.time && comboAttack < maxCombo)
        {
            comboAttack++;
            attackTimer = Time.time + cooldown;
        }
        switch (comboAttack)
        {
            case 1:
                UltimateAttack(comboAttack);
                break;
            case 2:
                UltimateAttack(comboAttack);
                break;
            case 3:
                UltimateAttack(comboAttack);
                break;
        }
    }
    //=============Shoot=================
    private void Shoot()
    {
        isAttack = true;
        if (attackTimer < Time.time)
        {
            attackTimer = Time.time + cooldown + 1f;
        }
        ChangeAnim(Constants.ANIM_SHOOT);

        if(kunaiCarry > 0)
        {
            Instantiate(KunaiProjectile, firePosition.position, firePosition.rotation);
            kunaiCarry -= 1;
        }
    }
}
