using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIButtonCallbackController : MonoBehaviour
{
    [SerializeField]
    protected UIDelegateButtonController buttonController;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        buttonController.OnButtonClick += OnButtonClick;
    }

    protected abstract void OnButtonClick();
}
