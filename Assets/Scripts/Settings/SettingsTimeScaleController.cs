using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SettingsTimeScaleController : MonoBehaviour
{
    [SerializeField]
    private float transitionTime = .5f;

    [SerializeField]
    private SettingsPanelController settingsPanelController;

    private Tween tween;

    // Start is called before the first frame update
    void Start()
    {
        settingsPanelController.OnHide += SpeedUp;
        settingsPanelController.OnShow += SlowDown;
    }

    private void SlowDown() {
        TransitionTimeScale(0f, transitionTime);
    }

    private void SpeedUp() {
        TransitionTimeScale(1f, transitionTime * 3);
    }

    private void TransitionTimeScale(float targetTimescale, float totalTransitionTime) {
        tween?.Complete();
        tween = DOTween.To(() => Time.timeScale, x => Time.timeScale = x, targetTimescale, totalTransitionTime).SetEase(Ease.InOutSine).SetUpdate(true);
    }
}
