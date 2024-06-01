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

    [Header("Other")]
    [SerializeField] private float slowDown;
    private Vector2 movementDirection;
    private float velocity;
     
    [SerializeField] private Slider slider;
    [SerializeField] private float speed;
    private Vector2 inputDirection;
    private Vector2 mousePosition;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        onCooldown = false;
    }

    // Update is called once per frame
    void Update()
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
            velocity = charge;
        }

        if (!charging && velocity > 0)
        {
            RaycastHit2D hit;
            Vector2 direction = movementDirection.normalized * Time.deltaTime * velocity * chargePower;
            Vector2 probablyPosition = new Vector2(transform.position.x, transform.position.y) + direction;
            Vector2 predictedPositon = CollisionForecast.ForecastBox2D(gameObject, direction, Vector2.one, out hit);
            transform.position = predictedPositon;
            if (probablyPosition != predictedPositon)
            {
                movementDirection = Vector2.Reflect(movementDirection, hit.normal);
                velocity *= 0.8f;
            }
            charge = 0;
            onCooldown = true;
            velocity -= Time.deltaTime + slowDown;
        }

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
