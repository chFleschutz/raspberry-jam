using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Collider2D trigger;
    [SerializeField] private PowerUp powerUp;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        StartCoroutine(PowerUpSequence(other.gameObject));
    }

    private IEnumerator PowerUpSequence(GameObject target)
    {
        sprite.enabled = false;
        trigger.enabled = false;

        powerUp.ApplyTo(target);
        yield return new WaitForSeconds(powerUp.Duration);
        powerUp.RemoveFrom(target);
        
        Destroy(gameObject);
    }
}
