using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreTextController : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    void Start()
    {
        highScoreText.text = GlobalConfig.GetHighScore().ToString();
    }

    void OnEnable()
    {
        highScoreText.text = GlobalConfig.GetHighScore().ToString();
    }
}
