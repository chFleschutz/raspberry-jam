using UnityEngine;

public class ObstacleKnockback : ObstacleBase
{
    [SerializeField] Transform piston;
    [SerializeField] private float knockbackStrength;
    [SerializeField] private float drawbackSpeed;
    [SerializeField] private float effectDistance;
    [SerializeField] private float interval;
    private bool isTriggered;
    private bool isExtending;
    private float time;
    private float distance;
    private Vector2 pistonOrigin;

    protected override void Start()
    {
        base.Start();
        pistonOrigin = piston.position;
    }

    private void Update()
    {
        if (!isTriggered)
        {
            if(time < interval)
            {
                time += Time.deltaTime;
            }
            else
            {
                isExtending = true;
                isTriggered = true;
                time = 0;
            }
        }
        else
        {
            if(isExtending)
            {
                Trigger();
                if(distance < effectDistance)
                    distance += Time.deltaTime * knockbackStrength;
                else 
                    isExtending = false;
            }
            else
            {
                if(distance > 0)
                    distance -= Time.deltaTime * drawbackSpeed;
                else
                {
                    isTriggered = false;
                }
            }
            piston.position = pistonOrigin + new Vector2(transform.up.x, transform.up.y) * distance;
        }
    }

    override protected void Trigger()
    {
        RaycastHit2D hit;
        CollisionForecast.ForecastBox2D(gameObject, transform.up * distance, Vector2.one, out hit);

        if(hit.collider != null && hit.collider.tag == "Player" && hit.normal == -new Vector2(transform.up.x, transform.up.y))
        {
            playerController.PlayerMovement.AddKnockback(transform.up, knockbackStrength);
        }
    }
}
