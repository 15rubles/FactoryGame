using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapElementController : MonoBehaviour
{
    [SerializeField] private bool wasTouched = false;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private float movingSpeed = 1f;

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

        if (wasTouched)
        {
            transform.Translate(newPosition * movingSpeed * GlobalConfig.GetSpeedMultiplied() * Time.deltaTime);
        }
    }
}
