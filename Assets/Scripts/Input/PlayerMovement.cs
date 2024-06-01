using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Cooldown")]
    [SerializeField] private float cooldownTime;
    private float cooldown;
    private bool onCooldown;

    [Header("Charge")]
    [SerializeField] private AnimationCurve chargeCurve;
    [SerializeField] private float chargeSpeed;
    [SerializeField] private float chargeMax;
    [SerializeField] private float chargePower;
    private bool charging;
    private float charge;

    [Header("Knockback")]
    [SerializeField] private float knockbackSlowDown;
    private Vector2 knockbackDirection;
    private float knockbackPower;

    [Header("Other")]
    [SerializeField] private float slowDown;
    private Vector2 movementDirection;
    private float velocity;
     
    [SerializeField] private Slider slider;
    [SerializeField] private float speed;
    private Vector2 inputDirection;
    private Vector2 mousePosition;

    public float CooldownTime { get => cooldownTime; set => cooldownTime = value; }
    public float ChargePower { get => chargePower; set => chargePower = value; }

    public void AddKnockback(Vector2 direction, float power)
    {
        knockbackDirection = direction.normalized;
        knockbackPower = power;
    }

    private void Start()
    {
        onCooldown = false;
    }

    private void Update()
    {
        if (inputDirection != Vector2.zero)
            transform.position = CollisionForecast.ForecastBox2D(gameObject, inputDirection * Time.deltaTime * speed, Vector2.one);

        if(onCooldown)
        {
            cooldown += Time.deltaTime;
            if(cooldown > cooldownTime)
            {
                cooldown = 0;
                onCooldown = false;
            }
        }

        if (charging && charge < chargeMax)
        {
            charge += Time.deltaTime * chargeSpeed * chargeCurve.Evaluate(charge/chargeMax);
            //velocity = charge;
        }

        if(!charging && charge > 0)
        {
            velocity = charge;
            charge = 0;
            onCooldown = true;
        }

        if (velocity > 0 || knockbackPower > 0)
        {
            RaycastHit2D hit;
            Vector2 direction = movementDirection.normalized * Time.deltaTime * velocity * chargePower;
            direction += knockbackDirection * knockbackPower * Time.deltaTime;

            Vector2 probablyPosition = new Vector2(transform.position.x, transform.position.y) + direction;
            Vector2 predictedPositon = CollisionForecast.ForecastBox2D(gameObject, direction, Vector2.one, out hit);
            transform.position = predictedPositon;

            if (probablyPosition != predictedPositon)
            {
                movementDirection = Vector2.Reflect(movementDirection, hit.normal);
                velocity *= 0.8f;

                if (hit.collider.tag == "Enemy")
                    hit.transform.GetComponent<EnemyBase>().SetKnockback(direction, velocity);
            }

            knockbackPower -= Time.deltaTime * knockbackSlowDown;
            velocity -= Time.deltaTime + slowDown;
        }

        if (slider != null)
            slider.value = charge/chargeMax;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void OnCharge(InputAction.CallbackContext context)
    {
        if(context.performed && !onCooldown)
        {
            charging = true;
        }

        if(context.canceled)
        {
            charging = false;
            mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            movementDirection = new Vector3(mousePosition.x, mousePosition.y, 0) - transform.position;
        }
    }
}
