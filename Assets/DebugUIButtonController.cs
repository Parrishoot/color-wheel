using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugUIButtonController : UIButtonController
{
    public override void OnClick()
    {
        Debug.Log("Click!");
    }
}
