using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class CameraController : Singleton<CameraController>
{
    public Tween existingShake;

    public float currentIntensity = 0f;

    public void Shake(float intensity, float time) {

        if(intensity < currentIntensity) {
            return;
        }

        if(existingShake != null && !existingShake.IsComplete()) {
            existingShake.Complete();
        }

        currentIntensity = intensity;
        existingShake = transform.DOShakePosition(duration: time, strength: intensity, vibrato: 100).OnComplete(() => currentIntensity = 0f);
    }
}
