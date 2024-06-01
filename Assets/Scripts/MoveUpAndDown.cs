using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUpAndDown : MonoBehaviour
{
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distance = 1.0f;

    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        var amount = 0.5f * (1.0f + Mathf.Sin(Time.time * speed)) * distance;
        transform.position = startPosition + Vector2.up * amount;
    }
}
