using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Vector3.forward * GlobalConfig.GetSpeedMultiplied() * Time.deltaTime);
    }
}
