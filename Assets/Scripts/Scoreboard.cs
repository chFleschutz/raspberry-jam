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
    [SerializeField] private int highScore;

    private int score = 0;

    public int Score => score;
    public int Highscore => highScore;

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

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (logScoreChanges)
            Debug.Log("High Score: " + highScore);
    }

    void OnDestroy()
    {
        onPointsAwarded.UnregisterListener(this);

        if (score >= highScore)
        {
            PlayerPrefs.SetInt("HighScore", score);
            if (logScoreChanges)
                Debug.Log("New High Score: " + score);
        }
    }
}
