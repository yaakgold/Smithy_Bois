using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayerState : IState
{
    public Enemy enemy;
    public Player player;
    public float timeBetweenAttacks;
    public float timeSinceLastAttack = 0;
    public float strength;

    public AttackPlayerState(Enemy _enemy, float _timeBetween, float _strength)
    {
        enemy = _enemy;
        timeBetweenAttacks = _timeBetween;
        strength = _strength;
    }

    public void OnEnter()
    {
        player = enemy.target.GetComponent<Player>();
    }

    public void OnExit()
    {
        
    }

    public void Tick()
    {
        timeSinceLastAttack -= Time.deltaTime;

        if(timeSinceLastAttack <= 0)
        {
            Attack();
            timeSinceLastAttack = timeBetweenAttacks;
        }
    }

    public void Attack()
    {
        Debug.Log("ATTACK!");
        //Animation here
        enemy.anim.SetTrigger("Attack");

        //Animation takes care of the actual damage
    }
}
