
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SMAAOptionsController : HideableUIObject
{
    [Header("Other Properties")]
    [SerializeField]
    private ToggleButtonController SMAAButton;

    [SerializeField]
    private ToggleButtonController LowToggleButton;

    [SerializeField]
    private ToggleButtonController MediumToggleButton;

    [SerializeField]
    private ToggleButtonController HighToggleButton;

    protected override void Start() {

        base.Start();

        SMAAButton.OnToggle += CheckShow;

        LowToggleButton.OnToggle += (toggle) => UpdateMode(AntialiasingQuality.Low, toggle);
        MediumToggleButton.OnToggle += (toggle) => UpdateMode(AntialiasingQuality.Medium, toggle);
        HighToggleButton.OnToggle += (toggle) => UpdateMode(AntialiasingQuality.High, toggle);

        ToggleButtonController startingButton = AntiAliasingManager.Instance.CurrentQuality switch
        {
            AntialiasingQuality.Low => LowToggleButton,
            AntialiasingQuality.Medium => MediumToggleButton,
            AntialiasingQuality.High => HighToggleButton,
            _ => MediumToggleButton,
        };

        startingButton.ToggleOn();
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

        AntiAliasingManager.Instance.SetAntiAliasQuality(mode);
    }   
}
