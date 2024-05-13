using DG.Tweening;
using UnityEngine;

public class CameraController : Singleton<CameraController>
{
    public Tween existingShake;

    public float currentIntensity = 0f;

    public Vector3 startingPos;

    public void Start() {
        startingPos = transform.position;
    } 

    public void Shake(float intensity, float time) {

        if(intensity < currentIntensity) {
            return;
        }

        if(existingShake != null && !existingShake.IsActive()) {
            existingShake.Complete();
            transform.position = startingPos;
        }

        currentIntensity = intensity;
        existingShake = transform.DOShakePosition(duration: time, strength: intensity, vibrato: 100).OnComplete(() => currentIntensity = 0f);
    }
}
