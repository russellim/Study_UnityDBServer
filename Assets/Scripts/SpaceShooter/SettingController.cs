using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingController : MonoBehaviour
{
    public RectTransform HPCountRecttransform;
    public HorizontalLayoutGroup HPCountLayoutGroup;
    public RectTransform SpecialCountRecttransform;
    public HorizontalLayoutGroup SpecialCountLayoutGroup;
    public RectTransform SpecialButtonRecttransform;

    [Header("Setting")]
    //오른쪽기준.
    public Toggle HandedToggle;

    private void Start()
    {
        StartInit();

        HandedToggle.onValueChanged.AddListener(delegate { SetHanded(HandedToggle.isOn); PlayButtonSound(); });
    }

    void StartInit()
    {
        HandedToggle.isOn = PlayerPrefsX.GetBool("Handed", true);
        SetHanded(HandedToggle.isOn);
    }

    void SetHanded(bool IsOn)
    {
        PlayerPrefsX.SetBool("Handed", IsOn);
        if (HPCountRecttransform)
        {
            SetPosition(HPCountRecttransform, HPCountLayoutGroup, !IsOn);
            SetPosition(SpecialCountRecttransform, SpecialCountLayoutGroup, !IsOn);
            SetPosition(SpecialButtonRecttransform, null, IsOn);
        }
    }
    void SetPosition(RectTransform rt, HorizontalLayoutGroup lg, bool IsOn)
    {
        int temp = IsOn ? 0 : 1;
        rt.anchorMin = new Vector2(temp, 0);
        rt.anchorMax = new Vector2(temp, 0);
        rt.pivot = new Vector2(temp, 0);
        rt.anchoredPosition = new Vector2(IsOn ? 10 : -10, rt.position.y);
        if(lg)
        {
            if (IsOn) lg.childAlignment = TextAnchor.UpperLeft;
            else lg.childAlignment = TextAnchor.UpperRight;
        }
    }

    public void PlayButtonSound()
    {
        SoundContoller.Instance.ButtonSound.Play();
    }
}
