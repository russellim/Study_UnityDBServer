using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SettingController : MonoBehaviour
{
    public RectTransform HPRecttransform;
    public HorizontalLayoutGroup HPLayoutGroup;

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
        if (HPRecttransform)
        {
            int temp = IsOn ? 0 : 1;
            HPRecttransform.anchorMin = new Vector2(temp, 0);
            HPRecttransform.anchorMax = new Vector2(temp, 0);
            HPRecttransform.pivot = new Vector2(temp, 0);
            HPRecttransform.anchoredPosition = new Vector2(IsOn ? 10 : -10, HPRecttransform.position.y);
            if (IsOn) HPLayoutGroup.childAlignment = TextAnchor.UpperLeft;
            else HPLayoutGroup.childAlignment = TextAnchor.UpperRight;
        }
    }
    public void PlayButtonSound()
    {
        SoundContoller.Instance.ButtonSound.Play();
    }
}
