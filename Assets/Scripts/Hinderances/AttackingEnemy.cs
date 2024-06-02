using Unity.VisualScripting;
using UnityEngine;

public class AttackingEnemy : EnemyBase
{
    [Header("Attack")]
    [SerializeField] private float attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackPrepareTime;
    [SerializeField] private float attackSpeed;
    [SerializeField] private float attackKnockback;
    [SerializeField] private float slowdownAfterAttack;
    [SerializeField] private float attackCooldown;

    private float currentSpeed;
    private float chargeTime;
    private bool isCharging;
    private float cooldown;

    protected override void Move(Vector2 goal)
    {
        Vector2 direction = goal - new Vector2(transform.position.x, transform.position.y);

        if (visuals != null)
        {
            visuals.rotation = Quaternion.Euler(new Vector3(0, 0, (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90));
        }

        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (!isCharging && direction.magnitude < attackRange && cooldown <= 0)
        {
            isCharging = true;
        }

        if(isCharging)
        {
            currentSpeed = 0;
            chargeTime += Time.deltaTime;
            if(chargeTime > attackPrepareTime)
            {
                chargeTime = 0;
                isCharging = false;
                cooldown = attackCooldown;
                currentSpeed = attackSpeed;
            }
        }

        RaycastHit2D hit;
        Vector2 adjustedDirection = (direction.normalized * currentSpeed + knockback.normalized * knockbackPower) * Time.deltaTime;
        transform.position = CollisionForecast.ForecastBox2D(gameObject, adjustedDirection, Vector2.one, out hit);

        if(currentSpeed > speed)
        {
            if(hit.collider != null && hit.collider.tag == "Player")
            {
                PlayerController.Instance.HealthController.TakeDamage(attackDamage);
                PlayerController.Instance.PlayerMovement.AddKnockback(direction, attackKnockback);
            }

            currentSpeed -= slowdownAfterAttack * Time.deltaTime;
        }
        else 
            currentSpeed = speed;

        if (knockbackPower > 0)
        {
            knockbackPower -= knockbackSlowdown * Time.deltaTime;
        }

    }
}
