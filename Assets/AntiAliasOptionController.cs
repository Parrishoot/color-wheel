using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AntiAliasOptionController : MonoBehaviour
{
    [SerializeField]
    private ToggleButtonController fxaaButton;

    [SerializeField]
    private ToggleButtonController smaaButton;

    [SerializeField]
    private SMAAOptionsController smaaOptionsController;
    

    // Start is called before the first frame update
    void Start()
    {
        fxaaButton.OnToggle += ToggleFXAAOn;
        smaaButton.OnToggle += ToggleSMAAOn;
    }

    private void ToggleFXAAOn(bool toggled) {

        if(!toggled) {
            return;
        }

        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>(); 
        cameraData.antialiasing = AntialiasingMode.FastApproximateAntialiasing;
    }

    private void ToggleSMAAOn(bool toggled) {

        if(!toggled) {
            return;
        }

        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>(); 
        cameraData.antialiasing = AntialiasingMode.SubpixelMorphologicalAntiAliasing;
        cameraData.antialiasingQuality = smaaOptionsController.CurrentQuality;
    }
}
