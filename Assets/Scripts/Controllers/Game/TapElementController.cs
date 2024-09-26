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

    void Start()
    {
        if (transform.position.x < newXPosition)
        {
            isRight = true;
        }
        else
        {
            isRight = false;
        }
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
            if (isRight)
            {
                if (transform.position.x < newXPosition)
                {
                    if (Mathf.Abs(transform.rotation.eulerAngles.y - 180) < 10)
                    {
                        transform.Translate(Vector3.left * movingSpeed * GlobalConfig.GetSpeedMultiplied() * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(Vector3.right * movingSpeed * GlobalConfig.GetSpeedMultiplied() * Time.deltaTime);
                    }
                }
                else
                {
                    Vector3 vector = transform.position;
                    vector.x = newXPosition;
                    transform.position = vector;
                    wasSet = true;
                }
            }
            else
            {
                if (transform.position.x > newXPosition)
                {
                    if (Mathf.Abs(transform.rotation.eulerAngles.y - 180) < 10)
                    {
                        transform.Translate(Vector3.right * movingSpeed * GlobalConfig.GetSpeedMultiplied() * Time.deltaTime);
                    }
                    else
                    {
                        transform.Translate(Vector3.left * movingSpeed * GlobalConfig.GetSpeedMultiplied() * Time.deltaTime);
                    }
                }
                else
                {
                    Vector3 vector = transform.position;
                    vector.x = newXPosition;
                    transform.position = vector;
                    wasSet = true;
                }
            }
        }
    }
}
