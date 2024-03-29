using ScreenFaderComponents.Actions;
using UnityEngine;

public class DemoDefaultGUI : MonoBehaviour
{
	[SerializeField]
	private DefaultScreenFader component;

	[SerializeField]
	private Texture2D logo;

	private float _r;

	private float _g;

	private float _b;

	private float r;

	private float g;

	private float b;

	private float fadeSpeed = 1f;

	private ShowLogoAction showLogoAction = new ShowLogoAction();

	private void Start()
	{
		Fader.Instance.FadeOut(2f);
		_r = component.color.r;
		_g = component.color.g;
		_b = component.color.b;
	}

	private void OnGUI()
	{
		GUI.depth = -3;
		GUI.Window(1, new Rect(0f, 150f, 220f, 390f), DoWindow, "Settings");
		if (showLogoAction.IsLogoVisible)
		{
			GUI.DrawTexture(new Rect(500f, 100f, logo.width, logo.height), logo);
			GUI.Label(new Rect(500f, 100 + logo.height, logo.width, 500f), "Screen Fader it's the esiest way to fade-in or fade-out screen. \r\n\r\nScreen Fader is very simple, but on the other hand, it provide big possibilities. You can setup colors, transparency, speed of effect and delays before it starts in the Inspector panel.\r\nYou can subscribe on events and get notifications when effects will start or complete.");
		}
	}

	private void DoWindow(int id)
	{
		DrawControls();
		if (GUI.Button(new Rect(10f, 270f, 95f, 30f), "Fade IN " + fadeSpeed.ToString("#.0")))
		{
			Fader.Instance.FadeIn(fadeSpeed);
		}
		if (GUI.Button(new Rect(115f, 270f, 95f, 30f), "Fade OUT " + fadeSpeed.ToString("#.0")))
		{
			Fader.Instance.FadeOut(fadeSpeed);
		}
		if (GUI.Button(new Rect(10f, 310f, 200f, 30f), "FadeIn, Pause 3 sec, FadeOUT"))
		{
			Fader.Instance.FadeIn().StartAction(showLogoAction).Pause(3f)
				.StartAction(showLogoAction)
				.FadeOut();
		}
		if (GUI.Button(new Rect(10f, 350f, 200f, 30f), "Flash"))
		{
			Fader.Instance.Flash();
		}
	}

	private void DrawControls()
	{
		GUI.Label(new Rect(10f, 30f, 200f, 20f), "Color");
		GUI.Label(new Rect(20f, 50f, 200f, 20f), "Red: ");
		GUI.Label(new Rect(20f, 70f, 200f, 20f), "Green: ");
		GUI.Label(new Rect(20f, 90f, 200f, 20f), "Blue: ");
		r = GUI.HorizontalSlider(new Rect(100f, 55f, 100f, 20f), _r, 0f, 1f);
		g = GUI.HorizontalSlider(new Rect(100f, 75f, 100f, 20f), _g, 0f, 1f);
		b = GUI.HorizontalSlider(new Rect(100f, 95f, 100f, 20f), _b, 0f, 1f);
		if ((r != _r) | (g != _g) | (b != _b))
		{
			_r = r;
			_g = g;
			_b = b;
			component.color = new Color(_r, _g, _b);
		}
		GUI.Label(new Rect(10f, 110f, 200f, 20f), "Max Density: ");
		component.maxDensity = GUI.HorizontalSlider(new Rect(100f, 115f, 100f, 20f), component.maxDensity, 0f, 1f);
		fadeSpeed = GUI.HorizontalSlider(new Rect(100f, 135f, 100f, 20f), fadeSpeed, 0.1f, 5f);
		GUI.Label(new Rect(10f, 130f, 100f, 20f), string.Format("Speed: {0}", fadeSpeed.ToString("#.0")));
	}
}
