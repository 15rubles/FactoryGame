using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuButtonController : MonoBehaviour
{

    [SerializeField] List<GameObject> gameObjectsToHide;
    [SerializeField] List<GameObject> gameObjectsToShow;

    public void ChangeMenu()
    {
        foreach (GameObject gameObject in gameObjectsToShow)
        {
            gameObject.SetActive(true);
        }
        foreach (GameObject gameObject in gameObjectsToHide)
        {
            gameObject.SetActive(false);
        }
    }
}
