using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private RectTransform healthBarMask;
    private float baseWidth;

    public void SetHealth(float health)
    {
        healthBarMask.sizeDelta = new Vector2(baseWidth * health, healthBarMask.sizeDelta.y);
    }

    void Start()
    {
        // get base width of health bar
        baseWidth = healthBarMask.sizeDelta.x;
    }
}
