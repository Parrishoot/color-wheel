using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardBackgroundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject boardLineGameObject;

    public void CreateBackground(int rows, int columns) {

        // Go one extra because of the inner and outer lines
        for(int i = 0; i <= rows; i++) {
            Instantiate(boardLineGameObject, transform).GetComponent<BoardBackgroundLineRendererController>().DrawCircle(i);
        }

        // Go one extra because of the inner and outer lines
        for(int i = 0; i < columns; i++) {
            Instantiate(boardLineGameObject, transform).GetComponent<BoardBackgroundLineRendererController>().DrawLine(i);
        }
    }
}
