using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : GameEventListenerFloat
{
    [SerializeField] private GameEventFloat onPlayerDamaged;

    public override void OnInvoke(float health)
    {
        Debug.Log("DamageDealer: " + health);
    }

    private void Start()
    {
        onPlayerDamaged.RegisterListener(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Health health = GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(10.0f);
            }
        }   
    }

    private void OnDestroy()
    {
        onPlayerDamaged.UnregisterListener(this);
    }
}
