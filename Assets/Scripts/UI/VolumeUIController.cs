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
    private UIDelegateButtonController increaseVolumeButton;

    [SerializeField]
    private UIDelegateButtonController decreaseVolumeButton;

    [SerializeField]
    private Color onColor;

    [SerializeField]
    private Color offColor;

    [SerializeField]
    private VolumeManager.MixingGroup mixingGroup;

    void Start() {
        VolumeInit();

        VolumeManager volumeManager = VolumeManager.Instance;

        increaseVolumeButton.OnButtonClick += () => volumeManager.IncreaseVolume(mixingGroup);
        decreaseVolumeButton.OnButtonClick += () => volumeManager.DecreaseVolume(mixingGroup);

        volumeManager.OnVolumeIncreased += CheckVolumeIncreased; 
        volumeManager.OnVolumeDecreased += CheckVolumeDecreased; 
    }

    private void CheckVolumeIncreased(VolumeManager.MixingGroup mixingGroup) {

        if(this.mixingGroup != mixingGroup) {
            return;
        }

        VolumeIncreased(VolumeManager.Instance.MixingGroupVolumes[mixingGroup]);
    }

    private void CheckVolumeDecreased(VolumeManager.MixingGroup mixingGroup) {

        if(this.mixingGroup != mixingGroup) {
            return;
        }

        VolumeDecreased(VolumeManager.Instance.MixingGroupVolumes[mixingGroup]);
    }

    public void VolumeInit() {

        VolumeManager volumeManager = VolumeManager.Instance;

        for(int i = 1; i <= VolumeManager.MAX_VOLUME_SCALED; i++) {
            if(i > volumeManager.MixingGroupVolumes[mixingGroup]) {
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

    private void OnDestroy() {
        VolumeManager.Instance.OnVolumeIncreased -= CheckVolumeIncreased; 
        VolumeManager.Instance.OnVolumeDecreased -= CheckVolumeDecreased; 
    }
}
