using UnityEngine;

internal class pGUI
{
	private static int w = 150;

	private static int w2 = 50;

	private static int h = 25;

	public static pResult Button(string text, string text2 = "Code")
	{
		pResult result = pResult.NONE;
		GUILayout.BeginHorizontal();
		if (GUILayout.Button(text, GUILayout.Width(w), GUILayout.Height(h)))
		{
			result = pResult.BUTN;
		}
		if (GUILayout.Button(text2, GUILayout.Width(w2), GUILayout.Height(h)))
		{
			result = pResult.CODE;
		}
		GUILayout.EndHorizontal();
		return result;
	}
}
