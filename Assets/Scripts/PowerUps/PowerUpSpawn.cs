using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Collider2D trigger;
    [SerializeField] private PowerUp[] powerUps;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        foreach (var powerUp in powerUps)
        {
            StartCoroutine(PowerUpSequence(powerUp, other.gameObject));
        }
    }

    private IEnumerator PowerUpSequence(PowerUp powerUp, GameObject target)
    {
        sprite.enabled = false;
        trigger.enabled = false;

        powerUp.ApplyTo(target);
        yield return new WaitForSeconds(powerUp.Duration);
        powerUp.RemoveFrom(target);

        Destroy(gameObject);
    }
}
