using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class BorderController : MonoBehaviour
{
    [SerializeField] private float borderDamage;
    [SerializeField] private List<VisualEffect> effects = new List<VisualEffect>();
    [SerializeField] private Vector2 borderInset;
    [SerializeField] private float effectScale;
    private Vector2 screenSize;
    private Transform player;
    private Health healthController;

    public void SetBorderInset(float inset)
    {
        borderInset = new Vector2(inset, inset);
    }

    public void AddBorderInset(float inset)
    {
        borderInset += new Vector2(inset, inset);
    }

    public void SetBorderInset(Vector2 inset)
    {
        borderInset = inset;
    }

    public void AddBorderInset(Vector2 inset)
    {
        borderInset += inset;
    }

    private void Start()
    {
        player = PlayerController.Instance.transform;

        if(player == null)
        {
            Debug.LogError("Border: Please tag player as player");
            return;
        }
        else if (effects.Count <= 0)
        {
            Debug.LogError("Camera: Please assign the border effects in the inspector");
            return;
        }

        healthController = PlayerController.Instance.HealthController;

        UpdateBorders(screenSize - borderInset);
        CheckPlayerPosition();
    }

    private void Update()
    {
        if (player == null)
        {
            Debug.LogError("Border: Please tag player as player");
            return;
        }

        UpdateBorders(screenSize - borderInset);
        CheckPlayerPosition();
    }

    private void UpdateBorders(Vector2 halfBorderSize)
    {
        screenSize.y = Camera.main.orthographicSize;
        screenSize.x = Camera.main.aspect * screenSize.y;

        Vector2 diff = (screenSize - halfBorderSize) + Vector2.one;
        Vector3 cameraPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
        for (int i = 0; i < 4; i++)
        {
            Vector2 vel = effects[i].GetVector2("VelocityDirection").normalized * (diff * 2f * effectScale + Vector2.one);
            effects[i].SetVector2("VelocityDirection", vel);
            if (i < 2)
            {
                effects[i].SetVector2("WallSize", new Vector2(screenSize.x * 2, effects[i].GetVector2("WallSize").y));
                float y = (i % 2 == 0 ? 1 : -1) * (screenSize.y + 0.5f);
                effects[i].transform.position = cameraPosition + new Vector3(0, y, 0);
            }
            else
            {
                effects[i].SetVector2("WallSize", new Vector2(effects[i].GetVector2("WallSize").x, screenSize.y * 2));
                float x = (i % 2 == 0 ? -1 : 1) * (screenSize.x + 0.5f);
                effects[i].transform.position = cameraPosition + new Vector3(x, 0, 0);
            }

        }
    }

    private void CheckPlayerPosition()
    {
        Vector3 cameraPosition = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);

        if (player.position.x > cameraPosition.x + screenSize.x - borderInset.x ||
            player.position.y > cameraPosition.y + screenSize.y - borderInset.y ||
            player.position.x < cameraPosition.x - screenSize.x + borderInset.x ||
            player.position.y < cameraPosition.y - screenSize.y + borderInset.y)
        {
            healthController.TakeDamage(borderDamage * Time.deltaTime);
        }
    }
}
