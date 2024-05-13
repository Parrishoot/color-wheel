using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static VolumeManager;

public static class MixingGroupExtensions
{
    public static string GetParameterName(this MixingGroup mixingGroup) {
        return mixingGroup switch
        {
            MixingGroup.GAME_FX => "GameFXVolume",
            MixingGroup.MUSIC => "MusicVolume",
            MixingGroup.MASTER => "MasterVolume",
            _ => "MaterVolume",
        };
    }
}
