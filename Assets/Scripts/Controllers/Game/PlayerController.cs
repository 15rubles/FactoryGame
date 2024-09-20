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
    [SerializeField] private float timeForPathRecalculation = 0.25f;
    [SerializeField] private float initialPlayerSpeed = 7;
    [SerializeField] private float initialPlayerAcceleration = 8;
    [SerializeField] private float maxRotationAngle = 30;
    [SerializeField] private float secondToLerpToTargetRotation = 0.3f;
    [SerializeField] private float targetRotation = 1;

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
        targetRotation = GetTargetRotation();
        ChangeRotation(GetTargetRotation());
    }

    void LateUpdate()
    {
        agent.speed = initialPlayerSpeed * GlobalConfig.GetSpeedIncreasePercent();
        agent.acceleration = initialPlayerAcceleration * GlobalConfig.GetSpeedIncreasePercent();
    }

    float ConvertVector2ToRotationAngle(Vector2 vector)
    {
        return Mathf.Rad2Deg * Mathf.Atan(vector.x / vector.y);
    }

    float GetTargetRotation()
    {
        Debug.DrawLine(transform.position, transform.position + new Vector3(agent.velocity.x, 0, agent.velocity.z) * 3);
        float velocityRotationAngle = ConvertVector2ToRotationAngle(new Vector2(agent.velocity.x, agent.velocity.z));
        Debug.Log("Angle: " + velocityRotationAngle);
        if (velocityRotationAngle > 0)
        {
            if (velocityRotationAngle > 90)
            {
                return -Mathf.Clamp(180 - velocityRotationAngle, 0, maxRotationAngle);
            }
            else
            {
                return Mathf.Clamp(velocityRotationAngle, 0, maxRotationAngle);
            }
        }
        else if (velocityRotationAngle < 0)
        {
            if (velocityRotationAngle < -90)
            {
                return -Mathf.Clamp(-180 - velocityRotationAngle, -maxRotationAngle, 0);
            }
            else
            {
                return Mathf.Clamp(velocityRotationAngle, -maxRotationAngle, 0);
            }
        }
        return 0;
    }

    void ChangeRotation(float targetRotation)
    {
        Vector3 rotation = transform.rotation.eulerAngles;
        rotation.y = Mathf.LerpAngle(rotation.y, targetRotation, 1 / secondToLerpToTargetRotation * Time.deltaTime);
        transform.eulerAngles = rotation;
    }

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

    public bool IsMovingForward()
    {
        return agent.velocity.normalized.z > 0;
    }
}
