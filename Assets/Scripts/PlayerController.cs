using UnityEngine;

public class PlayerController : MonoBehaviour, IGameEventListener
{
    public static PlayerController Instance;
    [SerializeField] private GameEvent PlayerDeathEvent;

    [Header("Player Components")]
    public PlayerMovement PlayerMovement;
    public Health HealthController;
    public SurvivalPoints SurvivalPoints;

    public void OnInvoke()
    {
        PlayerMovement.enabled = false;
        SurvivalPoints.enabled = false;
    }

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Player: Multiple Player Controller");

        Instance = this;
    }

    private void Start()
    {
        PlayerDeathEvent.RegisterListener(this);
    }

    private void OnDestroy()
    {
        PlayerDeathEvent.UnregisterListener(this);
    }
}
