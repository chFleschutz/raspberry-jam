using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField, Range(0f, 1000f)] private float maxHealth = 100.0f;
    [SerializeField] private GameEventFloat onHealthChanged;
    [SerializeField] private GameEvent onDeath;

    private float health = 100.0f;

    public void TakeDamage(float damage)
    {
        if (health <= 0)
            return;

        health -= damage;
        onHealthChanged.Invoke(this, health);

        if (health <= 0)
            onDeath.Invoke(this);
    }

    void Start()
    {
        health = maxHealth;
    }
}
