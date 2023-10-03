using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    protected string currentAnim;
    protected bool isDead;
    public int maxHealth = 100;
    public int currentHealth;
    //public CharacterInfo characterUI;
    private void Start()
    {
        OnInit();
    }
    private void Update()
    {

    }
    public virtual void OnInit()
    {
        currentHealth = maxHealth;
        //characterUI.SetMaxHealth(maxHealth);
    }
    public virtual void OnDespawn()
    {
        //gameObject.SetActive(false);
        //Invoke(nameof(OnInit), 1f);
    }
    protected virtual void OnDeath()
    {
        ChangeAnim("die");
        //Invoke(nameof(OnDespawn), 1f);
    }
    public virtual void OnHit(int damage)
    {
        //Debug.Log("Enemy has taken: " + damage);
        currentHealth -= damage;
        if(currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            OnDeath();
        }
        // characterUI.SetHealth(currentHealth);
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
}
