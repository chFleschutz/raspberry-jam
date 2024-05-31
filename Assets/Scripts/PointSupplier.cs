using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Press P to award 10 points
public class PointSupplier : GameEventListenerInt
{
    [SerializeField] private GameEventInt onScoreChanged;
    [SerializeField] private GameEventInt onPointsAwarded;

    public override void OnInvoke(int score)
    {
        Debug.Log("Score: " + score);
    }

    void Start()
    {
        onScoreChanged.RegisterListener(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            onPointsAwarded.Invoke(this, 10);
        }
    }

    void OnDestroy()
    {
        onScoreChanged.UnregisterListener(this);
    }
}
