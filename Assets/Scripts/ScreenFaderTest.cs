using UnityEngine;

public class ScreenFaderTest : MonoBehaviour
{
	private void OnGUI()
	{
		if (GUILayout.Button("Load Scene"))
		{
			Fader.Instance.FadeIn().Pause().LoadLevel("Level2")
				.FadeOut(2f);
		}
	}
}
