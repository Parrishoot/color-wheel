using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PiecesManager : Singleton<PiecesManager>
{
    public Action<PieceManager> PieceSpawned { get; set; }

    public Action PieceMoved { get; set ; }
}
