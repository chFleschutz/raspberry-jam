using UnityEngine;

public class PlayerController : MonoBehaviour, IGameEventListener, IGameEventListener<float>
{
    public static PlayerController Instance;
    [SerializeField] private GameEvent PlayerDeathEvent;
    [SerializeField] private GameEventFloat playerHurtEvent;
    [SerializeField] private AudioSource hurtSound;

    [Header("Player Components")]
    public PlayerMovement PlayerMovement;
    public Health HealthController;
    public SurvivalPoints SurvivalPoints;
    public PlayerAttack PlayerAttack;

    public void OnInvoke()
    {
        PlayerMovement.enabled = false;
        SurvivalPoints.enabled = false;
        PlayerAttack.enabled = false;
    }

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("Player: Multiple Player Controller");

        hurtSound.Stop();

        Instance = this;
    }

    private void Start()
    {
        playerHurtEvent.RegisterListener(this);
        PlayerDeathEvent.RegisterListener(this);
    }

    private void OnDestroy()
    {
        playerHurtEvent.UnregisterListener(this);
        PlayerDeathEvent.UnregisterListener(this);
    }

    void IGameEventListener<float>.OnInvoke(float parameter)
    {
        if(!hurtSound.isPlaying && parameter < 0)
            hurtSound.Play();
    }
}
