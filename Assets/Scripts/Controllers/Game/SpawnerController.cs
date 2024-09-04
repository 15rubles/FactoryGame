using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpewnerController : MonoBehaviour
{

    [SerializeField] private List<GameObject> levelPieces;
    [SerializeField] private bool spawnNext = true;
    

    void Start()
    {
        levelPieces = Resources.LoadAll<GameObject>(Constants.levelPiecesFolder).ToList();
    }

    void Update()
    {
        if (spawnNext) {
            GameObject levelPiece = levelPieces[Random.Range(0, levelPieces.Count)];
            Instantiate(levelPiece, transform.position, Quaternion.identity);
            spawnNext = false;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.tag == Constants.spawnPointTag) {
            spawnNext = true;
        }
    }
}
