using System;
using Unity.Collections;
using UnityEngine;

public class SpeedRampManager : Singleton<SpeedRampManager>
{
    [SerializeField]
    private float maxTickSpeed = .85f;

    [SerializeField]
    private float minTickSpeed = .6f;

    [SerializeField]
    private int clustersForMinTickSpeed = 100;

    private float currentTickSpeed = 0f;

    private int currentClusters = 0;

    public void Start() {

        GameManager.Instance.GameReset += () => {
            currentClusters = 0;
        };

        currentTickSpeed = maxTickSpeed;
    }

    // Update is called once per frame
    public void ClusterPopped() {
        currentClusters = Math.Min(currentClusters + 1, clustersForMinTickSpeed);

        currentTickSpeed = Mathf.Lerp(maxTickSpeed, minTickSpeed, currentClusters / (float) clustersForMinTickSpeed);
        TickManager.Instance.UpdateTickTime(currentTickSpeed);
    }

    public float GetStartingTickSpeed() {
        return maxTickSpeed;
    }
}
