using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteColorRandomizer : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        Color newColor = Color.white;
        newColor.r = Random.Range(0f, 1f);
        newColor.b = Random.Range(0f, 1f);
        newColor.g = Random.Range(0f, 1f);

        spriteRenderer.material.SetColor("_Color", newColor);    
    }
}
