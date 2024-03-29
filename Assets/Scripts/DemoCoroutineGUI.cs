using System.Collections;
using ScreenFaderComponents.Actions;
using UnityEngine;

public class DemoCoroutineGUI : MonoBehaviour
{
	private TextMesh text;

	private TextMesh smalltext;

	private bool showLabel;

	private int showButton;

	private Transform cameraTransform;

	private Vector3 originalCamPos = new Vector3(0f, 1f, -10f);

	private Vector3 whatsnewCamPos = new Vector3(0f, -20f, -10f);

	private Vector3 howtouseCamPos = new Vector3(0f, 15f, -10f);

	private void Start()
	{
		text = GameObject.Find("Text").GetComponent<TextMesh>();
		smalltext = GameObject.Find("SmallText").GetComponent<TextMesh>();
		cameraTransform = Camera.main.GetComponent<Transform>();
		Fader.Instance.FadeIn(0f).Pause().StartCoroutine(this, ChangeTitleText1())
			.FadeOut()
			.Pause(2f)
			.FadeIn()
			.StartCoroutine(this, ChangeTitleText2())
			.FadeOut()
			.Pause(2f)
			.FadeIn()
			.StartCoroutine(this, "StartBlinkingLight", "light1")
			.StartCoroutine(this, "StartBlinkingLight", "light2")
			.StartCoroutine(this, ChangeTitleText3())
			.FadeOut()
			.StartCoroutine(this, ShowSmallTextAndGUI());
	}

	private IEnumerator StartBlinkingLight(string name)
	{
		Light i = GameObject.Find(name).GetComponent<Light>();
		while (true)
		{
			i.enabled = true;
			yield return new WaitForSeconds(Random.Range(1f, 3f));
			i.enabled = false;
			yield return new WaitForSeconds(0.05f);
			if (Random.Range(0, 5) == 1)
			{
				i.intensity = 0.5f;
				i.enabled = true;
				yield return new WaitForSeconds(0.05f);
				i.enabled = false;
				i.intensity = 1f;
				yield return new WaitForSeconds(0.1f);
			}
		}
	}

	private IEnumerator ChangeTitleText1()
	{
		text.text = "welcome";
		yield break;
	}

	private IEnumerator ChangeTitleText2()
	{
		text.text = "to the magic";
		yield return new WaitForSeconds(1f);
	}

	private IEnumerator ChangeTitleText3()
	{
		text.text = "of Screen Fader";
		GameObject.Find("light3").GetComponent<Light>().enabled = true;
		yield return new WaitForSeconds(1f);
	}

	private IEnumerator ShowSmallTextAndGUI()
	{
		yield return new WaitForSeconds(2f);
		smalltext.text = "the easiest";
		yield return new WaitForSeconds(1f);
		smalltext.text = "the easiest   way";
		yield return new WaitForSeconds(1f);
		smalltext.text = "the easiest   way   to make";
		yield return new WaitForSeconds(1f);
		smalltext.text = "the easiest   way   to make   fadings";
		yield return new WaitForSeconds(2f);
		while (showButton < 5)
		{
			showButton++;
			yield return new WaitForSeconds(0.15f);
		}
		showLabel = true;
	}

	private IEnumerator MoveCameraToMenu()
	{
		while (Vector3.Distance(cameraTransform.position, originalCamPos) > 0.25f)
		{
			cameraTransform.position = Vector3.Lerp(cameraTransform.position, originalCamPos, Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		while (showButton < 5)
		{
			showButton++;
			yield return new WaitForSeconds(0.15f);
		}
	}

	private IEnumerator MoveCameraToHowToUse()
	{
		while (Vector3.Distance(cameraTransform.position, howtouseCamPos) > 0.25f)
		{
			cameraTransform.position = Vector3.Lerp(cameraTransform.position, howtouseCamPos, Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		showButton = -1;
	}

	private IEnumerator MoveCameraToWTN()
	{
		while (Vector3.Distance(cameraTransform.position, whatsnewCamPos) > 0.25f)
		{
			cameraTransform.position = Vector3.Lerp(cameraTransform.position, whatsnewCamPos, Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
		showButton = -1;
	}

	private void OnGUI()
	{
		float x = Screen.width / 2 - 100;
		float num = Screen.height / 2 + 50;
		if (showButton > 0 && GUI.Button(new Rect(x, num + 33f, 200f, 30f), "Default Fading Demo"))
		{
			Fader.Instance.FadeIn(2f).Pause().StartAction(new LoadSceneAction(), 1);
		}
		if (showButton > 1 && GUI.Button(new Rect(x, num + 66f, 200f, 30f), "Squared effect Demo"))
		{
			Fader.Instance.FadeIn(2f).Pause().StartAction(new LoadSceneAction(), 2);
		}
		if (showButton > 2 && GUI.Button(new Rect(x, num + 99f, 200f, 30f), "Stripes effectDemo"))
		{
			Fader.Instance.FadeIn(2f).Pause().StartAction(new LoadSceneAction(), 3);
		}
		if (showButton > 3 && GUI.Button(new Rect(x, num + 132f, 200f, 30f), "How to use?"))
		{
			showButton = 0;
			Fader.Instance.StartCoroutine(this, "MoveCameraToHowToUse");
		}
		if (showButton > 4 && GUI.Button(new Rect(x, num + 165f, 200f, 30f), "What's new?"))
		{
			showButton = 0;
			Fader.Instance.StartCoroutine(this, "MoveCameraToWTN");
		}
		if (showButton == -1 && GUI.Button(new Rect(x, Screen.height - 100, 200f, 30f), "Main Menu"))
		{
			showButton = 0;
			Fader.Instance.StartCoroutine(this, MoveCameraToMenu());
		}
		if (showLabel)
		{
			GUI.Label(new Rect(Screen.width / 2 - 175, Screen.height - 50, 700f, 30f), "Note, this demo scene also created with Screen Fader v.1.2");
		}
	}
}
