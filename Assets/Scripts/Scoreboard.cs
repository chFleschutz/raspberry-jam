using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scoreboard : GameEventListenerInt
{
    // Event is called every time the score changes with the new score
    [SerializeField] private GameEventInt onScoreChanged;
    // Event is called every time points are awarded with the points awarded
    [SerializeField] private GameEventInt onPointsAwarded;
    [SerializeField] private bool logScoreChanges;

    private int score = 0;

    public int Score => score;

    public void Add(int points)
    {
        score += points;

        if (logScoreChanges)
            Debug.Log("Score changed: " + score + " (Added: " + points + ")");

        onScoreChanged.Invoke(this, score);
    }

    public override void OnInvoke(int points)
    {
        Add(points);
    }

    void Start()
    {
        onPointsAwarded.RegisterListener(this);
    }

    void OnDestroy()
    {
        onPointsAwarded.UnregisterListener(this);
    }
}
