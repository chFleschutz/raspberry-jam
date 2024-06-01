using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float knockbackSlowdown;
    [SerializeField] public float knockbackResistance;
    private float knockbackPower;
    private Transform player;
    private Health healthController;
    private Vector2 knockback;

    public void SetKnockback(Vector2 knockbackDirection, float knockbackStrength)
    {
        knockback = knockbackDirection;
        knockbackPower = Mathf.Log(knockbackStrength + 1, knockbackResistance);
    }

    public void TakeDamage(float damage)
    {
        healthController.TakeDamage(damage);
    }

    private void Start()
    {
        healthController = GetComponent<Health>();

        if(healthController == null)
        {
            Debug.LogError("Enemy: Missing Health Script");
            return;
        }

        player = GameObject.FindWithTag("Player").transform;

        if(player == null)
        {
            Debug.LogError("Enemy: Please tag player as player");
        }
    }


    private void Update()
    {
        Move(player.position);
    }

    private void Move(Vector2 goal)
    {
        Vector2 direction = goal - new Vector2(transform.position.x, transform.position.y);
        Vector2 adjustedDirection = (direction.normalized * speed + knockback.normalized * knockbackPower) * Time.deltaTime;
        transform.position = CollisionForecast.ForecastBox2D(gameObject, adjustedDirection, Vector2.one);

        if(knockbackPower > 0)
        {
            knockbackPower -= knockbackSlowdown * Time.deltaTime;
        }
    }
}
