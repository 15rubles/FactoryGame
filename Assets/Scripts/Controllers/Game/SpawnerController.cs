using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{

    [SerializeField] private List<GameObject> levelPieces;

    [SerializeField] private List<GameObject> objectsInGame;
    [SerializeField] private bool spawnNext = true;

    public static SpawnerController instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        levelPieces = Resources.LoadAll<GameObject>(Constants.levelPiecesFolder).ToList();
    }

    void Update()
    {
        if (spawnNext) {
            GameObject levelPiece = levelPieces[Random.Range(0, levelPieces.Count)];
            objectsInGame.Add(Instantiate(levelPiece, transform.position, Quaternion.identity));
            spawnNext = false;
        }
    }

    void OnTriggerExit(Collider collider) {
        if (collider.tag == Constants.spawnPointTag) {
            spawnNext = true;
        }
    }

    public void RemoveFirstElementOfObjectsInGame()
    {
        objectsInGame.RemoveAt(0);
    }


    public void ResetSpawner()
    {
        RemoveObjectsInGame();
        spawnNext = true;
    }

    void RemoveObjectsInGame()
    {
        foreach (GameObject objectInGame in objectsInGame)
        {
            Destroy(objectInGame);
        }
        objectsInGame.Clear();
    }
}
