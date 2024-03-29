using UnityEngine;

public class DefaultScreenFader : Fader
{
	protected Color last_fadeColor = Color.black;

	[Range(0f, 1f)]
	public float maxDensity = 1f;

	protected Texture colorTexture;

	protected override void Init()
	{
		base.Init();
		colorTexture = GetTextureFromColor(color);
		last_fadeColor = color;
	}

	protected override void DrawOnGUI()
	{
		GUI.color = color;
		GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), colorTexture, ScaleMode.StretchToFill, true);
	}

	protected override void Update()
	{
		if (color != last_fadeColor)
		{
			Init();
		}
		color.a = GetLinearBalance();
		base.Update();
	}

	protected virtual float GetLinearBalance()
	{
		return (!(fadeBalance < maxDensity)) ? maxDensity : fadeBalance;
	}
}
