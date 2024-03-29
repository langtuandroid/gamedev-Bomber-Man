using System;
using ScreenFaderComponents.Actions;
using UnityEngine;

public class DemoSquaredGUI : MonoBehaviour
{
	public SquaredScreenFader component;

	public Texture2D logo;

	public Texture2D[] patterns;

	private int selectedPattern;

	private float fadeSpeed = 1f;

	private ShowLogoAction showLogoAction = new ShowLogoAction();

	private void Start()
	{
		Fader.Instance.FadeOut(2f);
	}

	private void OnGUI()
	{
		GUI.depth = -3;
		GUI.Window(1, new Rect(0f, 150f, 220f, 390f), DoWindow, "Settings");
		if (showLogoAction.IsLogoVisible && logo != null)
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
			Fader.Instance.FadeIn(fadeSpeed).StartAction(showLogoAction);
		}
		if (GUI.Button(new Rect(115f, 350f, 95f, 30f), "Fade OUT"))
		{
			Fader.Instance.StartAction(showLogoAction).FadeOut(fadeSpeed);
		}
	}

	private void DrawControls()
	{
		GUI.Label(new Rect(10f, 20f, 200f, 20f), "Directions");
		Direction(20, 40, SquaredScreenFader.Direction.NONE);
		Direction(20, 60, SquaredScreenFader.Direction.DIAGONAL_LEFT_DOWN);
		Direction(20, 80, SquaredScreenFader.Direction.DIAGONAL_LEFT_UP);
		Direction(20, 100, SquaredScreenFader.Direction.DIAGONAL_RIGHT_DOWN);
		Direction(20, 120, SquaredScreenFader.Direction.DIAGONAL_RIGHT_UP);
		Direction(20, 140, SquaredScreenFader.Direction.HORIZONTAL_LEFT);
		Direction(20, 160, SquaredScreenFader.Direction.HORIZONTAL_RIGHT);
		Direction(20, 180, SquaredScreenFader.Direction.VERTICAL_DOWN);
		Direction(20, 200, SquaredScreenFader.Direction.VERTICAL_UP);
		fadeSpeed = GUI.HorizontalSlider(new Rect(90f, 235f, 110f, 20f), fadeSpeed, 0.5f, 10f);
		GUI.Label(new Rect(10f, 230f, 200f, 20f), string.Format("Speed {0:N1}", fadeSpeed));
		component.columns = (int)GUI.HorizontalSlider(new Rect(90f, 260f, 110f, 20f), component.columns, 5f, 50f);
		GUI.Label(new Rect(10f, 255f, 200f, 20f), string.Format("Columns {0}", component.columns));
		int num = GUI.SelectionGrid(new Rect(10f, 300f, 200f, 20f), selectedPattern, patterns, patterns.Length);
		GUI.Label(new Rect(10f, 280f, 200f, 20f), "Patterns");
		if (num != selectedPattern)
		{
			selectedPattern = num;
			component.texture = patterns[selectedPattern];
		}
	}

	private void Direction(int x, int y, SquaredScreenFader.Direction direction)
	{
		if (GUI.Toggle(new Rect(x, y, 240f, 20f), component.direction == direction, Enum.GetName(typeof(SquaredScreenFader.Direction), direction)))
		{
			component.direction = direction;
		}
	}
}
