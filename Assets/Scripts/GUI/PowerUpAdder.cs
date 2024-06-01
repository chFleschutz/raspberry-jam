using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpAdder : MonoBehaviour, IGameEventListener<PowerUp>
{
    [Header("Events")]
    [SerializeField] private GameEvent<PowerUp> powerUpActivatedEvent;

    [Header("HUD")]
    [SerializeField] private HUD hud;

    public void OnInvoke(PowerUp powerUp)
    {
        hud.AddPowerUp(powerUp);
    }

    void Start()
    {
        powerUpActivatedEvent.RegisterListener(this);
    }

    void OnDestroy()
    {
        powerUpActivatedEvent.UnregisterListener(this);
    }
}
