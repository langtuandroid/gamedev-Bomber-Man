using UnityEngine;

public class FaderFactory
{
	public static Fader CreateDefaultFader(GameObject go)
	{
		if (go == null)
		{
			go = new GameObject("ScreenFader");
		}
		return go.AddComponent<DefaultScreenFader>();
	}

	public static Fader CreateSquaredFader(GameObject go, int squares)
	{
		if (go == null)
		{
			go = new GameObject("ScreenFader");
		}
		SquaredScreenFader squaredScreenFader = go.AddComponent<SquaredScreenFader>();
		squaredScreenFader.direction = SquaredScreenFader.Direction.DIAGONAL_LEFT_UP;
		squaredScreenFader.columns = squares;
		return squaredScreenFader;
	}

	public static Fader CreateStripesFader(GameObject go, int stripes)
	{
		if (go == null)
		{
			go = new GameObject("ScreenFader");
		}
		StripeScreenFader stripeScreenFader = go.AddComponent<StripeScreenFader>();
		stripeScreenFader.numberOfStripes = stripes;
		return stripeScreenFader;
	}

	public static Fader CreateImageFader(GameObject go, Texture texture)
	{
		if (go == null)
		{
			go = new GameObject("ScreenFader");
		}
		ImageScreenFader imageScreenFader = go.AddComponent<ImageScreenFader>();
		imageScreenFader.image = texture;
		return imageScreenFader;
	}
}
