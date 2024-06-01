using UnityEngine;

public class ChargeFuel : MonoBehaviour
{
    [SerializeField] private float fuelAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlayerController.Instance.PlayerMovement.AddFuel(fuelAmount);
            Destroy(gameObject);
        }
    }
}
