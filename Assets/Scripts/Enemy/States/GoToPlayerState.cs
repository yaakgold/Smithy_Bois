using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPlayerState : IState
{
    public Vector2 movDir = Vector2.zero;
    public Enemy enemy;
    public float timeSinceLastChange, timeBetweenChanges;

    public GoToPlayerState(Enemy _enemy, float _time)
    {
        enemy = _enemy;
        timeBetweenChanges = timeSinceLastChange = _time;
    }

    public void OnEnter()
    {
        
    }

    public void OnExit()
    {
        enemy.moveDir = Vector2.zero;
    }

    public void Tick()
    {
        //timeSinceLastChange -= Time.deltaTime;

        //if(timeSinceLastChange <= 0)
        //{
        //    timeSinceLastChange = timeBetweenChanges;
        //    CalculateNewDir();
        //}

        CalculateNewDir();

        enemy.moveDir = movDir;
    }

    private void CalculateNewDir()
    {
        movDir = enemy.target.position - enemy.transform.position;
        movDir.Normalize();
    }
}
