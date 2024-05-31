using System.Collections.Generic;
using UnityEngine;

public class PathHandler : MonoBehaviour
{
    [SerializeField] private List<Vector2> wayPoints = new List<Vector2>();
    // Start is called before the first frame update
    private void Start()
    {
        wayPoints.Clear();
        Transform[] transforms = transform.GetComponentsInChildren<Transform>();
        foreach (Transform wayPoint in transforms)
        {
            if (wayPoint != transform)
                wayPoints.Add(wayPoint.position);
        }
        CameraMovement.Instance.SetGoalList(wayPoints);
    }
}
