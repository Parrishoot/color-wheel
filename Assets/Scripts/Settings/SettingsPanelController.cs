using System;
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
    private List<UIButtonController> buttonsToDisable;

    protected override void Start() {
        base.Start();

        if(openSettingsButton != null) {
            openSettingsButton.OnButtonClick += Show;
        }

        closeSettingsButton.OnButtonClick += Hide;
    }

    public override void Show() {
        
        foreach(UIButtonController button in buttonsToDisable) {
                button.OnPointerExit(null);
                button.enabled = false;
        }

        base.Show();
    }

    public override void Hide()
    {
         foreach(UIButtonController button in buttonsToDisable) {
                button.OnPointerExit(null);
                button.enabled = true;
        }

        base.Hide();
    }
}
