using System.Collections.Generic;
using UnityEngine;

public class ColorManager : Singleton<ColorManager>
{
    [field:SerializeReference]
    public List<Color> Colors { get; protected set; }

    public int GetRandomColorIndex() {
        return Random.Range(0, Colors.Count);
    }
}
