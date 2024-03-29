using System.Text;

internal class ExampleCodeBuilder
{
	private StringBuilder sb_fields = new StringBuilder();

	private StringBuilder sb_OnGUI = new StringBuilder();

	private StringBuilder sb_methods = new StringBuilder();

	public void AddOnGUI_FadeScreen()
	{
		sb_OnGUI.Append("\t\tif( GUILayout.Button( \"Fade Screen\") )\r\n\t\t{\r\n            Fader.SetupAsDefaultFader();\r\n            Fader.Instance.FadeIn().Pause(1).FadeOut();\r\n\t\t}\r\n");
	}

	public void AddOnGUI_FadeObject()
	{
		sb_fields.Append("\tpublic GameObject character;\r\n");
		sb_OnGUI.Append("        if ( GUILayout.Button( \"Fade Object\") )\r\n\t\t{\r\n            Fader.Instance.FadeIn(character).Pause().FadeOut(character, 0.25f);\r\n\t\t}\r\n");
	}

	public void AddOnGUI_LoadLevel()
	{
		sb_OnGUI.Append("\t\tif (GUILayout.Button(\"Load Scene\"))\r\n\t\t{\r\n\t\t    Fader.Instance.FadeIn().Pause().LoadLevel(\"Level2\").FadeOut(2);\r\n\t\t}\r\n");
	}

	public void AddOnGUI_SetColour()
	{
		sb_OnGUI.Append("        if (GUILayout.Button(\"Set Color\") )\r\n\t\t{\r\n           Color color = GetRandomColor();\r\n           Fader.Instance.SetColor(color).FadeIn().Pause().FadeOut();\r\n\t\t}\r\n");
		sb_methods.Append("\r\n   private Color GetRandomColor()\r\n\t{\r\n\t    return new Color(\r\n\t        Random.Range(0.25f, 0.75f), \r\n\t        Random.Range(0.25f, 0.75f), \r\n\t        Random.Range(0.25f, 0.75f));\r\n\t}");
	}

	public void AddOnGUI_SquaredFade()
	{
		sb_OnGUI.Append("        if ( GUILayout.Button( \"Squared Fading\") )\r\n\t\t{\r\n            Fader.SetupAsSquaredFader(10); // 10 - is a number of squares in the row\r\n            Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut();\r\n\t\t}\r\n");
	}

	public void AddOnGUI_StripesFade()
	{
		sb_OnGUI.Append("        if ( GUILayout.Button( \"Stripes Fading\") )\r\n\t\t{\r\n            Fader.SetupAsStripesFader(10); // 10 - is a number of stripes\r\n            Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut();\r\n\t\t}\r\n");
	}

	internal void AddOnGUI_LinesFade()
	{
		sb_OnGUI.Append("        if ( GUILayout.Button( \"Stripes Fading\") )\r\n\t\t{\r\n            // linesImages - array of Texture objects\r\n            Fader.SetupAsLinesFader(LinesScreenFader.Direction.IN_FROM_RIGHT, linesImages);\r\n            Fader.Instance.SetColor(Color.black).FadeIn().Pause().FadeOut();\r\n\t\t}\r\n");
	}

	public void AddOnGUI_Flash()
	{
		sb_OnGUI.Append("        if ( GUILayout.Button( \"Quick Flash\") )\r\n\t\t{\r\n            Color darkRed = new Color(1, 0.5f, 0.5f);\r\n            Fader.Instance.SetColor(darkRed).Flash();\r\n\t\t}\r\n");
	}

	public void AddOnGUI_StartCoroutine()
	{
		sb_OnGUI.Append("        if ( GUILayout.Button( \"Start Coroutine\") )\r\n\t\t{\r\n            Fader.Instance.StartCoroutine(this, \"pCoroutine\").Pause(3).FadeIn().FadeOut();\r\n\t\t}\r\n");
	}

	public void AddOnGUI_StartAction()
	{
		sb_OnGUI.Append("        if ( GUILayout.Button( \"StartAction\") )\r\n\t\t{\r\n            \r\n\t\t}\r\n");
	}

	public void AddMethod_StartCoroutine()
	{
		sb_methods.Append("  \r\n  \r\n  /// THIS SIMPLE COROUTINE JUST MOVES A MOON\r\n  public IEnumerator pCoroutine()\r\n  {\r\n      GameObject moon = GameObject.Find(\"Test_Moon\");\r\nTransform moontransform = moon.GetComponent<Transform>();\r\n      Vector3 p1 = new Vector3(moontransform.position.x - 5, moontransform.position.y, moontransform.position.z);\r\n      while (moontransform.position != p1)\r\n      {\r\n          moontransform.position = Vector3.Lerp(moontransform.position, p1, Time.deltaTime);\r\n          yield return new WaitForEndOfFrame();\r\n      }\r\n  \r\n      yield break;\r\n  }\r\n");
	}

	public void AddMethod_StartAction()
	{
	}

	public string GetExampleCode()
	{
		return "using UnityEngine;\r\n\r\n/// \r\n/// TEST SCRIPT FOR SCREEN FADER\r\n/// \r\npublic class ScreenFaderTest : MonoBehaviour\r\n{\r\n " + sb_fields.ToString() + "\r\n\tvoid OnGUI () \r\n    { \r\n " + sb_OnGUI.ToString() + "   }" + sb_methods.ToString() + "\r\n}";
	}

	public string GetFormattedExampleCode()
	{
		string exampleCode = GetExampleCode();
		exampleCode = exampleCode.Replace("Fader.", "<color=red><b>Fader</b>.</color>");
		exampleCode = exampleCode.Replace("FadeIn", "<color=orange>FadeIn</color>");
		exampleCode = exampleCode.Replace("FadeOut", "<color=orange>FadeOut</color>");
		exampleCode = exampleCode.Replace("Pause", "<color=orange>Pause</color>");
		exampleCode = exampleCode.Replace(".Flash", ".<color=orange>Flash</color>");
		exampleCode = exampleCode.Replace("SetColor", "<color=orange>SetColor</color>");
		exampleCode = exampleCode.Replace("StartCoroutine", "<color=orange>StartCoroutine</color>");
		exampleCode = exampleCode.Replace("StartAction", "<color=orange>StartAction</color>");
		return exampleCode.Replace("LoadLevel", "<color=orange>LoadLevel</color>");
	}
}
