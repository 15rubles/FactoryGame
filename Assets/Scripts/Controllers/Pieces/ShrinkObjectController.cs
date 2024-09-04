using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkObjectController : MonoBehaviour
{
    [SerializeField] float minScale = 1f; // Minimum Z value
    [SerializeField] float maxScale = 5f;  // Maximum Z value
    [SerializeField] float scaleSpeed = 0.15f;  // Maximum Z value
    [SerializeField] Vector3 worldDelta;
    [SerializeField] float qwe = 5f;

    [SerializeField] private float hitStartZ;
    [SerializeField] private bool isDragging = false;
    [SerializeField] private bool isFlipped = false;


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
                    hitStartZ = hit.point.z;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                // Check if the touch is on the object
                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log("Start: " + hitStartZ + " End: " + hit.point.z);
                    float newScale = (hitStartZ - hit.point.z) * (isFlipped ? -1 : 1) * scaleSpeed + transform.localScale.z;
                    qwe = hitStartZ - hit.point.z;
                    hitStartZ = hit.point.z;

                    // Clamp the position within the minX and maxX range
                    newScale = Mathf.Clamp(newScale, minScale, maxScale);

                    // Update the object's position
                    transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, newScale);
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isDragging = false;
            }
        }
    }
}
