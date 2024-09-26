using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapElementController : MonoBehaviour
{
    [SerializeField] private bool wasTouched = false;
    [SerializeField] private bool wasSet = false;
    [SerializeField] private float newXPosition;
    private float movingSpeed = 4f;

    [SerializeField] private bool isRight;

    private Vector3 targetPosition;

    void Start()
    {
        // Determine if the object should move to the right or left
        isRight = transform.position.x < newXPosition;

        // Set the target position
        targetPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);
    }

    void Update()
    {
        // Check for touch input
        if (Input.touchCount > 0 && !wasTouched)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    wasTouched = true;
                }
            }
        }

        // If the object was touched and not yet set
        if (wasTouched && !wasSet)
        {
            targetPosition = new Vector3(newXPosition, transform.position.y, transform.position.z);
            // Smoothly move the object towards the target position using Lerp
            transform.position = Vector3.Lerp(transform.position, targetPosition, movingSpeed * Time.deltaTime * GlobalConfig.GetSpeedMultiplied());

            // Check if the object has reached the target position
            if (Mathf.Abs(transform.position.x - newXPosition) < 0.01f)
            {
                // Snap to the exact target position
                Vector3 finalPosition = transform.position;
                finalPosition.x = newXPosition;
                transform.position = finalPosition;
                wasSet = true;
            }
        }
    }
}
