using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cheats : MonoBehaviour, IGameEventListener<float>
{
    [Header("References")]
    [SerializeField] private GameEventFloat onPlayerDamaged;
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerMovement playerMovement;

    [Header("Damage")]
    [SerializeField] private float damageAmount = 10.0f;
    [SerializeField] private bool logDamage = false;

    [Header("Healing")]
    [SerializeField] private float healAmount = 10.0f;

    [Header("Fuel")]
    [SerializeField] private float fuelAmount = 10.0f;

    public void OnInvoke(float health)
    {
        if (logDamage)
            Debug.Log("DamageDealer: " + health);
    }

    private void Start()
    {
        onPlayerDamaged.RegisterListener(this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            playerHealth.TakeDamage(damageAmount);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            playerHealth.IncreaseHealth(healAmount);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            playerMovement.AddFuel(fuelAmount);
        }
    }

    private void OnDestroy()
    {
        onPlayerDamaged.UnregisterListener(this);
    }
}
