using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.Rendering.Universal;

public class AntiAliasingManager : Singleton<AntiAliasingManager>
{
    [field:SerializeReference]
    public AntialiasingMode CurrentAntiAliasingMode { get; protected set; } = AntialiasingMode.SubpixelMorphologicalAntiAliasing;

    [field:SerializeReference]
    public AntialiasingQuality CurrentQuality { get; protected set; } = AntialiasingQuality.Medium;

    public void SetAntiAliasMode(AntialiasingMode newMode) {

        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>(); 
        cameraData.antialiasing = newMode;

        CurrentAntiAliasingMode = newMode;

        if(newMode == AntialiasingMode.SubpixelMorphologicalAntiAliasing) {
            cameraData.antialiasingQuality = CurrentQuality;
        }
    }

    public void SetAntiAliasQuality(AntialiasingQuality newQuality) {
        
        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>(); 
        cameraData.antialiasingQuality = newQuality;
        
        CurrentQuality = newQuality;
    }
}
