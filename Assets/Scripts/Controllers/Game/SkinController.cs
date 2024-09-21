using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SkinController : MonoBehaviour
{
    public static SkinController instance;
    [SerializeField] Dictionary<string, GameObject> characterSkins;

    [SerializeField] Dictionary<string, CharacterSkin> characterSkinsSO;
    [SerializeField] List<string> skinNames;
    [SerializeField] GameObject skinsSpawnPoint;
    [SerializeField] PlayerController playerController;

    [SerializeField] int currentSkin;

    [SerializeField] int selectedSkin;

    void Awake()
    {
        instance = this;

        List<CharacterSkin> skins = Resources.LoadAll<CharacterSkin>(Constants.skinsFolder).ToList();

        characterSkins = new Dictionary<string, GameObject>();
        characterSkinsSO = new Dictionary<string, CharacterSkin>();
        skinNames = new List<string>();

        foreach (CharacterSkin characterSkin in skins)
        {
            characterSkinsSO.Add(characterSkin.skinPrefab.name, characterSkin);
            var newSkin = Instantiate(characterSkin.skinPrefab, skinsSpawnPoint.transform.position, characterSkin.skinPrefab.transform.rotation);
            newSkin.transform.parent = playerController.gameObject.transform;
            characterSkins.Add(characterSkin.skinPrefab.name, newSkin);
            newSkin.SetActive(false);

            skinNames.Add(characterSkin.skinPrefab.name);
        }
    }

    void Start()
    {
        selectedSkin = skinNames.IndexOf(GlobalConfig.GetSelectedSkin());
        currentSkin = selectedSkin;
        characterSkins[skinNames[selectedSkin]].SetActive(true);
    }


    public void NextSkin()
    {
        if (currentSkin + 1 > skinNames.Count - 1)
        {
            characterSkins[skinNames[currentSkin]].SetActive(false);
            currentSkin = 0;
            characterSkins[skinNames[currentSkin]].SetActive(true);
        }
        else
        {
            characterSkins[skinNames[currentSkin]].SetActive(false);
            currentSkin++;
            characterSkins[skinNames[currentSkin]].SetActive(true);
        }

        SkinSelectStateButtonController.instance.ChangeCurrentSkin(characterSkinsSO[skinNames[currentSkin]]);
        SkinSelectStateButtonController.instance.ChangeButton();
    }

    public void PreviousSkin()
    {
        if (currentSkin - 1 < 0)
        {
            characterSkins[skinNames[currentSkin]].SetActive(false);
            currentSkin = skinNames.Count - 1;
            characterSkins[skinNames[currentSkin]].SetActive(true);
        }
        else
        {
            characterSkins[skinNames[currentSkin]].SetActive(false);
            currentSkin--;
            characterSkins[skinNames[currentSkin]].SetActive(true);
        }

        SkinSelectStateButtonController.instance.ChangeCurrentSkin(characterSkinsSO[skinNames[currentSkin]]);
        SkinSelectStateButtonController.instance.ChangeButton();
    }

    public CharacterSkin GetCurrentSkin()
    {
        return characterSkinsSO[skinNames[currentSkin]];
    }

    public void SetSelectedSkin(string name)
    {
        selectedSkin = skinNames.IndexOf(name);
    }

    public void SetCurrentSkiAsActive()
    {
        characterSkins[skinNames[selectedSkin]].SetActive(false);
        characterSkins[skinNames[currentSkin]].SetActive(true);
    }

    public void SetSelectedSkiAsActive()
    {
        characterSkins[skinNames[currentSkin]].SetActive(false);
        characterSkins[skinNames[selectedSkin]].SetActive(true);
    }
}
