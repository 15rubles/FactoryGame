using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorScrollerController : MonoBehaviour
{
    [SerializeField] GameObject floorPieceSpawnPoint;

    void OnTriggerExit(Collider collider) {
        if (collider.tag == Constants.floorTag) {
            collider.transform.position = floorPieceSpawnPoint.transform.position;
        }
    }
}
