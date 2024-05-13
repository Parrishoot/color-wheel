using TMPro;
using UnityEngine;

public static class ColorExtensions
{
    public static void SetAlpha(this TMP_Text text, float alpha) {
        
        Color color = text.color;
        color.a = alpha;

        text.color = color;
    }
}
