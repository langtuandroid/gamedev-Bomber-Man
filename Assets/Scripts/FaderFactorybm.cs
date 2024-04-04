using UnityEngine;

public class FaderFactorybm
{
    public static Faderbm CreateDefaultFader(GameObject go)
    {
        if (go == null) go = new GameObject("ScreenFader");
        return go.AddComponent<DefaultScreenFaderbm>();
    }

    public static Faderbm CreateSquaredFader(GameObject go, int squares)
    {
        if (go == null) go = new GameObject("ScreenFader");
        var squaredScreenFaderbm = go.AddComponent<SquaredScreenFaderbm>();
        squaredScreenFaderbm.direction = SquaredScreenFaderbm.Direction.DIAGONAL_LEFT_UP;
        squaredScreenFaderbm.columns = squares;
        return squaredScreenFaderbm;
    }

    public static Faderbm CreateStripesFader(GameObject go, int stripes)
    {
        if (go == null) go = new GameObject("ScreenFader");
        var stripeScreenFaderbm = go.AddComponent<StripeScreenFaderbm>();
        stripeScreenFaderbm.numberOfStripes = stripes;
        return stripeScreenFaderbm;
    }

    public static Faderbm CreateImageFader(GameObject go, Texture texture)
    {
        if (go == null) go = new GameObject("ScreenFader");
        var imageScreenFaderbm = go.AddComponent<ImageScreenFaderbm>();
        imageScreenFaderbm.image = texture;
        return imageScreenFaderbm;
    }
}