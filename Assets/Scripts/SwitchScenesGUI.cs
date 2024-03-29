using ScreenFaderComponents.Actions;
using UnityEngine;

internal class SwitchScenesGUI : MonoBehaviour
{
	private void OnGUI()
	{
		GUI.depth = -3;
		GUI.Window(0, new Rect(0f, 0f, 220f, 140f), DoWindow, "Screen Fader Types");
	}

	private void DoWindow(int id)
	{
		if (GUI.Button(new Rect(10f, 30f, 200f, 30f), "Default Fading"))
		{
			Fader.Instance.FadeIn().StartAction(new LoadSceneAction(), 1);
		}
		if (GUI.Button(new Rect(10f, 65f, 200f, 30f), "Squares Effect"))
		{
			Fader.Instance.FadeIn().StartAction(new LoadSceneAction(), 2);
		}
		if (GUI.Button(new Rect(10f, 100f, 200f, 30f), "Stripes Effect"))
		{
			Fader.Instance.FadeIn().StartAction(new LoadSceneAction(), 3);
		}
	}
}
