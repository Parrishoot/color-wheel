using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDelegateButtonController : UIButtonController
{
    public Action OnButtonClick { get; set; }
    
    public override void OnClick()
    {
        OnButtonClick?.Invoke();
    }
}
