using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingPieceController : MonoBehaviour
{

    [SerializeField] private List<Vector3> rotationSteps;
    [SerializeField] private int currentStep = 360;
    
    [SerializeField] private float rotationSpeed = 1f;
    private bool wasTouched = false;

    void Update()
    {
        RotateToCurrentStep();
        
        CheckButtonPress();
    }

    void CheckButtonPress() {
        if (Input.touchCount > 0)
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

            if (touch.phase == TouchPhase.Ended && wasTouched)
            {
                wasTouched = false;
                if (rotationSteps.Count > currentStep + 1)
                {
                    currentStep++;
                }
                else
                {
                    currentStep = 0;
                }
            }
        }
    }

    void RotateToCurrentStep() {
        Quaternion targetRotation = Quaternion.Euler(rotationSteps[currentStep]);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            GlobalConfig.GetSpeedMultiplied() * rotationSpeed * Time.deltaTime);
    }
}