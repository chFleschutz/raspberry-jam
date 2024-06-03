using UnityEngine;

public class PlayerPointer : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private Vector2 size;
    private Transform player;
    private Vector2 screenSize;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        player = PlayerController.Instance.transform;
    }

    private void Update()
    {
        screenSize.y = cam.orthographicSize;
        screenSize.x = cam.aspect * screenSize.y;
        float screenMinX = cam.transform.position.x - screenSize.x;
        float screenMinY = cam.transform.position.y - screenSize.y;
        float screenMaxX = cam.transform.position.x + screenSize.x;
        float screenMaxY = cam.transform.position.y + screenSize.y;

        if (player.position.x < screenMaxX && player.position.x > screenMinX &&
            player.position.y < screenMaxY && player.position.y > screenMinY)
        {
            pointer.gameObject.SetActive(false);
            return;
        }

        pointer.gameObject.SetActive(true);

        Vector3 direction = player.transform.position - cam.transform.position;
        float x = Mathf.Clamp(direction.x, -screenSize.x + size.x, screenSize.x - size.x);
        float y = Mathf.Clamp(direction.y, -screenSize.y + size.y, screenSize.y - size.y);

        pointer.localPosition = new Vector3(x, y, 100);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        pointer.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
    }
}
