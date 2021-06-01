using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public string currentState;
    public float speed;
    public float detectionRange;
    public Vector2 moveDir;
    public Transform target;

    protected StateMachine _stateMachine;

    public virtual void Update()
    {
        _stateMachine.Tick();

        currentState = _stateMachine._currentState.ToString();
    }
}
