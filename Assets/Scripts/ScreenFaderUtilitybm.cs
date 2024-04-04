using UnityEngine;

public static class ScreenFaderUtilitybm
{
    public static Rect Lerp(Rect from, Rect to, float t)
    {
        return new Rect(Mathf.Lerp(from.xMin, to.xMin, t), Mathf.Lerp(from.yMin, to.yMin, t),
            Mathf.Lerp(from.width, to.width, t), Mathf.Lerp(from.height, to.height, t));
    }
}