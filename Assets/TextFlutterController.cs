using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlutterController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    [SerializeField]
    private float frequency = 1f;

    [SerializeField]
    private float speed = 1f;

    [SerializeField]
    private float magnitude = 1f;

    private float startY = 0f;

    // Start is called before the first frame update
    void Start()
    {
        float startY = text.textBounds.center.y;
    }

    // Update is called once per frame
    void Update()
    {
        text.ForceMeshUpdate();
        Mesh mesh = text.mesh;
        Vector3[] vertices = mesh.vertices;


        for(int i = 0; i < text.text.Length; i++) {

            int charIndex = text.textInfo.characterInfo[i].vertexIndex;

            for(int j = 0; j < 4; j++) {
                vertices[charIndex + j] += Vector3.up * (startY + Mathf.Sin(Time.time * speed + (frequency * i)) * magnitude);
            }
        }

        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }
}
