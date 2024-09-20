using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLevelPieceController : MonoBehaviour
{
    void OnTriggerExit(Collider collider) {
        if (collider.tag == Constants.spawnPointTag) {
            Destroy(collider.transform.parent.gameObject);
            SpawnerController.instance.RemoveFirstElementOfObjectsInGame();
        }
    }
}
