using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour, IGameEventListener<float>
{
    [SerializeField] private GameEventFloat onPlayerDamaged;
    [SerializeField] private Health playerHealth;
    [SerializeField] private float damage = 10.0f;
    [SerializeField] private bool logDamage = false;

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
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnDestroy()
    {
        onPlayerDamaged.UnregisterListener(this);
    }
}
