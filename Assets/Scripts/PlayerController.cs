using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IGameEventListener
{
    [SerializeField] private GameEvent PlayerDeathEvent;

    [Header("Player Components")]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private SurvivalPoints survivalPoints;

    public void OnInvoke()
    {
        playerMovement.enabled = false;
        survivalPoints.enabled = false;
    }

    private void Start()
    {
        PlayerDeathEvent.RegisterListener(this);
    }

    private void OnDestroy()
    {
        PlayerDeathEvent.UnregisterListener(this);
    }
}
