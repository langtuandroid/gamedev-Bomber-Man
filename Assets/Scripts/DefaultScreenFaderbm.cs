using UnityEngine;

public class DefaultScreenFaderbm : Faderbm
{
	protected Color last_fadeColorbm = Color.black;

	[Range(0f, 1f)]
	public float maxDensity = 1f;

	protected Texture colorTexturebm;

	protected override void Init()
	{
		base.Init();
		colorTexturebm = GetTextureFromColor(color);
		last_fadeColorbm = color;
	}

	protected override void DrawOnGUI()
	{
		GUI.color = color;
		GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), colorTexturebm, ScaleMode.StretchToFill, true);
	}

	protected override void Update()
	{
		if (color != last_fadeColorbm)
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
