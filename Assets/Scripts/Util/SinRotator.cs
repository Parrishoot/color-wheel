using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinRotator : MonoBehaviour
{
    [SerializeField]
    private float rotationAmount = 15f;

    [SerializeField]
    private float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = Vector3.forward * Mathf.Sin(Time.time * speed) * rotationAmount;
    }
}
