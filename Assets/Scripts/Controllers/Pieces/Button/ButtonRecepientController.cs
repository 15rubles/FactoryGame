using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRecepientController : MonoBehaviour
{
    [SerializeField] private ButtonInputController buttonInputController;

    [SerializeField] private float movingSpeed = 1f;
    [SerializeField] private float offValue = 1f;
    [SerializeField] private float onValue = -1f;

    void Update()
    {
        if (buttonInputController.GetButtonSwitch()) {
            if (transform.position.y > onValue) {
                transform.Translate(Vector3.down * movingSpeed * Time.deltaTime);
            }
        } else {
            if (transform.position.y < offValue) {
                transform.Translate(Vector3.up * movingSpeed * Time.deltaTime);
            }
        }
    }
}
