using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPiecePrefab : MonoBehaviour
{
    [SerializeField] private GameObject newLevelPieceSpawnPoint;

    public GameObject GetNewLevelPieceSpawnPoint() {
        return newLevelPieceSpawnPoint;
    }
}
