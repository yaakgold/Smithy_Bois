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
<<<<<<< HEAD
    private float attackSpeed = 2.0f;
=======
<<<<<<< HEAD
    private float attackSpeed = 1.0f;
=======
    private float attackSpeed = .5f;
>>>>>>> b7d7fe08bcd61a3632cda46566f5cf106a875454
>>>>>>> 46a04db17123940d800f9268fc1cbf47c2da1a20
    private float timeSinceLastAttack = 0;
    private Vector3 playerDirection;

    public Slider attackRechargeBar;
    public Slider healthBar;
    public TMP_Text coinsTxt;

    public GameObject winPanel;
    public TMP_Text coinWinText;
    public GameObject deathPanel;
    public TMP_Text coinDeathText;

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
            if (Input.GetButton("Fire1"))
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
            if(Input.GetButton("Submit") && !weaponActive)
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
            if(Input.GetButton("Submit") && !weaponActive)
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
            if(Input.GetButton("Submit") && !weaponActive)
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
            if(Input.GetButton("Submit") && !weaponActive)
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
        deathPanel.SetActive(true);
        coinDeathText.text = $"At least you found {_coins} coins in the dungeons.";

        Destroy(gameObject);
    }

    public void OnHit()
    {
        healthBar.value = health.currentHealth / health.maxHealth;
    }

    public void WinScreen()
    {
        winPanel.SetActive(true);
        coinWinText.text = $"You were able to find {_coins} coins along the way. Congrats!";
    }
}
