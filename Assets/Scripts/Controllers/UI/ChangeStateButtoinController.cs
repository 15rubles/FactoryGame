using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChangeStateButtoinController : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite onSprite;
    [SerializeField] private Sprite offSprite;
    [SerializeField] private GameObject disabled;

    [SerializeField] private string mixerName;


    void OnEnable()
    {
        UpdateButton();
    }

    public void UpdateButton()
    {
        Debug.Log(SoundFXController.instance.GetValueFromMixer(mixerName));
        if (SoundFXController.instance.GetValueFromMixer(mixerName) < -1f)
        {
            image.sprite = offSprite;
            disabled.SetActive(true);
        }
        else
        {
            image.sprite = onSprite;
            disabled.SetActive(false);
        }
    }
}
