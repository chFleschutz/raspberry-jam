using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Press P to award 10 points
public class PointSupplier : MonoBehaviour, IGameEventListener<int>
{
    [SerializeField] private GameEventInt onScoreChanged;
    [SerializeField] private GameEventInt onPointsAwarded;
    [SerializeField] private int points = 10;
    [SerializeField] private bool logScore = false;

    public void OnInvoke(int score)
    {
        if (logScore)
            Debug.Log("Point Supplier yells: score " + score);
    }

    void Start()
    {
        onScoreChanged.RegisterListener(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            onPointsAwarded.Invoke(this, points);
        }
    }

    void OnDestroy()
    {
        onScoreChanged.UnregisterListener(this);
    }
}
