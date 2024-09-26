using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkTapController : MonoBehaviour
{
    [SerializeField] private bool wasTouched = false;
    [SerializeField] private bool wasSet = false;
    [SerializeField] private float targetZScale = 0.5f; // The new scale on the Z-axis
    private float scalingSpeed = 9f; // Speed at which the object scales

    private Vector3 initialScale;

    void Start()
    {
        // Store the initial scale of the object
        initialScale = transform.localScale;
    }

    void Update()
    {
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

        if (wasTouched && !wasSet)
        {
            // Calculate the target scale
            Vector3 targetScale = new Vector3(initialScale.x, initialScale.y, targetZScale);

            // Lerp the scale towards the target scale
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, scalingSpeed * Time.deltaTime);

            // Check if the object has reached the target scale
            if (Mathf.Abs(transform.localScale.z - targetZScale) < 0.01f)
            {
                // Once the target scale is reached, we stop scaling
                Vector3 finalScale = transform.localScale;
                finalScale.z = targetZScale;
                transform.localScale = finalScale;
                wasSet = true;
            }
        }
    }
}
