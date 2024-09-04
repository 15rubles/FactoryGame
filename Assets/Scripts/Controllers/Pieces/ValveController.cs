using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValveController : MonoBehaviour
{
    [SerializeField] Transform valveCenter; // The center of the valve (could be the pivot point)
    [SerializeField] float minRotation = 0f; // min rotation value
    [SerializeField] float maxRotation = 90f; // max rotation value (needs to be bigger that minRotation)

    [Tooltip("if reversed is true, minRotation will mean fully open")]
    [SerializeField] bool isReversed = false;
    
    [SerializeField] float percentOfOpening;

    float currentRotation;
    private Vector2 lastDirection;
    private bool wasTouched = false;
    private Vector2 valveCenterV2;

    private void Start()
    {
        valveCenterV2 = new Vector2(valveCenter.position.x, valveCenter.position.z);
        currentRotation = transform.localEulerAngles.y;
    }

    void Update()
    {
        percentOfOpening = GetPercentOfOpening();
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    valveCenterV2 = new Vector2(valveCenter.position.x, valveCenter.position.z);
                    lastDirection = (new Vector2(hit.point.x, hit.point.z) - valveCenterV2).normalized;
                    
                    wasTouched = true;
                }
            }
            if (touch.phase == TouchPhase.Moved && wasTouched)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Vector2 direction = (new Vector2(hit.point.x, hit.point.z) - valveCenterV2).normalized;

                    float angle = Vector2.SignedAngle(direction, lastDirection);
                    if(Mathf.Abs(angle) <= 100f) {
                        currentRotation += angle;
                    }

                    currentRotation = Mathf.Clamp(currentRotation, minRotation, maxRotation);
                    transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, currentRotation, transform.localEulerAngles.z);
                    
                    lastDirection = direction;
                }
            }
            if (touch.phase == TouchPhase.Ended)
            {
                wasTouched = false;
            }
        }
    }

    public float GetPercentOfOpening() {
        float percent = (currentRotation-minRotation) / (maxRotation - minRotation);
        return isReversed ? 1 - percent : percent;
    }
}
