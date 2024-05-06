using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanelController : HideableUIObject
{
    [Header("Other Properties")]

    [SerializeField]
    private UIDelegateButtonController openSettingsButton;

    [SerializeField]
    private UIDelegateButtonController closeSettingsButton;

    [SerializeField]
    List<UIButtonController> buttonsToDisable;

    protected override void Start() {
        base.Start();

        openSettingsButton.OnButtonClick += () => {

            foreach(UIButtonController button in buttonsToDisable) {
                button.OnPointerExit(null);
                button.enabled = false;
            }

            Show();
        };

        closeSettingsButton.OnButtonClick += () => { 

            foreach(UIButtonController button in buttonsToDisable) {
                button.OnPointerExit(null);
                button.enabled = true;
            }

            Hide();
        };
    }
}
