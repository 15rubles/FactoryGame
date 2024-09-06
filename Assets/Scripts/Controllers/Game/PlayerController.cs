using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Transform destinationLineStart; 
    [SerializeField] private Transform destinationLineEnd; 
    [SerializeField] private float destinationCalculationAccuracy = 1f;
    [SerializeField] private float closestDistanceAccuracy = 0.05f;
    [SerializeField] private float maxDistanceFromNavMeshBeforeLoss = 0.05f;
    [SerializeField] private float timeForPathRecalculation = 0.25f;
    [SerializeField] private bool isUpdatePosition = true;
    [SerializeField] private float initialPlayerSpeed = 7;
    [SerializeField] private float initialPlayerAcceleration = 8;

    public Vector3 destinationLineStartVector;
    public Vector3 destinationLineEndVector;
    public Vector3 destinationPointVector;
    private NavMeshAgent agent;

    private float timer = 0;


    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.speed = initialPlayerSpeed;
        agent.acceleration = initialPlayerAcceleration;
    }


    // Update is called once per frame
    void Update()
    {
        // agent.updatePosition = isUpdatePosition;
        if (timer >= (timeForPathRecalculation / GlobalConfig.GetSpeedIncreasePercent()) || Input.touchCount > 0)
        {
            Vector3 destinationPoint = FindClosestAccessiblePoint(destinationLineStart.position, 
            destinationLineEnd.position, destinationCalculationAccuracy);
            destinationPointVector = destinationPoint;
            destinationLineStartVector = destinationLineStart.position;
            destinationLineEndVector = destinationLineEnd.position;
            agent.destination = destinationPoint;
            agent.SetDestination(destinationPoint);
            timer = 0;
        } else {
            timer += Time.deltaTime;
        }
       
        // if (IsNavMeshAgentCanMove()) {
        //     Debug.Log(agent.path.corners + " " + agent.path.corners.Length);
        //     if (agent.path.corners.Length > 2) {
        //         Vector3 direction = (agent.path.corners[1]  - agent.path.corners[0]).normalized;
        //         transform.Translate(direction * GlobalConfig.GetSpeed() * Time.deltaTime);
        //     }
        // }
    }

    void LateUpdate()
    {
        agent.speed = initialPlayerSpeed * GlobalConfig.GetSpeedIncreasePercent();
        agent.acceleration = initialPlayerAcceleration * GlobalConfig.GetSpeedIncreasePercent();
    }

    bool IsNavMeshAgentCanMove() {
        NavMeshHit hit;
        Vector3 pos = transform.position;
        pos.y = 0.15f;
        if (NavMesh.SamplePosition(pos, out hit, 10f, NavMesh.AllAreas)) 
        {
            Debug.Log("Distance: " + Vector3.Distance(pos, hit.position));
            Debug.DrawLine(pos,  hit.position, Color.green, 1f);
            if (Vector3.Distance(pos, hit.position) > maxDistanceFromNavMeshBeforeLoss) {
                return true; 
           }
        }
        return false;
        
    }

    // private void OnEnable()
    // {
    //     // Subscribe to the NavMesh.OnNavMeshPreUpdate event
    //     NavMesh.onPreUpdate += OnNavMeshPreUpdate;
    // }

    // private void OnDisable()
    // {
    //     // Unsubscribe from the event to prevent memory leaks
    //     NavMesh.onPreUpdate -= OnNavMeshPreUpdate;
    // }

    public Vector3 FindClosestAccessiblePoint(Vector3 lineStart, Vector3 lineEnd, float maxDistance)
    {
        // Calculate the direction and length of the line
        Vector3 lineDirection = (lineEnd - lineStart).normalized;
        float lineLength = Vector3.Distance(lineStart, lineEnd);

        // Calculate the number of samples along the line
        int sampleCount = Mathf.CeilToInt(lineLength / maxDistance);
        Vector3 closestAccessiblePoint = Vector3.zero;
        float closestDistance = Mathf.Infinity;

        // Sample points along the line segment
        for (int i = 0; i <= sampleCount; i++)
        {
            Vector3 currentPoint = lineStart + lineDirection * (i * maxDistance);
            
            NavMeshHit hit;
            if (NavMesh.SamplePosition(currentPoint, out hit, 50f, NavMesh.AllAreas))
            {
                // Compute the distance from the line segment to the accessible point
                float distanceToLine = Vector3.Distance(currentPoint, hit.position);
                // Debug.Log(currentPoint + " i= " + i + " distance: " + distanceToLine + " closest:" + closestDistance);
                if (distanceToLine <= closestDistance)
                {
                    if (closestDistance - distanceToLine > closestDistanceAccuracy) 
                    {
                        closestDistance = distanceToLine;
                        closestAccessiblePoint = hit.position;
                    }
                    else if (IsFirstPathLonger(transform.position, closestAccessiblePoint, hit.position)) 
                    {
                        closestDistance = distanceToLine;
                        closestAccessiblePoint = hit.position;
                    }
                }
            }
        }

        // Optionally, check if closestAccessiblePoint is still zero (no accessible points found)
        if (closestDistance == Mathf.Infinity)
        {
            // No accessible points found, return the start point or a default value
            return lineStart;
        }

        return closestAccessiblePoint;
    }

    bool IsFirstPathLonger(Vector3 currentPosition, Vector3 firstPoint, Vector3 secondPoint) 
    {
        NavMeshPath path1 = new NavMeshPath();
        NavMeshPath path2 = new NavMeshPath();

        currentPosition = new Vector3 (currentPosition.x, firstPoint.y, currentPosition.z);

        NavMesh.CalculatePath(currentPosition, firstPoint, NavMesh.AllAreas, path1);
        NavMesh.CalculatePath(currentPosition, secondPoint, NavMesh.AllAreas, path2);
        // Debug.Log(NavMesh.CalculatePath(currentPosition, firstPoint, NavMesh.AllAreas, path1));
        // Debug.Log("curr: " + currentPosition + " firstPos: " + firstPoint + " second: " + secondPoint);
        // Debug.Log("Path1: " + CalculatePathLength(path1) + " path: " + path1 + " Path2: " + CalculatePathLength(path2) + " path: " + path2);

        return CalculatePathLength(path1) > CalculatePathLength(path2);
    }

    float CalculatePathLength(NavMeshPath navMeshPath)
    {
        float length = 0.0f;
        // Iterate over corners and sum up the distances between consecutive corners
        for (int i = 1; i < navMeshPath.corners.Length; i++)
        {
            length += Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
        }

        return length;
    }

}
