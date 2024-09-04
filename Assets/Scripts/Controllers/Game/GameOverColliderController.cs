using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverColliderController : MonoBehaviour
{
    void OnTriggerEnter(Collider collider) {
        if (collider.tag == Constants.playerTag) {
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
    }
}
