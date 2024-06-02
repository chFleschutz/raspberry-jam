using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement Instance;
    [SerializeField] private AnimationCurve speedOverTime;
    [SerializeField] private float speed;
    private float distance;
    private List<Vector2> goalPositions = new List<Vector2>();
    private int currentGoalIndex = 0;

    public void AddGoalDirecion(Vector2 direction)
    {
        goalPositions[0] += direction;
    }

    public void AddGoalPosition(Vector2 position)
    {
        goalPositions.Add(position);
    }

    public void SetGoalList(List<Vector2> positions)
    {
        goalPositions = positions;
    }

    public void AddGoalList(List<Vector2> positions)
    {
        goalPositions.AddRange(positions);
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("CameraMovement instance exists twice. Call CameraMovement.Instance only in Start()");
        }
        Instance = this;
    }

    private void Update()
    {
        if (goalPositions.Count <= 0)
            return;

        if(distance == 0) 
            distance = (new Vector3(goalPositions[0].x, goalPositions[0].y, 0) - transform.position).magnitude;

        var goalPosition = goalPositions[currentGoalIndex];
        Vector3 direction = new Vector3(goalPosition.x, goalPosition.y) - new Vector3(transform.position.x, transform.position.y, 0);

        if (!(direction.x < 0.01f && direction.x > -0.1f) || !(direction.y < 0.1f && direction.y > -0.1f))
            transform.position += direction.normalized * speed * Time.deltaTime * speedOverTime.Evaluate(direction.magnitude/distance);
        else
        {
            currentGoalIndex = (currentGoalIndex + 1) % goalPositions.Count;
        }
    }
}
