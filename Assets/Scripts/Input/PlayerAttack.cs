using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float bulletDamage;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float knockbackStrength;

    private float cooldown;
    private bool isShooting;

    private void Update()
    {
        if (cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if (isShooting && cooldown <= 0)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 direction = new Vector3(mousePosition.x, mousePosition.y, 0) - transform.position;

            Bullet bullet = Instantiate(bulletPrefab);
            bullet.transform.position = transform.position;
            bullet.Initialize(gameObject, bulletDamage, direction, bulletSpeed);

            PlayerController.Instance.PlayerMovement.AddKnockback(-direction, knockbackStrength);

            cooldown = cooldownTime;
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            isShooting = true;
        }
        if(context.canceled)
        {
            isShooting = false;
        }
    }
}
