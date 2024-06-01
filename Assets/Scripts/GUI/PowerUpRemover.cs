using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpRemover : MonoBehaviour, IGameEventListener<PowerUp>
{
    [Header("Events")]
    [SerializeField] private GameEvent<PowerUp> powerUpDeactivatedEvent;

    [Header("HUD")]
    [SerializeField] private HUD hud;

    public void OnInvoke(PowerUp powerUp)
    {
        hud.RemovePowerUp(powerUp);
    }

    void Start()
    {
        powerUpDeactivatedEvent.RegisterListener(this);
    }

    void OnDestroy()
    {
        powerUpDeactivatedEvent.UnregisterListener(this);
    }
}
