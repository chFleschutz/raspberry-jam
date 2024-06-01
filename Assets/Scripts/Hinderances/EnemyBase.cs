using UnityEngine;

public class EnemyBase : MonoBehaviour, IGameEventListener
{
    [Header("Base States")]
    [SerializeField] protected float speed;
    [SerializeField] protected float knockbackSlowdown;
    [SerializeField] protected float knockbackResistance;
    [SerializeField] protected ChargeFuel fuelCapsule;
    [SerializeField] protected GameEvent onDeath;
    protected float knockbackPower;
    protected Transform player;
    private Health healthController;
    protected Vector2 knockback;

    public void SetKnockback(Vector2 knockbackDirection, float knockbackStrength)
    {
        knockback = knockbackDirection;
        knockbackPower = Mathf.Log(knockbackStrength + 1, knockbackResistance);
    }

    public void TakeDamage(float damage)
    {
        healthController.TakeDamage(damage);
    }

    protected void Start()
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

        onDeath.RegisterListenerOnSourceObject(healthController, this);
    }


    protected void Update()
    {
        Move(player.position);
    }

    private void OnDestroy()
    {
        onDeath.UnregisterListenerOnSourceObject(healthController, this);
    }

    protected virtual void Move(Vector2 goal)
    {
        Vector2 direction = goal - new Vector2(transform.position.x, transform.position.y);
        Vector2 adjustedDirection = (direction.normalized * speed + knockback.normalized * knockbackPower) * Time.deltaTime;
        transform.position = CollisionForecast.ForecastBox2D(gameObject, adjustedDirection, Vector2.one);

        if(knockbackPower > 0)
        {
            knockbackPower -= knockbackSlowdown * Time.deltaTime;
        }
    }

    void IGameEventListener.OnInvoke()
    {
        ChargeFuel fuel = Instantiate(fuelCapsule);
        fuel.transform.position = transform.position;
        Destroy(gameObject);
    }
}
