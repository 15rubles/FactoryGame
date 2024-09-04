using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragObjectController : MonoBehaviour
{
    [SerializeField] float minX = -5f; // Minimum X value
    [SerializeField] float maxX = 5f;  // Maximum X value
    [SerializeField] Vector3 worldDelta;

    [SerializeField] private Vector2 touchStartPos;
    [SerializeField] private bool isDragging = false;


    void Update()
    {
        // Handle touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Convert touch position to world point
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Check if the touch is on the object
                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    isDragging = true;
                    touchStartPos = touch.position;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                // Calculate the difference between the initial touch position and the current position
                Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(touchStartPos.x, 0, 0));
                Vector3 worldCurrent = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, 0));

                float newPos = (worldCurrent - worldStart).x + transform.position.x;
                touchStartPos = touch.position;

                // Clamp the position within the minX and maxX range
                newPos = Mathf.Clamp(newPos, minX, maxX);

                // Update the object's position
                transform.position = new Vector3(newPos, transform.position.y, transform.position.z);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
    }
}
