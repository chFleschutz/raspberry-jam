using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float lifespan = 5;
    private GameObject sourceShooter;
    private float damage;
    private float speed;
    private Vector2 direction;
    private float lifetime = 0;

    public void Initialize(GameObject source, float bulletDamage, Vector2 bulletDirection, float bulletSpeed)
    {
        sourceShooter = source;
        damage = bulletDamage;
        speed = bulletSpeed;
        direction = bulletDirection.normalized;
    }

    private void Update()
    {
        if (lifetime < lifespan)
            lifetime += Time.deltaTime;
        else
            Destroy(gameObject);

        RaycastHit2D hit;
        Vector2 probablePosition = new Vector2(transform.position.x, transform.position.y) + direction * speed * Time.deltaTime;
        Vector2 predictedPosition = CollisionForecast.ForecastCircle2D(gameObject, direction * speed * Time.deltaTime, radius, out hit);

        if(hit.collider == null)
        {
            transform.position = probablePosition;
            return;
        }

        if (hit.collider.gameObject == sourceShooter)
        {
            transform.position = probablePosition;
            return;
        }

        transform.position = predictedPosition;
        Destroy(gameObject);

        if (hit.collider.tag == "Enemy")
        {
            hit.transform.GetComponent<EnemyBase>().TakeDamage(damage);
            return;
        }
        
        if (hit.collider.tag == "Player")
        {
            PlayerController.Instance.HealthController.TakeDamage(damage);
            return;
        }
    }
}
