using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreTextController : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    private float score = 0;

    [SerializeField] private float scoreMultiplier = 1;
    public static ScoreTextController instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        scoreText.text = "0";
    }

    void Update()
    {
        score += Time.deltaTime * GlobalConfig.GetSpeed() * scoreMultiplier;
        scoreText.text = Mathf.FloorToInt(score).ToString();
    }

    public int GetScore()
    {
        return Mathf.FloorToInt(score);
    }
}
