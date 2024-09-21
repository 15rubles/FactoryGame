using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelectStateButtonController : MonoBehaviour
{
    public static SkinSelectStateButtonController instance;

    [SerializeField] Image image;
    [SerializeField] Sprite selectedSprite;
    [SerializeField] Sprite selectSprite;
    [SerializeField] Sprite buySprite;
    [SerializeField] GameObject selectedGO;
    [SerializeField] GameObject selectGO;
    [SerializeField] GameObject buyGO;
    [SerializeField] TMP_Text priceText;
    [SerializeField] CharacterSkin currentSkin;


    void Awake()
    {
        instance = this;
    }

    void OnEnable()
    {
        SkinController.instance.SetCurrentSkiAsActive();
        currentSkin = SkinController.instance.GetCurrentSkin();
        ChangeButton();
    }

    void OnDisable()
    {
        SkinController.instance.SetSelectedSkiAsActive();
    }

    public void ChangeCurrentSkin(CharacterSkin currentSkin)
    {
        this.currentSkin = currentSkin;
    }

    public void ChangeButton()
    {
        switch (GlobalConfig.GetSkinState(currentSkin.skinPrefab.name))
        {
            case (int)CharacterSkinState.NotPurchased:
                image.sprite = buySprite;
                selectedGO.SetActive(false);
                selectGO.SetActive(false);
                buyGO.SetActive(true);
                priceText.text = currentSkin.price.ToString();
                break;
            case (int)CharacterSkinState.Purchased:
                if (GlobalConfig.GetSelectedSkin().Equals(currentSkin.skinPrefab.name))
                {
                    image.sprite = selectedSprite;
                    selectedGO.SetActive(true);
                    selectGO.SetActive(false);
                    buyGO.SetActive(false);
                }
                else
                {
                    image.sprite = selectSprite;
                    selectedGO.SetActive(false);
                    selectGO.SetActive(true);
                    buyGO.SetActive(false);
                }
                break;
        }
    }

    public void ButtonClick()
    {
        switch (GlobalConfig.GetSkinState(currentSkin.skinPrefab.name))
        {
            case (int)CharacterSkinState.NotPurchased:
                if (GlobalConfig.GetTotalCoins() >= currentSkin.price)
                {
                    GlobalConfig.UpdateTotalCoins(-currentSkin.price);
                    GlobalConfig.SetSkinState(currentSkin.skinPrefab.name, CharacterSkinState.Purchased);
                    TotalCoinsTextController.instance.UpdateText();
                    GlobalConfig.SetSelectedSkin(currentSkin.skinPrefab.name);
                    SkinController.instance.SetSelectedSkin(currentSkin.skinPrefab.name);
                }
                break;
            case (int)CharacterSkinState.Purchased:
                if (!GlobalConfig.GetSelectedSkin().Equals(currentSkin.skinPrefab.name))
                {
                    GlobalConfig.SetSelectedSkin(currentSkin.skinPrefab.name);
                    SkinController.instance.SetSelectedSkin(currentSkin.skinPrefab.name);
                }
                break;
        }
        ChangeButton();
    }
}
