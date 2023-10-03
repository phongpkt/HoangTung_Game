using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : IState
{
    float randomTime;
    float timer;

    public void OnEnter(Enemy enemy)
    {
        timer = 0;
        randomTime = Random.Range(2f,3f);
        enemy.isChase = false;
        if (enemy.isRight)
        {
            enemy.ChangeDirection(!enemy.isRight);
        }
        else
        {
            enemy.ChangeDirection(!enemy.isRight);
        }
    }

    public void OnExecute(Enemy enemy)
    {
        timer += Time.deltaTime;
        if (enemy.Target != null)
        {
            enemy.ChangeState(new ChaseState());
        }
        else
        {
            if (timer < randomTime)
            {
                enemy.isMoving = true;
            }
            else
            {
                enemy.ChangeState(new IdleState());
            }
        }
    }

    public void OnExit(Enemy enemy)
    {

    }
}
