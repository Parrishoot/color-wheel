using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ToggleButtonController : UIButtonController
{
    [SerializeField]
    private bool toggled = false;

    [SerializeField]
    private Material toggledMaterial;

    public Action<bool> OnToggle { get; set; }

    [SerializeField]
    private List<ToggleButtonController> linkedExclusiveToggles = new List<ToggleButtonController>();

    private void Start() {

        if(toggled) {
            ToggleOn();
        }
        else {
            ToggleOff();
        }

        foreach(ToggleButtonController toggleButtonController in linkedExclusiveToggles) {
            toggleButtonController.OnToggle += CheckToggle;
        }
    }

    public override void OnClick()
    {
        if(toggled) {
            if(linkedExclusiveToggles.Count == 0) {
                ToggleOff();   
            }
        }
        else {
            ToggleOn();
        }
    }

    private void CheckToggle(bool otherToggle) {
        
        if(!otherToggle) {
            return;
        }

        ToggleOff();
    }

    public void ToggleOn() {
        background.material = toggledMaterial;
        toggled = true;

        OnToggle?.Invoke(toggled);
    }

    public void ToggleOff() {
        background.material = buttonUnhoveredMaterial;
        toggled = false;

        OnToggle?.Invoke(toggled);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if(!toggled) {
            base.OnPointerExit(eventData);
        }
        else {
            background.material = toggledMaterial;
            textFlutterController.StopFlutter();
        }
    }
}
