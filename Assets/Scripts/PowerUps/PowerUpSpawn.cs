using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    [Header("Events")] 
    [SerializeField] private GameEvent<PowerUp> powerUpActivatedEvent;
    [SerializeField] private GameEvent<PowerUp> powerUpDeactivatedEvent;

    [Header("Components")]
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Collider2D trigger;

    [SerializeField] private PowerUp[] powerUps;
    private int finishCount = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        finishCount = 0;
        foreach (var powerUp in powerUps)
        {
            StartCoroutine(PowerUpSequence(powerUp, other.gameObject));
        }
    }

    private IEnumerator PowerUpSequence(PowerUp powerUp, GameObject target)
    {
        powerUpActivatedEvent.Invoke(this, powerUp);
        sprite.enabled = false;
        trigger.enabled = false;

        powerUp.ApplyTo(target);
        yield return new WaitForSeconds(powerUp.Duration);
        powerUp.RemoveFrom(target);

        powerUpDeactivatedEvent.Invoke(this, powerUp);

        finishCount++;
        if (finishCount == powerUps.Length)
            Destroy(gameObject);
    }
}
