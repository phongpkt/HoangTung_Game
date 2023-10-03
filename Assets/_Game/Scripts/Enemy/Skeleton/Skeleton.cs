using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField] private SpriteRenderer sprites;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float chaseSpeed;
    [SerializeField] private Rigidbody2D rb;
    public override void OnInit()
    {
        maxHealth = 100;
        moveSpeed = 2;
        chaseSpeed = 4;
        attackRange = 2;
        ChangeState(new IdleState());
        base.OnInit();
    }
    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }
        if (isMoving)
        {
            ChangeAnim(Constants.ANIM_PATROL);
            rb.velocity = transform.right * moveSpeed;
        }
        if (isChase)
        {
            ChangeAnim(Constants.ANIM_RUN);
            rb.velocity = transform.right * chaseSpeed;
        }
        else if (!isAttack && !isChase && !isMoving)
        {
            ChangeAnim(Constants.ANIM_IDLE);
            rb.velocity = Vector2.zero;
        }
    }
    public override void OnDeath()
    {
        rb.velocity = Vector2.zero;
        base.OnDeath();
    }

    public override void OnHit(int damage)
    {
        base.OnHit(damage);
        sprites.color = Color.red;
        Invoke(nameof(ResetColor), 0.5f);
    }
    private void ResetColor()
    {
        sprites.color = Color.white;
    }
    public override void OnAttack()
    {
        rb.velocity = Vector2.zero;
        ChangeAnim(Constants.ANIM_ATTACK);
        ActiveAttack();
        Invoke(nameof(DeactiveAttack), 0.5f);
    }
    private void ActiveAttack()
    {
        isAttack = true;
    }
    private void DeactiveAttack()
    {
        isAttack = false;
    }

}
