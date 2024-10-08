using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinsTextController : MonoBehaviour
{
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text coinsTextShadow;
    private int coins;

    public static CoinsTextController instance;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        coinsText.text = coins.ToString();
        coinsTextShadow.text = coins.ToString();
    }

    public void UpdateCoins(int valueToAdd)
    {
        coins += valueToAdd;
        coinsText.text = coins.ToString();
        coinsTextShadow.text = coins.ToString();
    }


    public int GetCoins()
    {
        return coins;
    }
}
