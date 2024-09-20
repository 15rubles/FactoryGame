using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    [SerializeField] GameObject resetFloorPoint;
    [SerializeField] GameObject floorPieceSpawnPoint;
    void Update()
    {
        if (Time.timeScale > 0)
        {
            transform.Translate(Vector3.forward * GlobalConfig.GetSpeedMultiplied() * Time.deltaTime);
        }
        else if (!GlobalConfig.GetOnPause())
        {
            transform.Translate(Vector3.forward * GlobalConfig.GetSpeedMultiplied() * Time.unscaledDeltaTime);
        }
        if (transform.position.z < resetFloorPoint.transform.position.z)
        {
            gameObject.transform.position = floorPieceSpawnPoint.transform.position;
        }
    }
}
