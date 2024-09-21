using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalCoinsTextController : MonoBehaviour
{
    [SerializeField] private TMP_Text totalCoinsText;

    public static TotalCoinsTextController instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        totalCoinsText.text = GlobalConfig.GetTotalCoins().ToString();
    }

    void OnEnable()
    {
        totalCoinsText.text = GlobalConfig.GetTotalCoins().ToString();
    }

    public void UpdateText()
    {
        totalCoinsText.text = GlobalConfig.GetTotalCoins().ToString();
    }
}
