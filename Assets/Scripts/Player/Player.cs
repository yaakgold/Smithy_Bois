using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Health health;
    public float Speed { get; set; } = 10.0f;
    public float Strength { get; set; } = 1.0f;

    void Start()
    {
        health.OnDeath.AddListener(Die);
    }

    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        Vector3 velocity = direction * Speed;
        transform.position += velocity * Time.deltaTime;
    }

    public void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            
        }
    }

    public void Die()
    {
        //add stuff here for visuals :)
        Destroy(gameObject);
    }
}
