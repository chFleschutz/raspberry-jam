using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour, IGameEventListener<int>, IGameEventListener<float>
{
    [Header("Events")]
    [SerializeField] private GameEvent<int> scoreEvent;
    [SerializeField] private GameEvent<float> healthEvent;

    [Header("HUD Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private GameObject powerUpPanel;

    [Header("PowerUp Prefab")]
    [SerializeField] private GameObject powerUpPrefab;
    private Dictionary<PowerUp, GameObject> powerUpObjects = new();

    public void AddPowerUp(PowerUp powerUp)
    {
        Debug.Log("Adding PowerUp");

        if (powerUpObjects.ContainsKey(powerUp))
            return;

        if (powerUp.Duration <= 0.01f)
            return;

        var powerUpObject = Instantiate(powerUpPrefab, powerUpPanel.transform);
        powerUpObject.name = powerUp.Name;
        powerUpObject.GetComponent<Image>().sprite = powerUp.Icon;

        powerUpObjects.Add(powerUp, powerUpObject);
    }

    public void RemovePowerUp(PowerUp powerUp)
    {
        Debug.Log("Removing PowerUp");

        if (!powerUpObjects.ContainsKey(powerUp))
            return;

        Destroy(powerUpObjects[powerUp]);
        powerUpObjects.Remove(powerUp);
    }

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
