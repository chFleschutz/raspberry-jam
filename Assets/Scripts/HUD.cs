using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour, IGameEventListener<int>
{
    [Header("Events")]
    [SerializeField] private GameEvent<int> scoreEvent;

    [Header("HUD Elements")]
    [SerializeField] private TMPro.TextMeshProUGUI scoreText;

    public void OnInvoke(int parameter)
    {
        scoreText.text = "Score: " + parameter.ToString();
    }

    void Start()
    {
        scoreEvent.RegisterListener(this);
    }

    void OnDestroy()
    {
        scoreEvent.UnregisterListener(this);
    }
}
