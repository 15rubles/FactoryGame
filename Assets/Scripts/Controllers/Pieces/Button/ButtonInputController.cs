using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonInputController : MonoBehaviour
{
    [SerializeField] private bool buttonSwitch = false;

    [SerializeField] private bool wasTouched = false;

    void Update()
    {
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
                buttonSwitch = true;
                wasTouched = false;
            }
        }
    }

    public bool GetButtonSwitch() {
        return buttonSwitch;
    }
}
