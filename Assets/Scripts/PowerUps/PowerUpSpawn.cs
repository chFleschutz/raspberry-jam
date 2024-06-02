using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawn : MonoBehaviour
{
    [Header("Events")] 
    [SerializeField] private GameEvent<PowerUp> powerUpActivatedEvent;
    [SerializeField] private GameEvent<PowerUp> powerUpDeactivatedEvent;

    [Header("Components")]
    [SerializeField] private GameObject visuals;
    [SerializeField] private Collider2D trigger;
    [SerializeField] private AudioSource pickUpSound;

    [Header("Settings")]
    [SerializeField] private float respawnTime = 10f;
    [SerializeField] private PowerUp[] powerUps;

    private void Start()
    {
        pickUpSound.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
            return;

        StartCoroutine(PowerUpSequence(other.gameObject));
    }

    private IEnumerator PowerUpSequence(GameObject target)
    {
        pickUpSound.Play();
        Activate(false);

        // Apply a random power up
        var powerUp = powerUps[Random.Range(0, powerUps.Length)];
        powerUpActivatedEvent.Invoke(this, powerUp);
        powerUp.ApplyTo(target);

        yield return new WaitForSeconds(powerUp.Duration);

        // Remove the power up
        powerUp.RemoveFrom(target);
        powerUpDeactivatedEvent.Invoke(this, powerUp);

        // Respawn the power up
        yield return new WaitForSeconds(respawnTime);
        Activate(true);
    }

    private void Activate(bool active)
    {
        trigger.enabled = active;
        visuals.SetActive(active);
    }
}
