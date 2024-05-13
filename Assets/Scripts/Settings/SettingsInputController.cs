using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsInputController: MonoBehaviour
{
    [SerializeField]
    private SettingsPanelController settingsPanelController;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) {
            settingsPanelController.Toggle();
        }       
    }
}
