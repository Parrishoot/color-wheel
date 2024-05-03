
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SMAAOptionsController : HideableUIObject
{
    [SerializeField]
    private ToggleButtonController SMAAButton;

    [SerializeField]
    private ToggleButtonController LowToggleButton;

    [SerializeField]
    private ToggleButtonController MediumToggleButton;

    [SerializeField]
    private ToggleButtonController HighToggleButton;

    public AntialiasingQuality CurrentQuality { get; protected set; } = AntialiasingQuality.Medium;

    void Start() {

        SMAAButton.OnToggle += CheckShow;

        LowToggleButton.OnToggle += (toggle) => UpdateMode(AntialiasingQuality.Low, toggle);
        MediumToggleButton.OnToggle += (toggle) => UpdateMode(AntialiasingQuality.Medium, toggle);
        HighToggleButton.OnToggle += (toggle) => UpdateMode(AntialiasingQuality.High, toggle);
    }

    private void CheckShow(bool toggle) {
        if(toggle) {
            Show();
        }
        else {
            Hide();
        }
    }

    public void UpdateMode (AntialiasingQuality mode, bool toggle) {

        if(!toggle) {
            return;
        }

        CurrentQuality = mode;
        UniversalAdditionalCameraData cameraData = Camera.main.GetComponent<UniversalAdditionalCameraData>();

        if(cameraData.antialiasing != AntialiasingMode.SubpixelMorphologicalAntiAliasing) {
            return;
        }

        cameraData.antialiasingQuality = mode;
    }   
}
