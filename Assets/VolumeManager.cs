using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeManager : Singleton<VolumeManager>
{
    public enum MixingGroup {
        GAME_FX,
        MUSIC,
        MASTER
    }

    [SerializeField]
    private SerializeDict<MixingGroup, int> startingValues;
    
    [SerializeField]
    private AudioMixer audioMixer;

    public Dictionary<MixingGroup, int> MixingGroupVolumes { get; protected set; }

    public Action<MixingGroup> OnVolumeIncreased { get; set; }

    public Action<MixingGroup> OnVolumeDecreased { get; set; }
    
    public static int MIN_VOLUME_SCALED = 0;

    public static int MAX_VOLUME_SCALED = 10;

    protected override void Awake()
    {
        base.Awake();
        
        MixingGroupVolumes = startingValues.ToDict();
    }

    void Start() {
        foreach(MixingGroup mixingGroup in MixingGroupVolumes.Keys) {
            audioMixer.SetFloat(mixingGroup.GetParameterName(), GetDBVolumeForScaledVolume(MixingGroupVolumes[mixingGroup]));
        }
    }

    public void IncreaseVolume(MixingGroup mixingGroup) {

        if(MixingGroupVolumes[mixingGroup].Equals(MAX_VOLUME_SCALED)) {
            return;
        }

        MixingGroupVolumes[mixingGroup]++;
        audioMixer.SetFloat(mixingGroup.GetParameterName(), GetDBVolumeForScaledVolume(MixingGroupVolumes[mixingGroup]));
        
        OnVolumeIncreased?.Invoke(mixingGroup);
    }

    public void DecreaseVolume(MixingGroup mixingGroup) {

        if(MixingGroupVolumes[mixingGroup].Equals(MIN_VOLUME_SCALED)) {
            return;
        }

        MixingGroupVolumes[mixingGroup]--;
        audioMixer.SetFloat(mixingGroup.GetParameterName(), GetDBVolumeForScaledVolume(MixingGroupVolumes[mixingGroup]));
        
        OnVolumeDecreased?.Invoke(mixingGroup);
    }

    
    private float GetDBVolumeForScaledVolume(int scaledVolume) {
        return Mathf.Log((scaledVolume + .00001f) / MAX_VOLUME_SCALED) * 20f;
    }

    private float GetScaledVolumeForDB(int scaledVolume) {
        return Mathf.Log((scaledVolume + .00001f) / MAX_VOLUME_SCALED) * 20f;
    }
}
