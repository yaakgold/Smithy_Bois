using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public UnityEvent OnDeath;

    private void Start()
    {
        currentHealth = maxHealth;
        OnDeath = new UnityEvent();
    }

    public float TakeDamage(float amt)
    {
        currentHealth -= amt;

        if(currentHealth <= 0)
        {
            OnDeath.Invoke();
        }

        return currentHealth;
    }
}
