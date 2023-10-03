using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : IState
{
    public void OnEnter(Enemy enemy)
    {
        if (enemy.Target != null)
        {
            enemy.ChangeDirection(enemy.Target.transform.position.x > enemy.transform.position.x);
            enemy.isMoving = false;
            enemy.isChase = true;
        }
        else
        {
            enemy.ChangeState(new PatrolState());
        }
    }

    public void OnExecute(Enemy enemy)
    {
        if(enemy.Target != null)
        {
            if (enemy.IsTargetInRange())
            {
                enemy.ChangeState(new AttackState());
            }
        }
        else
        {
            enemy.ChangeState(new IdleState());
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
