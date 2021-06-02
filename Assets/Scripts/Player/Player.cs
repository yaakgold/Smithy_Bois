using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public Health health;
    public float Speed { get; set; } = 10.0f;
    public float Strength { get; set; } = 1.0f;
    public Transform attackPoint;
    private float attackReach = 2.0f;
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

        Vector3 velocity = playerDirection * Speed;
        transform.position += velocity * Time.deltaTime;
    }

    public void Attack()
    {
        //play attack animation
        Collider2D[] collidingEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackReach);
        foreach(Collider2D enemy in collidingEnemies)
        {
            if(enemy.TryGetComponent(out Health h))
            {
                h.TakeDamage(Strength);
            }
        }
    }

    public void Die()
    {
        //add stuff here for visuals :)
        Destroy(gameObject);
    }
}
