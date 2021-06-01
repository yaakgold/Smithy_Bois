using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public float timeBetweenChanges;
    public Health health;

    private void Awake()
    {
        //Create state machine
        _stateMachine = new StateMachine();

        //Create states
        WanderState wander = new WanderState(this, timeBetweenChanges);
        GoToPlayerState goToPlayerState = new GoToPlayerState(this, timeBetweenChanges);

        //Specific Transitions
        At(wander, goToPlayerState, checkPlayerInRange(), false);
        At(goToPlayerState, wander, playerOutOfRange(), false);

        //Any Transitions
        

        //Set State to wander
        _stateMachine.SetState(wander);

        //Method shortucts for transitions
        void At(IState from, IState to, Func<bool> condition, bool transitionToSelf) => _stateMachine.AddTransition(from, to, condition, transitionToSelf);
        Func<bool> checkPlayerInRange() => () =>
        {
            var hits = Physics2D.CircleCastAll(transform.position, detectionRange, Vector2.one).Where(x => x.transform.CompareTag("Player")).ToList();

            if (hits.Count > 0 && hits[0].transform.gameObject.activeSelf == true)
                target = hits[0].transform;
            else
                target = null;
            return target != null;
        };

        Func<bool> playerOutOfRange() => () =>
        {
            return checkPlayerInRange().Invoke() == false;
        };
    }

    private void Start()
    {
        health.OnDeath.AddListener(Die);
    }

    public override void Update()
    {
        base.Update();

        //Movement stuff
        transform.position += (Vector3)moveDir * speed * Time.deltaTime;

        health.TakeDamage(Time.deltaTime);
    }

    public void Die()
    {
        //TODO: Do more
        Destroy(gameObject);
    }
}
