using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinScaler : MonoBehaviour
{
    [SerializeField]
    private float scaleAmount = .1f;

    [SerializeField]
    private float speed = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.localScale = Vector3.one + (Vector3.one * Mathf.Sin(Time.time * speed) * scaleAmount);
    }
}
