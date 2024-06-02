using UnityEngine;

[CreateAssetMenu(fileName = "BigPlayer", menuName = "PowerUps/BigPlayer")]
public class Big : PowerUp
{
    public float scale;
    public override void ApplyTo(GameObject target)
    {
        PlayerController.Instance.transform.localScale *= scale; 
    }

    public override void RemoveFrom(GameObject target)
    {
        PlayerController.Instance.transform.localScale /= scale; 
    }
}
