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

        AntialiasingMode startingMode = AntiAliasingManager.Instance.CurrentAntiAliasingMode;

        if(startingMode == AntialiasingMode.FastApproximateAntialiasing) {
            fxaaButton.ToggleOn();
        } 
        else {
            smaaButton.ToggleOn();
        }
    }

    private void ToggleFXAAOn(bool toggled) {
        
        if(!toggled) {
            return;
        }

        AntiAliasingManager.Instance.SetAntiAliasMode(AntialiasingMode.FastApproximateAntialiasing);
    }

    private void ToggleSMAAOn(bool toggled) {
        
        if(!toggled) {
            return;
        }

        AntiAliasingManager.Instance.SetAntiAliasMode(AntialiasingMode.SubpixelMorphologicalAntiAliasing);
    }
}
