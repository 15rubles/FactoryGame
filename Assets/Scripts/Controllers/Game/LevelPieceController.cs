using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPieceController : MonoBehaviour
{
     void Update()
    {
        transform.Translate(Vector3.back * GlobalConfig.GetSpeed() * Time.deltaTime);
    }
}
