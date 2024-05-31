using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalPoints : MonoBehaviour
{
    [SerializeField] private GameEventInt pointsAwardedEvent;
    [SerializeField] private int pointsPerUpdate = 10;
    [SerializeField] private float updateInterval = 1.0f;

    float elapsedTime = 0f;

    void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime >= updateInterval)
        {
            pointsAwardedEvent.Invoke(this, pointsPerUpdate);
            elapsedTime = 0f;
        }
    }
}
