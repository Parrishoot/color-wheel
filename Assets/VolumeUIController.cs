using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class VolumeUIController : MonoBehaviour
{

    [SerializeField]
    private List<Image> dots;

    [SerializeField]
    private float transitionTime = .2f;

    [SerializeField]
    private float transitionElasticity = .2f;

    [SerializeField]
    private Color onColor;

    [SerializeField]
    private Color offColor;

    public void VolumeInit(int currentVolume) {
        for(int i = 1; i <= MixingGroupController.MAX_VOLUME_SCALED; i++) {
            if(i > currentVolume) {
                dots[i].gameObject.transform.localScale = Vector3.one;
                dots[i].color = offColor;
            }
            else {
                dots[i].gameObject.transform.localScale = Vector3.one * 2;
                dots[i].color = onColor;
            }
        }
    }

    public void VolumeIncreased(int currentVolume) {
        DOTween.Sequence()
            .Append(dots[currentVolume].transform.DOScale(Vector3.one * 2, transitionTime).SetEase(Ease.InOutBack, overshoot: transitionElasticity))
            .Join(dots[currentVolume].DOColor(onColor, transitionTime).SetEase(Ease.InOutCubic))
            .SetUpdate(true)
            .Play();
    }

    public void VolumeDecreased(int currentVolume) {
        DOTween.Sequence()
            .Append(dots[currentVolume + 1].transform.DOScale(Vector3.one, transitionTime).SetEase(Ease.InOutBack, overshoot: transitionElasticity))
            .Join(dots[currentVolume + 1].DOColor(offColor, transitionTime).SetEase(Ease.InOutCubic))
            .SetUpdate(true)
            .Play();
    }
}
