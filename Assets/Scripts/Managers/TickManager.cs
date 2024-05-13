using System;
using UnityEngine;

public class TickManager : Singleton<TickManager>
{
    [field:SerializeField]
    public float TickTime { get; protected set; } = 1f;

    public Action OnTick { get; set; }

    private Timer nextTickTimer = null;

    private bool ticking = false;

    public void Start() {

        GameManager.Instance.GameReset += Reset;
        Reset();
    }

    private void Reset() {
        TickTime = SpeedRampManager.Instance.GetStartingTickSpeed();
        nextTickTimer = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(!ticking) {
            return;
        }

        if(nextTickTimer == null) {
            nextTickTimer = new Timer(TickTime);
            nextTickTimer.AddOnTimerFinishedEvent(() => {
                OnTick?.Invoke();
                nextTickTimer = null;
            });
        }

        nextTickTimer.DecreaseTime(Time.deltaTime);
    }
    

    public void StartTicking() {
        ticking = true;
    }

    public void StopTicking() {
        ticking = false;
    }

    public void UpdateTickTime(float newTickTime) {
        TickTime = newTickTime;
    }
}
