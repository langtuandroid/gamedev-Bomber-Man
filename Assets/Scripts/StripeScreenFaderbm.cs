using UnityEngine;

public class StripeScreenFaderbm : Faderbm
{
	public enum Direction
	{
		HORIZONTAL_LEFT = 0,
		HORIZONTAL_RIGHT = 1,
		HORIZONTAL_IN = 2,
		HORIZONTAL_OUT = 3
	}

	private struct AnimRect
	{
		private Rect rect;

		public float fromScale;

		public float toScale;

		public AnimRect(Rect rect, float fromScale, float toScale)
		{
			this.rect = rect;
			this.fromScale = fromScale;
			this.toScale = toScale;
		}

		public Rect GetRect(float time)
		{
			if (time >= 1f)
			{
				return rect;
			}
			if (time < 0f)
			{
				return new Rect(rect.xMin + rect.width * time / 2f, rect.yMin + rect.height * time / 2f, 0f, 0f);
			}
			return new Rect(rect.xMin + (rect.width - rect.width * time) / 2f, rect.yMin + (rect.height - rect.height * time) / 2f * time, rect.width * time, rect.height * time);
		}
	}

	[Range(2f, 50f)]
	public int numberOfStripes = 10;

	public Direction direction;

	private Color last_color = Color.black;

	private int last_numberOfStripes = 10;

	private Texture texture;

	private AnimRect[] rcs;

	protected override void Init()
	{
		base.Init();
		texture = GetTextureFromColor(color);
		rcs = new AnimRect[numberOfStripes];
		int num = Screen.width / rcs.Length * 3;
		for (int i = 0; i < rcs.Length; i++)
		{
			rcs[i] = new AnimRect(new Rect((Screen.width + num) / rcs.Length * i - 5, -5f, (Screen.width + num) / rcs.Length, Screen.height + 10), 0.1f, 1f);
		}
		last_color = color;
		last_numberOfStripes = numberOfStripes;
	}

	protected override void Update()
	{
		if ((color != last_color) | (numberOfStripes != last_numberOfStripes))
		{
			Init();
		}
		base.Update();
	}

	protected override void DrawOnGUI()
	{
		for (int i = 0; i < rcs.Length; i++)
		{
			switch (direction)
			{
			case Direction.HORIZONTAL_LEFT:
				GUI.DrawTexture(rcs[i].GetRect(GetLinearT(i, rcs.Length)), texture);
				break;
			case Direction.HORIZONTAL_RIGHT:
				GUI.DrawTexture(rcs[rcs.Length - i - 1].GetRect(GetLinearT(i, rcs.Length)), texture);
				break;
			case Direction.HORIZONTAL_IN:
				GUI.DrawTexture(rcs[rcs.Length - i - 1].GetRect(GetLinearT(i * 2, rcs.Length)), texture);
				GUI.DrawTexture(rcs[i].GetRect(GetLinearT(i * 2, rcs.Length)), texture);
				break;
			case Direction.HORIZONTAL_OUT:
				if (i < rcs.Length / 2)
				{
					GUI.DrawTexture(rcs[rcs.Length / 2 - i - 1].GetRect(GetLinearT(i * 2, rcs.Length)), texture);
					GUI.DrawTexture(rcs[rcs.Length / 2 + i].GetRect(GetLinearT(i * 2, rcs.Length)), texture);
				}
				break;
			}
			if (((direction == Direction.HORIZONTAL_IN) | (direction == Direction.HORIZONTAL_OUT)) && i > rcs.Length / 2 + 1)
			{
				break;
			}
		}
	}
}
