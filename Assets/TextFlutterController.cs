using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextFlutterController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text text;

    [field:SerializeField]
    public float Frequency { get; set; } = 1f;

    [field:SerializeField]
    public float Speed { get; set; }= 5f;

    [field:SerializeField]
    public float Magnitude { get; set; } = 1f;

    // Start is called before the first frame update
    void Start()
    {

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
                vertices[charIndex + j] += Vector3.up * (Mathf.Sin(Time.time * Speed + (Frequency * i)) * Magnitude);
            }
        }

        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }
}
