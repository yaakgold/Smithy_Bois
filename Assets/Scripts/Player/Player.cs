using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Health health;
    public GameObject drill, flamethrower, pickaxe, sword;
    public Projectile projectile;
    public Transform attackPoint;
    public bool weaponActive;
    public Weapon weapon = new Weapon();
    private int _coins;
    public int Coins { get { return _coins;  } set { _coins = value; coinsTxt.text = $"Coins: {_coins}"; } }

    public float Speed { get; set; } = 5.0f;
    public float Strength { get; set; } = 1.0f;

    private float attackReach = 2.0f;
    private float attackSpeed = 2.0f;
    private float timeSinceLastAttack = 0;
    private Vector3 playerDirection;

    public Slider attackRechargeBar;
    public Slider healthBar;
    public TMP_Text coinsTxt;

    void Start()
    {
        health.OnDeath.AddListener(Die);
        health.OnHit.AddListener(OnHit);

        if(drill)
            drill.SetActive(false);

        if(flamethrower)
            flamethrower.SetActive(false);

        if(pickaxe)
            pickaxe.SetActive(false);

        if(sword)
            sword.SetActive(false);

        weaponActive = false;
    }

    void Update()
    {
        playerDirection = Vector3.zero;

        attackRechargeBar.value = timeSinceLastAttack / attackSpeed;
        timeSinceLastAttack -= Time.deltaTime;
        if (timeSinceLastAttack <= 0) {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                timeSinceLastAttack = attackSpeed;
            }
        }
     
        playerDirection.x = Input.GetAxis("Horizontal");
        playerDirection.y = Input.GetAxis("Vertical");
        animator.SetFloat("MoveX", playerDirection.x);
        animator.SetFloat("MoveY", playerDirection.y);

        Vector3 velocity = playerDirection * Speed;
        transform.position += velocity * Time.deltaTime;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name.Contains("Drill"))
        {
            if(Input.GetKeyDown(KeyCode.E) && !weaponActive)
            {
                drill.SetActive(true);
                flamethrower.SetActive(false);
                pickaxe.SetActive(false);
                sword.SetActive(false);
                weaponActive = true;
                weapon = drill.GetComponent<Weapon>();
            }
        } else if(collision.gameObject.name.Contains("Flamethrower"))
        {
            if(Input.GetKeyDown(KeyCode.E) && !weaponActive)
            {
                flamethrower.SetActive(true);
                drill.SetActive(false);
                pickaxe.SetActive(false);
                sword.SetActive(false);
                weaponActive = true;
                weapon = flamethrower.GetComponent<Weapon>();
            }
        } else if(collision.gameObject.name.Contains("Pickaxe"))
        {
            if(Input.GetKeyDown(KeyCode.E) && !weaponActive)
            {
                pickaxe.SetActive(true);
                drill.SetActive(false);
                flamethrower.SetActive(false);
                sword.SetActive(false);
                weaponActive = true;
                weapon = pickaxe.GetComponent<Weapon>();
            }
        } else if(collision.gameObject.name.Contains("Sword"))
        {
            if(Input.GetKeyDown(KeyCode.E) && !weaponActive)
            {
                sword.SetActive(true);
                drill.SetActive(false);
                flamethrower.SetActive(false);
                pickaxe.SetActive(false);
                weaponActive = true;
                weapon = sword.GetComponent<Weapon>();
            }
        }
    }

    public void Attack()
    {
        //play attack animation
        animator.SetTrigger("Attack");

        Collider2D[] collidingEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackReach);
        foreach(Collider2D enemy in collidingEnemies)
        {
            if(enemy.TryGetComponent(out Health h) && enemy.gameObject != gameObject)
            {
                h.TakeDamage(Strength);

                if(weapon != null)
                    AudioManager.Instance.Play($"{weapon.type} Hit");
            }
        }

        if(weapon != null) weapon.DisableAnim();
    }

    public void AttackAnim()
    {
        if (weapon != null) weapon.Attack();
    }

    public void DrillCheck()
    {
        if (!weapon) return;
        if(weapon.type == Weapon.eWeaponType.Drill)
        {
            DrillShoot();
        }
    }

    public void DrillShoot()
    {
        Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Projectile>().Fire(attackSpeed, Camera.main.ScreenToWorldPoint(Input.mousePosition), Strength, "Enemy", "Player");
        AudioManager.Instance.Play("Drill Attack");
    }

    public void Die()
    {
        //add stuff here for visuals :)
        Destroy(gameObject);
    }

    public void OnHit()
    {
        healthBar.value = health.currentHealth / health.maxHealth;
    }
}
