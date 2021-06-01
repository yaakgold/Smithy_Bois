using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderState : IState
{
    public Vector2 movDir = Vector2.zero;
    public Enemy enemy;
    public float timeSinceLastChange, timeBetweenChanges;

    public WanderState(Enemy _enemy, float _time)
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
        timeSinceLastChange -= Time.deltaTime;
        
        if(timeSinceLastChange <= 0)
        {
            timeSinceLastChange = timeBetweenChanges;
            CalculateNewDir();
        }
        
        enemy.moveDir = movDir;
    }

    private void CalculateNewDir()
    {
        movDir.x = Random.Range(-1.0f, 1.0f);
        movDir.y = Random.Range(-1.0f, 1.0f);
        movDir.Normalize();
    }
}
