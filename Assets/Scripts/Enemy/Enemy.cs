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
    public GameObject projectile;
    public float fireSpeed;
    public string enemyName;

    public bool isBoss;
    public GameObject particleAttack;
    public Transform particleLocation;
    public List<GameObject> coins;

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

    public void Update()
    {
        if(!deathAdded)
        {
            health.OnDeath.AddListener(Die);
            health.OnHit.AddListener(GetHit);
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

    public void GetHit()
    {
        AudioManager.Instance.Play($"{enemyName} Get Hit");
        anim.SetTrigger("GetHit");
    }

    public void Die()
    {
        //TODO: Do more

        //Remove the gameobject from the list in the room
        room.enemiesInRoom.Remove(gameObject);

        //Play death audio
        AudioManager.Instance.Play($"{enemyName} Die");

        //If is boss, do end game, win
        if(isBoss)
        {
            //TODO: Something for when the player wins
            
            //automatically give player X number of coins
        }
        else
        {
            //If not boss, drop a coin
            Instantiate(coins[UnityEngine.Random.Range(1, coins.Count)], transform.position, Quaternion.identity);
        }

        //Remove the gameobject from existence
        Destroy(gameObject);
    }

    public void TryToDoDamage()
    {
        //Do damage (maybe do a check thing to make sure you hit the player?
        target.GetComponent<Player>().health.TakeDamage(strength);
        AudioManager.Instance.Play($"{enemyName} Attack");
    }

    public void RobotThrow()
    {
        if(target)
        {
            Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>().Fire(fireSpeed, target.position, strength, target.tag, tag);
            AudioManager.Instance.Play($"{enemyName} Attack");
        }
    }

    public void CreateParticles()
    {
        Destroy(Instantiate(particleAttack, particleLocation.position, particleAttack.transform.rotation), 2);
    }
}
