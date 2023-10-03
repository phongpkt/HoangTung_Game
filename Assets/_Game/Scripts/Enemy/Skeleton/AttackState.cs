using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    float timer;
    public void OnEnter(Enemy enemy)
    {
        if(enemy.Target != null)
        {
            enemy.isMoving = false;
            enemy.isChase = false;
            enemy.isAttack = true;
            enemy.OnAttack();
        }
        else
        {
            enemy.ChangeState(new PatrolState());
        }
        timer = 0;
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (timer >= 1.5f)
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExit(Enemy enemy)
    {
        
    }
}
