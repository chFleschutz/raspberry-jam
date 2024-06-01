using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    [SerializeField] private float borderDamage;
    [SerializeField] private List<Transform> transforms = new List<Transform>();
    private Vector2 borderInset;
    private Vector2 screenSize;
    private Transform player;
    private Health healthController;

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
        borderInset = Vector2.one * 2;
        player = GameObject.FindWithTag("Player").transform;

        if(player == null)
        {
            Debug.LogError("Border: Please tag player as player");
            return;
        }
        else if (transforms.Count <= 0)
        {
            Debug.LogError("Camera: Please assign the border transforms in the inspector");
            return;
        }

        healthController = player.GetComponent<Health>();

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
        else if (transforms.Count <= 0)
        {
            Debug.LogError("Camera: Please assign the border transforms in the inspector");
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
            if (i < 2)
            {
                transforms[i].localScale = new Vector3(screenSize.x * 2, diff.y, 1);
                float y = (i % 2 == 0 ? 1 : -1) * (halfBorderSize.y + diff.y * 0.5f);
                transforms[i].position = cameraPosition + new Vector3(0, y, 0);
            }
            else
            {
                transforms[i].localScale = new Vector3(diff.x, screenSize.y * 2, 1);
                float x = (i % 2 == 0 ? -1 : 1) * (halfBorderSize.x + diff.x * 0.5f);
                transforms[i].position = cameraPosition + new Vector3(x, 0, 0);
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
