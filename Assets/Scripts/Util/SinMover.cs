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
        startingPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = startingPosition + ((1 + Mathf.Sin(speed * Time.time)) / 2) * Vector3.up * magnitude;
    }
}
