using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float speed;
    public Vector2 moveDir;

    protected StateMachine _stateMachine;

    public virtual void Update()
    {
        _stateMachine.Tick();
    }
}
