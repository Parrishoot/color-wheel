using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waver : MonoBehaviour
{
    [SerializeField]
    private float scale = 1f;

    [SerializeField]
    private float timeScale = 10f;

    [SerializeField]
    private PieceManager pieceManager;
    // Update is called once per frame
    void Update()
    {
        Vector2 perlinCoords = new Vector2(Mathf.Cos(transform.localEulerAngles.z), Mathf.Sin(transform.localEulerAngles.z)) * (pieceManager.Coords.Value.y + 1); 
        transform.localScale = Vector3.one * Mathf.PerlinNoise((perlinCoords.x + Time.time *  timeScale) * scale, (perlinCoords.y + Time.time * timeScale) * scale);
    }
}
