using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundContoller : Singleton<SoundContoller>
{
    public AudioMixer audioMixer;

    public AudioSource BGM;
    public AudioSource ButtonSound;
    public AudioSource PauseSound;
    public AudioSource PlayStartSound;

    [Header("Setting")]
    public Toggle BGMToggle;
    public Toggle SFXToggle;
    public Toggle UIToggle;


    private void Start()
    {
        StartInit();

        BGMToggle.onValueChanged.AddListener(delegate { SetBGMVolume(BGMToggle.isOn); PlayButtonSound(); });
        SFXToggle.onValueChanged.AddListener(delegate { SetSFXVolume(SFXToggle.isOn); PlayButtonSound(); });
        UIToggle.onValueChanged.AddListener(delegate { SetUIVolume(UIToggle.isOn); PlayButtonSound(); });
    }

    void StartInit()
    {
        BGMToggle.isOn = PlayerPrefsX.GetBool("BGMVolume", true);
        SFXToggle.isOn = PlayerPrefsX.GetBool("SFXVolume", true);
        UIToggle.isOn = PlayerPrefsX.GetBool("UIVolume", true);
        SetBGMVolume(BGMToggle.isOn);
        SetSFXVolume(SFXToggle.isOn);
        SetUIVolume(UIToggle.isOn);
    }

    void SetBGMVolume(bool IsOn)
    {
        audioMixer.SetFloat("BGMVolume", (IsOn ? 0 : 1) * -80f);
        PlayerPrefsX.SetBool("BGMVolume", IsOn);
    }

    void SetSFXVolume(bool IsOn)
    {
        audioMixer.SetFloat("SFXVolume", (IsOn ? 0 : 1) * -80f);
        PlayerPrefsX.SetBool("SFXVolume", IsOn);
    }

    void SetUIVolume(bool IsOn)
    {
        audioMixer.SetFloat("UIVolume", (IsOn ? 0 : 1) * -80f);
        PlayerPrefsX.SetBool("UIVolume", IsOn);
    }

    public void PlayButtonSound()
    {
        ButtonSound.Play();
    }
}
