using UnityEngine;

public class FaderFactorybm
{
    public static Faderbm CreateDefaultFader(GameObject go)
    {
        if (go == null) go = new GameObject("ScreenFader");
        return go.AddComponent<DefaultScreenFaderbm>();
    }
    
}