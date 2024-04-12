using System;
using UnityEngine;

public class TickManager : Singleton<TickManager>
{
    [SerializeField]
    private float tickTime = 1f;

    public Action OnTick { get; set; }

    private Timer nextTickTimer = null;

    private bool ticking = false;

    // Update is called once per frame
    void Update()
    {
        if(!ticking) {
            return;
        }

        if(nextTickTimer == null) {
            nextTickTimer = new Timer(tickTime);
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
}
