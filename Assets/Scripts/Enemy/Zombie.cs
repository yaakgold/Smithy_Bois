using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    public float timeBetweenChanges;

    private void Awake()
    {
        //Create state machine
        _stateMachine = new StateMachine();

        //Create states
        WanderState wander = new WanderState(this, timeBetweenChanges);

        //Specific Transitions
        

        //Any Transitions
        

        //Set State to wander
        _stateMachine.SetState(wander);

        //Method shortucts for transitions
        void At(IState from, IState to, Func<bool> condition, bool transitionToSelf) => _stateMachine.AddTransition(from, to, condition, transitionToSelf);
        
    }

    public override void Update()
    {
        base.Update();

        //Movement stuff
        transform.position += (Vector3)moveDir * speed * Time.deltaTime;
    }
}
