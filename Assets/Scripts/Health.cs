using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public UnityEvent OnDeath;
    public UnityEvent OnHit;

    private void Start()
    {
        currentHealth = maxHealth;
        OnDeath = new UnityEvent();
        OnHit = new UnityEvent();
    }

    public float TakeDamage(float amt)
    {
        currentHealth -= amt;

        if(currentHealth <= 0)
        {
            OnDeath.Invoke();
        }
        else
        {
            OnHit.Invoke();
        }
        
        return currentHealth;
    }
}
