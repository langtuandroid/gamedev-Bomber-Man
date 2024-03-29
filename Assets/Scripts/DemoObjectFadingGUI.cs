using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class DemoObjectFadingGUI : MonoBehaviour
{
	public GameObject character;

	public Texture damageImage;

	public Texture[] linesImages;

	public Image canvasImage;

	public bool showGUI;

	private Rect codeWindowRectangle = new Rect(300f, (Screen.height - 575) / 2, Screen.width - 350, 575f);

	private bool showCode;

	private string exampleCode = string.Empty;

	private string exampleCodeFormatted = string.Empty;

	private bool copy;

	private void Start()
	{
		Fader.SetupAsDefaultFader();
	}

	private void LateUpdate()
	{
		codeWindowRectangle = new Rect(300f, (Screen.height - 575) / 2, Screen.width - 350, 575f);
	}

	private void OnGUI()
	{
		if (!showGUI)
		{
			return;
		}
		GUILayout.Space((Screen.height - 300) / 2);
		GUILayout.BeginHorizontal();
		GUILayout.Space(50f);
		GUILayout.BeginVertical();
		GUILayout.Label("Try fading effects:");
		switch (pGUI.Button("Default Fading", "Setup"))
		{
		case pResult.BUTN:
			Fader.SetupAsDefaultFader();
			Fader.Instance.SetColor(Color.black).FadeIn().Pause()
				.FadeOut();
			break;
		case pResult.CODE:
			CreateDefaultFadeScreenCode();
			ShowCode();
			break;
		}
		switch (pGUI.Button("Squared Effect", "Setup"))
		{
		case pResult.BUTN:
			Fader.SetupAsSquaredFader(10);
			Fader.Instance.SetColor(Color.black).FadeIn().Pause()
				.FadeOut();
			break;
		case pResult.CODE:
			CreateSquaredFadeScreenCode();
			ShowCode();
			break;
		}
		switch (pGUI.Button("Striped Effect", "Setup"))
		{
		case pResult.BUTN:
			Fader.SetupAsStripesFader(10);
			Fader.Instance.SetColor(Color.black).FadeIn().Pause()
				.FadeOut();
			break;
		case pResult.CODE:
			CreateStripesFadeScreenCode();
			ShowCode();
			break;
		}
		switch (pGUI.Button("[new] Lines Effect", "Setup"))
		{
		case pResult.BUTN:
			Fader.SetupAsLinesFader(LinesScreenFader.Direction.IN_UP_DOWN, linesImages);
			Fader.Instance.FadeIn(0.5f).Pause(3f).FadeOut(0.25f);
			break;
		case pResult.CODE:
			CreateLinesCode();
			ShowCode();
			break;
		}
		switch (pGUI.Button("[new] Image Effect", "Setup"))
		{
		case pResult.BUTN:
			Fader.SetupAsImageFader(damageImage);
			Fader.Instance.SetColor(Color.red).FadeIn(0.05f).Pause(0.05f)
				.FadeOut(0.15f);
			break;
		}
		GUILayout.Space(10f);
		switch (pGUI.Button("Flash"))
		{
		case pResult.BUTN:
		{
			Fader.SetupAsDefaultFader();
			Color color = new Color(1f, 0.5f, 0.5f);
			Fader.Instance.SetColor(color).Flash();
			break;
		}
		case pResult.CODE:
			CreateFlashCode();
			ShowCode();
			break;
		}
		switch (pGUI.Button("Object Fading"))
		{
		case pResult.BUTN:
			Fader.Instance.FadeIn(character).Pause(1f).FadeOut(character, 0.25f);
			break;
		case pResult.CODE:
			CreateFadeObjectCode();
			ShowCode();
			break;
		}
		switch (pGUI.Button("Load Level Fading"))
		{
		case pResult.BUTN:
			if (Fader.Instance is ImageScreenFader)
			{
				Fader.SetupAsDefaultFader();
			}
			Fader.Instance.FadeIn().Pause().LoadLevel(GetLevelIndex())
				.FadeOut(2f);
			break;
		case pResult.CODE:
			CreateLoadLevelCode();
			ShowCode();
			break;
		}
		switch (pGUI.Button("Change Colours"))
		{
		case pResult.BUTN:
			Fader.Instance.SetColor(GetRandomColor()).FadeIn().Pause()
				.FadeOut();
			break;
		case pResult.CODE:
			CreateSetColourScreenCode();
			ShowCode();
			break;
		}
		switch (pGUI.Button("Start Coroutine"))
		{
		case pResult.BUTN:
			Fader.Instance.StartCoroutine(this, "pCoroutine").Pause(3f).FadeIn()
				.FadeOut();
			break;
		case pResult.CODE:
			CreateStartCoroutineScreenCode();
			ShowCode();
			break;
		}
		GUILayout.EndVertical();
		GUILayout.EndHorizontal();
		if (showCode)
		{
			GUI.Window(0, codeWindowRectangle, DoWindow, "Code (C#)");
		}
	}

	private void DoWindow(int id)
	{
		if (GUILayout.Button("Copy"))
		{
			copy = !copy;
			if (copy)
			{
				Utility.SendToClipboard(exampleCode);
			}
		}
		if (!copy)
		{
			GUILayout.Label(exampleCodeFormatted);
		}
		else
		{
			GUILayout.TextArea(exampleCode);
		}
		GUI.DragWindow();
	}

	public void On_ButtonClick()
	{
		GameObject obj = GameObject.Find("Button 0");
		GameObject obj2 = GameObject.Find("Button 1");
		GameObject obj3 = GameObject.Find("Button 2");
		float time = 0.5f;
		Fader.SetupAsDefaultFader();
		Fader.Instance.FadeIn(obj3, time).FadeIn(obj2, time).FadeIn(obj, time)
			.Pause(time)
			.FadeIn(canvasImage)
			.StartCoroutine(this, ShowGUI());
	}

	public IEnumerator ShowGUI()
	{
		showGUI = true;
		yield break;
	}

	public IEnumerator pCoroutine()
	{
		GameObject moon = GameObject.Find("Test_Moon");
		Transform moontransform = moon.GetComponent<Transform>();
		Vector3 p1 = new Vector3(moontransform.position.x - 5f, moontransform.position.y, moontransform.position.z);
		while (moontransform.position != p1)
		{
			moontransform.position = Vector3.Lerp(moontransform.position, p1, Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}

	private Color GetRandomColor()
	{
		return new Color(Random.Range(0.25f, 0.75f), Random.Range(0.25f, 0.75f), Random.Range(0.25f, 0.75f));
	}

	private int GetLevelIndex()
	{
		return (Application.loadedLevel == 0) ? 1 : 0;
	}

	private void ShowCode()
	{
		showCode = !showCode;
	}

	private void CreateSetColourScreenCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_SetColour();
		exampleCode = exampleCodeBuilder.GetExampleCode();
		exampleCodeFormatted = exampleCodeBuilder.GetFormattedExampleCode();
	}

	private void CreateDefaultFadeScreenCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_FadeScreen();
		exampleCode = exampleCodeBuilder.GetExampleCode();
		exampleCodeFormatted = exampleCodeBuilder.GetFormattedExampleCode();
	}

	private void CreateLoadLevelCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_LoadLevel();
		exampleCode = exampleCodeBuilder.GetExampleCode();
		exampleCodeFormatted = exampleCodeBuilder.GetFormattedExampleCode();
	}

	private void CreateFadeObjectCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_FadeObject();
		exampleCode = exampleCodeBuilder.GetExampleCode();
		exampleCodeFormatted = exampleCodeBuilder.GetFormattedExampleCode();
	}

	private void CreateStartActionScreenCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_StartAction();
		exampleCodeBuilder.AddMethod_StartAction();
		exampleCode = exampleCodeBuilder.GetExampleCode();
		exampleCodeFormatted = exampleCodeBuilder.GetFormattedExampleCode();
	}

	private void CreateStartCoroutineScreenCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_StartCoroutine();
		exampleCodeBuilder.AddMethod_StartCoroutine();
		exampleCode = exampleCodeBuilder.GetExampleCode();
		exampleCodeFormatted = exampleCodeBuilder.GetFormattedExampleCode();
	}

	private void CreateFlashCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_Flash();
		exampleCode = exampleCodeBuilder.GetExampleCode();
		exampleCodeFormatted = exampleCodeBuilder.GetFormattedExampleCode();
	}

	private void CreateStripesFadeScreenCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_StripesFade();
		exampleCode = exampleCodeBuilder.GetExampleCode();
		exampleCodeFormatted = exampleCodeBuilder.GetFormattedExampleCode();
	}

	private void CreateLinesCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_LinesFade();
		exampleCode = exampleCodeBuilder.GetExampleCode();
	}

	private void CreateSquaredFadeScreenCode()
	{
		ExampleCodeBuilder exampleCodeBuilder = new ExampleCodeBuilder();
		exampleCodeBuilder.AddOnGUI_SquaredFade();
		exampleCode = exampleCodeBuilder.GetExampleCode();
		exampleCodeFormatted = exampleCodeBuilder.GetFormattedExampleCode();
	}
}
