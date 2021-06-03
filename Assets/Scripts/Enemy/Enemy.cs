using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public string currentState;
    public float speed;
    public float strength;
    public float detectionRange;
    public float attackRange;
    public float timeBetweenAttacks;
    public Vector2 moveDir;
    public Transform target;
    public Animator anim;

    protected StateMachine _stateMachine;

    public float timeBetweenChanges;
    public Health health;
    public Room room;

    private bool deathAdded;

    private void Awake()
    {
        //Create state machine
        _stateMachine = new StateMachine();

        //Create states
        WanderState wander = new WanderState(this, timeBetweenChanges);
        GoToPlayerState goToPlayerState = new GoToPlayerState(this, timeBetweenChanges);
        AttackPlayerState attackPlayerState = new AttackPlayerState(this, timeBetweenAttacks, strength);

        //Specific Transitions
        At(wander, goToPlayerState, checkPlayerInRange(), false);
        At(goToPlayerState, wander, playerOutOfRange(), false);
        At(goToPlayerState, attackPlayerState, closeEnoughToAttack(), false);
        At(attackPlayerState, goToPlayerState, notCloseEnoughToAttack(), false);

        //Any Transitions
        _stateMachine.AddAnyTransition(wander, () => target == null, false);

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

        Func<bool> closeEnoughToAttack() => () =>
        {
            if (target == null)
                return false;
            return Vector2.Distance(target.position, transform.position) < attackRange;
        };

        Func<bool> notCloseEnoughToAttack() => () =>
        {
            return closeEnoughToAttack().Invoke() == false;
        };
    }

    private void Start()
    {
        
    }

    public void Update()
    {
        if(!deathAdded)
        {
            health.OnDeath.AddListener(Die);
            deathAdded = true;
        }

        _stateMachine.Tick();

        currentState = _stateMachine._currentState.ToString();

        //Movement stuff
        transform.position += (Vector3)moveDir * speed * Time.deltaTime;

        //Animation stuff
        anim.SetFloat("MoveX", moveDir.x);
        anim.SetFloat("MoveY", moveDir.y);
    }

    public void Die()
    {
        //TODO: Do more

        //Remove the gameobject from the list in the room
        room.enemiesInRoom.Remove(gameObject);

        //Remove the gameobject from existence
        Destroy(gameObject);
    }
}
