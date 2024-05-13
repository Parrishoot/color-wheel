using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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

    [SerializeField]
    private bool fluttering = true;

    [SerializeField]
    private TMP_MeshInfo[] cachedData;

    // Start is called before the first frame update
    void Start()
    {
        // TODO: FIX THIS HACK - WILL NOT WORK ON TEXT UPDATE
        text.ForceMeshUpdate();
        cachedData = text.textInfo.CopyMeshInfoVertexData();
    }

    // Update is called once per frame
    void Update()
    {
        if(!fluttering) {
            return;
        }

        text.ForceMeshUpdate();
        Mesh mesh = text.mesh;
        Vector3[] vertices = mesh.vertices;


        for(int i = 0; i < text.textInfo.characterCount; i++) {

            int charIndex = text.textInfo.characterInfo[i].vertexIndex;

            for(int j = 0; j < 4; j++) {
                vertices[charIndex + j] += Vector3.up * (Mathf.Sin(Time.unscaledTime * Speed + (Frequency * i)) * Magnitude);
            }
        }

        mesh.vertices = vertices;
        text.canvasRenderer.SetMesh(mesh);
    }
    
    public void StartFlutter() {
        fluttering = true;
    }

    public void StopFlutter() {
        fluttering = false;
        ResetPositions();
    }

    public void CopyVertices() {
        
        
        // Mesh mesh = text.mesh;
        // Vector3[] vertices = mesh.vertices;
        // originalPositions = new Vector3[vertices.Length];

        // for(int i = 0; i < text.textInfo.characterCount; i++) {

        //     int charIndex = text.textInfo.characterInfo[i].vertexIndex;

        //     for(int j = 0; j < 4; j++) {
        //         vertices[charIndex + j] = originalPositions[charIndex + j];
        //     }
        // }
    }

    void ResetPositions() {
        text.textInfo.meshInfo = cachedData;
        text.ForceMeshUpdate();
    }
}
