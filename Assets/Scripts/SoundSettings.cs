using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundSettings : MonoBehaviour
{

    [SerializeField] Slider slider;

    private void Start()
    {
        SoundManager.Instance.PlayMusic("wait");
    }

    public void SetVolume(float value)
    {
        if (value < 1)
        {
            value = 0.01f;
        }
        RefreshSlider(value);
        SoundManager.Instance.MusicSource.volume = value;
    }

    public void SetVolumeFromSlider()
    {
        SetVolume(slider.value);
    }

    public void RefreshSlider(float value)
    {
        slider.value = value;
    }
}
