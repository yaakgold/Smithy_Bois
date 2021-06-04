using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Health health;
    public Weapon weapon = new Weapon();
    public float Speed { get; set; } = 5.0f;
    public float Strength { get; set; } = 1.0f;
    public Transform attackPoint;
    private float attackReach = 2.0f;
    private float attackSpeed = 2.0f;
    private Vector3 playerDirection;

    void Start()
    {
        health.OnDeath.AddListener(Die);
    }

    void Update()
    {
        playerDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Attack();
        }

        playerDirection.x = Input.GetAxis("Horizontal");
        playerDirection.y = Input.GetAxis("Vertical");
        animator.SetFloat("MoveX", playerDirection.x);
        animator.SetFloat("MoveY", playerDirection.y);

        Vector3 velocity = playerDirection * Speed;
        transform.position += velocity * Time.deltaTime;
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
            }
        }

        if(weapon != null) weapon.DisableAnim();
    }

    public void AttackAnim()
    {
        if (weapon != null) weapon.Attack();
    }

    public void Die()
    {
        //add stuff here for visuals :)
        Destroy(gameObject);
    }

    public void ChooseWeapon(Weapon weapon)
    {
        switch (weapon.type)
        {
            case Weapon.eWeaponType.Drill:

                break;
            case Weapon.eWeaponType.Flamethrower:
                break;
            case Weapon.eWeaponType.Pickaxe:
                break;
            case Weapon.eWeaponType.Sword:
                break;
            default:
                break;
        }
    }
}
