using System;
using ScreenFaderComponents.Enumerators;
using ScreenFaderComponents.Events;
using UnityEngine;

public class DemoStripesGUI : MonoBehaviour
{
	public StripeScreenFader component;

	public Texture2D logo;

	private bool showLogo;

	private float _r;

	private float _g;

	private float _b;

	private float fadeSpeed = 1f;

	private void Start()
	{
		Fader.Instance.FadeOut(2f);
		Fader.Instance.FadeFinish += Instance_FadeFinish;
		Fader.Instance.FadeStart += Instance_FadeStart;
		_r = component.color.r;
		_g = component.color.g;
		_b = component.color.b;
	}

	private void OnGUI()
	{
		GUI.depth = -3;
		GUI.Window(1, new Rect(0f, 150f, 220f, 390f), DoWindow, "Settings");
		if (showLogo)
		{
			GUI.DrawTexture(new Rect(500f, 100f, logo.width, logo.height), logo);
			GUI.Label(new Rect(500f, 100 + logo.height, logo.width, 500f), "Screen Fader it's the esiest way to fade-in or fade-out screen. \r\n\r\nScreen Fader is very simple, but on the other hand, it provide big possibilities. You can setup colors, transparency, speed of effect and delays before it starts in the Inspector panel.\r\nYou can subscribe on events and get notifications when effects will start or complete.");
		}
	}

	private void DoWindow(int id)
	{
		DrawControls();
		if (GUI.Button(new Rect(10f, 350f, 95f, 30f), "Fade IN"))
		{
			Fader.Instance.FadeIn(fadeSpeed);
		}
		if (GUI.Button(new Rect(115f, 350f, 95f, 30f), "Fade OUT"))
		{
			Fader.Instance.FadeOut(fadeSpeed);
		}
	}

	private void DrawControls()
	{
		GUI.Label(new Rect(10f, 20f, 200f, 20f), "Directions");
		Direction(20, 40, StripeScreenFader.Direction.HORIZONTAL_LEFT);
		Direction(20, 60, StripeScreenFader.Direction.HORIZONTAL_RIGHT);
		Direction(20, 80, StripeScreenFader.Direction.HORIZONTAL_OUT);
		Direction(20, 100, StripeScreenFader.Direction.HORIZONTAL_IN);
		GUI.Label(new Rect(10f, 130f, 200f, 20f), "Color");
		GUI.Label(new Rect(20f, 150f, 200f, 20f), "Red: ");
		GUI.Label(new Rect(20f, 170f, 200f, 20f), "Green: ");
		GUI.Label(new Rect(20f, 190f, 200f, 20f), "Blue: ");
		float num = GUI.HorizontalSlider(new Rect(100f, 155f, 100f, 20f), _r, 0f, 1f);
		float num2 = GUI.HorizontalSlider(new Rect(100f, 175f, 100f, 20f), _g, 0f, 1f);
		float num3 = GUI.HorizontalSlider(new Rect(100f, 195f, 100f, 20f), _b, 0f, 1f);
		if ((num != _r) | (num2 != _g) | (num3 != _b))
		{
			_r = num;
			_g = num2;
			_b = num3;
			component.color = new Color(_r, _g, _b);
		}
		component.numberOfStripes = (int)GUI.HorizontalSlider(new Rect(100f, 215f, 100f, 20f), component.numberOfStripes, 3f, 50f);
		GUI.Label(new Rect(10f, 210f, 200f, 20f), string.Format("Stripes: {0}", component.numberOfStripes));
		fadeSpeed = GUI.HorizontalSlider(new Rect(100f, 235f, 100f, 20f), fadeSpeed, 0.5f, 10f);
		GUI.Label(new Rect(10f, 230f, 100f, 20f), string.Format("Speed: {0}", fadeSpeed.ToString("#.0")));
	}

	private void Direction(int x, int y, StripeScreenFader.Direction direction)
	{
		if (GUI.Toggle(new Rect(x, y, 240f, 20f), component.direction == direction, Enum.GetName(typeof(StripeScreenFader.Direction), direction)))
		{
			component.direction = direction;
		}
	}

	private void Instance_FadeStart(object sender, FadeEventArgs e)
	{
		showLogo = false;
	}

	private void Instance_FadeFinish(object sender, FadeEventArgs e)
	{
		if (e.Direction == FadeDirection.In)
		{
			showLogo = true;
		}
	}
}
