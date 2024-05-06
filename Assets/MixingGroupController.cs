using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MixingGroupController : MonoBehaviour
{
    [SerializeField]
    private UIDelegateButtonController increaseVolumeButton;

    [SerializeField]
    private UIDelegateButtonController decreaseVolumeButton;

    [SerializeField]
    private AudioMixer audioMixer;

    [SerializeField]
    private string volumeMixerParam;

    [SerializeField]
    private VolumeUIController volumeUIController;

    public static int MIN_VOLUME_SCALED = 0;

    public static int MAX_VOLUME_SCALED = 10;

    private int currentVolumeScaled = 6;

    // Start is called before the first frame update
    void Start()
    {
        increaseVolumeButton.OnButtonClick += IncreaseVolume;
        decreaseVolumeButton.OnButtonClick += DecreaseVolume;
        
        volumeUIController.VolumeInit(currentVolumeScaled);
    }

    private void IncreaseVolume() {

        if(currentVolumeScaled.Equals(MAX_VOLUME_SCALED)) {
            return;
        }

        currentVolumeScaled++;
        audioMixer.SetFloat(volumeMixerParam, GetDBVolumeForScaledVolume(currentVolumeScaled));
        volumeUIController.VolumeIncreased(currentVolumeScaled);
    }

    private void DecreaseVolume() {

        if(currentVolumeScaled.Equals(MIN_VOLUME_SCALED)) {
            return;
        }

        currentVolumeScaled--;
        audioMixer.SetFloat(volumeMixerParam, GetDBVolumeForScaledVolume(currentVolumeScaled));
        volumeUIController.VolumeDecreased(currentVolumeScaled);
    }

    private float GetDBVolumeForScaledVolume(int scaledVolume) {
        return Mathf.Log((scaledVolume + .00001f) / MAX_VOLUME_SCALED) * 20f;
    }
}
