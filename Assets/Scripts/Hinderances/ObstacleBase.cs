using UnityEngine;

public class ObstacleBase : MonoBehaviour
{
    protected PlayerController playerController;

    protected virtual void Start()
    {
        playerController = PlayerController.Instance;
    }


    private void Update()
    {
        
    }

    protected virtual void Trigger()
    {
        
    }
}
