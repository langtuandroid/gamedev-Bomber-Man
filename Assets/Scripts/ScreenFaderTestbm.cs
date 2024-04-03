using UnityEngine;

public class ScreenFaderTestbm : MonoBehaviour
{
	private void OnGUI()
	{
		if (GUILayout.Button("Load Scene"))
		{
			Faderbm.Instance.FadeIn().Pause().LoadLevel("Level2")
				.FadeOut(2f);
		}
	}
}
