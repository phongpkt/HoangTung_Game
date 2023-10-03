using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    protected float attackRange;

    public bool isAttack;
    public bool isMoving;
    public bool isChase;
    protected bool isDead;
    public bool isRight = true;

    protected IState currentState;

    private Character target;
    public Character Target => target;
    [SerializeField] private Animator anim;
    protected string currentAnim;

    protected int maxHealth;
    [SerializeField] protected int currentHealth;
    private void Start()
    {
        OnInit();
    }
    public virtual void OnInit()
    {
        currentHealth = maxHealth;
        isDead = false;
    }
    public virtual void OnDespawn()
    {
        Destroy(gameObject, 1f);
    }
    public virtual void OnDeath()
    {
        ChangeAnim(Constants.ANIM_DIE);
        ChangeState(null);
        Invoke(nameof(OnDespawn), 1f);
    }
    public virtual void OnAttack() {}
    public virtual void OnHit(int damage)
    {
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            OnDeath();
        }
    }
    protected void ChangeAnim(string animName)
    {
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }
    private void Update()
    {
        if (currentState != null && !isDead)
        {
            currentState.OnExecute(this);
        }
    }
    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }

        currentState = newState;

        if (currentState != null)
        {
            currentState.OnEnter(this);
        }
    }
    public void ChangeDirection(bool isRight)
    {
        this.isRight = isRight;

        transform.rotation = isRight ? Quaternion.Euler(Vector3.zero) : Quaternion.Euler(Vector3.up * 180);
    }
    public bool IsTargetInRange()
    {
        if (target != null && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    internal void SetTarget(Character character)
    {
        this.target = character;
        if(Target != null)
        {
            ChangeState(new ChaseState());
        }
        else
        {
            ChangeState(new IdleState());

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.ENEMY_WALL))
        {
            ChangeDirection(!isRight);
        }
    }
}
