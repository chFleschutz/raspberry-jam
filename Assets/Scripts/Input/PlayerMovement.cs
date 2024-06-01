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
    [SerializeField] private float chargeFuelCost;
    private bool charging;
    private float charge;

    [Header("Knockback")]
    [SerializeField] private float knockbackSlowDown;
    [SerializeField] private float knockbackResistance;
    private Vector2 knockbackDirection;
    private float knockbackPower;

    [Header("Fuel")]
    [SerializeField] private float maxFuel;
    private float currentFuel;

    [Header("Other")]
    [SerializeField] private float slowDown;
    private Vector2 movementDirection;
    private float velocity;
     
    [SerializeField] private Slider slider;
    [SerializeField] private Slider slider2;
    [SerializeField] private float speed;
    private Vector2 inputDirection;
    private Vector2 mousePosition;

    public float CooldownTime { get => cooldownTime; set => cooldownTime = value; }
    public float ChargePower { get => chargePower; set => chargePower = value; }

    public void AddFuel(float fuel)
    {
        currentFuel += fuel;

        if(currentFuel > maxFuel)
            currentFuel = maxFuel;
    }

    public void AddKnockback(Vector2 direction, float power)
    {
        knockbackDirection = direction.normalized;
        knockbackPower = Mathf.Log(power, knockbackResistance);
    }

    private void Start()
    {
        currentFuel = maxFuel;
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

        if (charging && charge < chargeMax && currentFuel > 0 && !onCooldown)
        {
            float deltaCharge = Time.deltaTime * chargeSpeed * chargeCurve.Evaluate(charge / chargeMax);
            charge += deltaCharge;
            currentFuel -= deltaCharge;
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
            Vector2 predictedPosition = CollisionForecast.ForecastBox2D(gameObject, direction, Vector2.one, out hit);
            transform.position = predictedPosition;

            if (probablyPosition != predictedPosition)
            {
                movementDirection = Vector2.Reflect(movementDirection, hit.normal);
                velocity *= 0.8f;

                if (hit.collider.tag == "Enemy")
                    hit.transform.GetComponent<EnemyBase>().SetKnockback(direction, velocity);
            }

            if (knockbackPower > 0)
                knockbackPower -= Time.deltaTime * knockbackSlowDown;
            else 
                knockbackPower = 0;

            if (velocity > 0) 
                velocity -= Time.deltaTime + slowDown;
            else 
                velocity = 0;

        }

        if (slider != null) slider.value = charge/chargeMax;

        if (slider2 != null) slider2.value = currentFuel / maxFuel;
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        inputDirection = context.ReadValue<Vector2>();
    }

    public void OnCharge(InputAction.CallbackContext context)
    {
        if(context.performed)
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
