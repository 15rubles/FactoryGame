using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalCoinsTextController : MonoBehaviour
{
    [SerializeField] private TMP_Text totalCoinsText;

    void Start()
    {
        totalCoinsText.text = GlobalConfig.GetTotalCoins().ToString();
    }

    void OnEnable()
    {
        totalCoinsText.text = GlobalConfig.GetTotalCoins().ToString();
    }
}
