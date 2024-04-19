using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BoardBackgroundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject boardLineGameObject;

    [SerializeField]
    private Transform boardBackgroundLineTransform;

    [SerializeField]
    private Transform boardBackgroundShadowTransform;

    [SerializeField]
    private Vector2 shadowOffset = Vector2.zero;

    [SerializeField]
    private Color shadowColor = Color.grey;

    public void CreateBackground(int rows, int columns) {

        // Go one extra because of the inner and outer lines
        for(int i = 0; i <= rows; i++) {
            Instantiate(boardLineGameObject, boardBackgroundLineTransform).GetComponent<BoardBackgroundLineRendererController>().DrawCircle(i);
        }

        // Go one extra because of the inner and outer lines
        for(int i = 0; i < columns; i++) {
            Instantiate(boardLineGameObject, boardBackgroundLineTransform).GetComponent<BoardBackgroundLineRendererController>().DrawLine(i);
        }

        GameObject shadows = Instantiate(boardBackgroundLineTransform.gameObject, boardBackgroundShadowTransform);
        shadows.transform.position += new Vector3(shadowOffset.x, shadowOffset.y, 0f);

        foreach(LineRenderer lineRenderer in shadows.GetComponentsInChildren<LineRenderer>()) {
            lineRenderer.startColor = shadowColor;
            lineRenderer.endColor = shadowColor;
            lineRenderer.sortingLayerName = "Shadow";
        }
    }
}
