using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinMover : MonoBehaviour
{
    [SerializeField]
    private float speed;

    [SerializeField]
    private float magnitude;

    private Vector3 startingPosition;

    void Start() {
        startingPosition = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = startingPosition + ((1 + Mathf.Sin(speed * Time.time)) / 2) * Vector3.one * magnitude;
    }
}
