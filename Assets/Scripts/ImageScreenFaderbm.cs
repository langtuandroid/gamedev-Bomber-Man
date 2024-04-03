using UnityEngine;

internal class ImageScreenFaderbm : Faderbm
{
	public Texture image;

	protected Color last_fadeColor = Color.black;

	[Range(0f, 1f)]
	public float maxDensity = 1f;

	protected Texture colorTexture;

	protected override void Init()
	{
		Faderbm.instance = this;
		colorTexture = image;
		last_fadeColor = color;
	}

	protected override void DrawOnGUI()
	{
		GUI.color = color;
		if (image != null)
		{
			GUI.DrawTexture(new Rect(0f, 0f, Screen.width, Screen.height), image, ScaleMode.StretchToFill, true);
		}
		else
		{
			Debug.Log("ImageScreenFader: image is null");
		}
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
