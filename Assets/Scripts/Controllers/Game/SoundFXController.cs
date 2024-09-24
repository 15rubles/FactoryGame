using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundFXController : MonoBehaviour
{
    [SerializeField] private AudioSource soundFXObject;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float soundVolume;
    public static SoundFXController instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        audioMixer.SetFloat(Constants.musicMixerName, PlayerPrefs.GetFloat(Constants.musicMixerName, 0));
        audioMixer.SetFloat(Constants.soundMixerName, PlayerPrefs.GetFloat(Constants.soundMixerName, 0));
    }


    public void PlaySound(AudioClip audioClip, Transform spawnPoint)
    {
        AudioSource audioSource = Instantiate(soundFXObject, spawnPoint.position, Quaternion.identity);

        audioSource.clip = audioClip;

        audioSource.volume = soundVolume;

        audioSource.Play();

        float clipLength = audioClip.length;

        Destroy(audioSource, clipLength);
    }

    public void MusicButton()
    {
        audioMixer.GetFloat(Constants.musicMixerName, out float musicMixerValue);
        if (musicMixerValue < -1f)
        {
            audioMixer.SetFloat(Constants.musicMixerName, 0);
            PlayerPrefs.SetFloat(Constants.musicMixerName, 0);
        }
        else
        {
            audioMixer.SetFloat(Constants.musicMixerName, -80);
            PlayerPrefs.SetFloat(Constants.musicMixerName, -80);
        }
    }

    public void SoundButton()
    {
        audioMixer.GetFloat(Constants.soundMixerName, out float soundMixerValue);
        if (soundMixerValue < -1f)
        {
            audioMixer.SetFloat(Constants.soundMixerName, 0);
            PlayerPrefs.SetFloat(Constants.soundMixerName, 0);
        }
        else
        {
            audioMixer.SetFloat(Constants.soundMixerName, -80);
            PlayerPrefs.SetFloat(Constants.soundMixerName, -80);
        }
    }

    public float GetValueFromMixer(string mixerName)
    {
        audioMixer.GetFloat(mixerName, out float mixerValue);
        return mixerValue;
    }

}
