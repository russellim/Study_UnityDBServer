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
        BGMToggle.onValueChanged.AddListener(delegate { SetBGMVolume(BGMToggle.isOn); });
        SFXToggle.onValueChanged.AddListener(delegate { SetSFXVolume(SFXToggle.isOn); });
        UIToggle.onValueChanged.AddListener(delegate { SetUIVolume(UIToggle.isOn); });

        BGMToggle.isOn = PlayerPrefsX.GetBool("BGMVolume", true);
        SFXToggle.isOn = PlayerPrefsX.GetBool("SFXVolume", true);
        UIToggle.isOn = PlayerPrefsX.GetBool("UIVolume", true);
    }

    void SetBGMVolume(bool IsOn)
    {
        audioMixer.SetFloat("BGMVolume", (IsOn ? 0 : 1) * -80f);
        PlayerPrefsX.SetBool("BGMVolume", IsOn);
        ButtonSound.Play();
    }

    void SetSFXVolume(bool IsOn)
    {
        audioMixer.SetFloat("SFXVolume", (IsOn ? 0 : 1) * -80f);
        PlayerPrefsX.SetBool("SFXVolume", IsOn);
        ButtonSound.Play();
    }

    void SetUIVolume(bool IsOn)
    {
        audioMixer.SetFloat("UIVolume", (IsOn ? 0 : 1) * -80f);
        PlayerPrefsX.SetBool("UIVolume", IsOn);
        ButtonSound.Play();
    }
}
