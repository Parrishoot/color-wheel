using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class BoardBackgroundLineRendererController : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private int circleResolution = 100;

    [SerializeField]
    private float width = .1f;

    [SerializeField]
    private float wiggleAmount = .05f;

    [SerializeField]
    private float wiggleTimeScale = .5f;

    private Vector3[] points;

    public void Update() {

        // TODO: MOVE THIS TO A SHADER?
        Vector3[] newPositions = new Vector3[points.Length];

        for(int i = 0; i < points.Length; i++) {
            newPositions[i] = points[i] + new Vector3(1f, 1f, 0) * (Mathf.PerlinNoise(Time.time * wiggleTimeScale + points[i].x, Time.time * wiggleTimeScale + points[i].y) * wiggleAmount);
        }

        lineRenderer.SetPositions(newPositions);
    }

    public void DrawLine(int column) { 
        
        SetWidth();

        float angle = ((column + .5f) / (float) BoardManager.Instance.Columns) * Mathf.PI * 2;

        lineRenderer.positionCount = 2;
        points = new Vector3[2];

        Vector3 fixedPoint = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

        points[0] = fixedPoint * BoardManager.Instance.GetInnerRadiusForYCoord(0) / 2f;
        points[1] = fixedPoint * BoardManager.Instance.GetInnerRadiusForYCoord(BoardManager.Instance.Rows) / 2f;

        lineRenderer.SetPositions(points);
    }

    public void DrawCircle(int row) {

        SetWidth();

        int numPoints = (row + 1) * circleResolution;

        lineRenderer.positionCount = numPoints + 1;
        points = new Vector3[numPoints + 1];


        for(int i = 0; i <= numPoints; i++) {
            float angle = 2 * Mathf.PI * (i / (float) numPoints);
            points[i] = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f) * BoardManager.Instance.GetInnerRadiusForYCoord(row) / 2f;
        }

        lineRenderer.SetPositions(points);
    }

    public void SetWidth() {
        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;
    }
}
