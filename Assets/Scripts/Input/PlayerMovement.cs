using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.VFX;

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
    [SerializeField] private float chargeFuelCost = 1;
    [SerializeField] private VisualEffect effect;
    [SerializeField] private float trailLength;
    [SerializeField] private float jitterStrength;
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
    [SerializeField] Transform visuals;
    private Vector2 movementDirection;
    private float velocity;
     
    [SerializeField] private Slider chargePowerSlider;
    [SerializeField] private Slider chargePoolSlider;
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
        effect.Stop();
    }

    private void Update()
    {
        if (inputDirection != Vector2.zero)
            transform.position = CollisionForecast.ForecastBox2D(gameObject, inputDirection * Time.deltaTime * speed, Vector2.one);

        if(onCooldown)
        {
            Blink();
            cooldown += Time.deltaTime;
            if(cooldown > cooldownTime)
            {
                cooldown = 0;
                visuals.GetComponent<SpriteRenderer>().color = Color.white;
                onCooldown = false;
            }
        }

        if (charging && !onCooldown)
        {
            if (charge < chargeMax && currentFuel > 0)
            {
                float deltaCharge = Time.deltaTime * chargeSpeed * chargeCurve.Evaluate(charge / chargeMax);
                charge += deltaCharge;
                currentFuel -= deltaCharge * chargeFuelCost;
            }
            Effects();
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
                velocity -= Time.deltaTime * slowDown;
            else 
                velocity = 0;

        }

        if (chargePowerSlider != null) 
            chargePowerSlider.value = charge/chargeMax;

        if (chargePoolSlider != null) 
            chargePoolSlider.value = currentFuel / maxFuel;

        mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 dir = (mousePosition - new Vector2(transform.position.x, transform.position.y)).normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        visuals.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
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
            if(!onCooldown)
                movementDirection = new Vector3(mousePosition.x, mousePosition.y, 0) - transform.position;

            effect.Stop();
            visuals.localPosition = Vector2.zero;
        }
    }

    private void Effects()
    {
        effect.SetFloat("YVelocity", charge / chargeMax * -trailLength);
        effect.Play();

        Vector2 newPosition = new Vector2(Random.value * charge / chargeMax * jitterStrength, Random.value * charge / chargeMax * jitterStrength);
        visuals.localPosition = newPosition;
    }

    [SerializeField] private float maxBlinkTime = 0.2f;
    [SerializeField] private Color blinkColor = Color.gray;
    private float blinkTime = 0;
    private void Blink()
    {
        if (blinkTime > 0)
        {
            blinkTime -= Time.deltaTime;
            if(blinkTime < maxBlinkTime * 0.5f)
                visuals.GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            blinkTime = maxBlinkTime;
            visuals.GetComponent<SpriteRenderer>().color = blinkColor;
        }

    }
}
