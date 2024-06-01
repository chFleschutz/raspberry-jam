using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour, IGameEventListener<int>, IGameEventListener<float>
{
    [Header("Events")]
    [SerializeField] private GameEvent<int> scoreEvent;
    [SerializeField] private GameEvent<float> healthEvent;

    [Header("HUD Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private HealthBar healthBar;

    // Score Event
    public void OnInvoke(int parameter)
    {
        scoreText.text = "Score: " + parameter.ToString();
    }

    // Health Event
    public void OnInvoke(float parameter)
    {
        healthBar.SetHealth(parameter / 100); // TODO: Use MaxHealth of the Player instead of 100
    }

    void Start()
    {
        scoreEvent.RegisterListener(this);
        healthEvent.RegisterListener(this);
    }

    void OnDestroy()
    {
        scoreEvent.UnregisterListener(this);
        healthEvent.UnregisterListener(this);
    }
}
