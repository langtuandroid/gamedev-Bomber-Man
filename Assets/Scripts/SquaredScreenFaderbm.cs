using UnityEngine;

public class SquaredScreenFaderbm : Faderbm
{
	public enum Direction
	{
		NONE = 0,
		HORIZONTAL_LEFT = 1,
		HORIZONTAL_RIGHT = 2,
		VERTICAL_UP = 3,
		VERTICAL_DOWN = 4,
		DIAGONAL_LEFT_DOWN = 5,
		DIAGONAL_LEFT_UP = 6,
		DIAGONAL_RIGHT_UP = 7,
		DIAGONAL_RIGHT_DOWN = 8
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
			return new Rect(rect.x + rect.width / 2f * (0.5f - time / 2f), rect.y + rect.height / 2f * (0.5f - time / 2f), rect.width * time, rect.height * time);
		}
	}

	[Range(2f, 50f)]
	public int columns = 10;

	private int last_columns = 10;

	public Direction direction = Direction.DIAGONAL_LEFT_DOWN;

	public Texture texture;

	private int rows;

	private AnimRect[,] squares;

	protected override void Init()
	{
		base.Init();
		if (texture == null)
		{
			texture = GetTextureFromColor(color);
		}
		int num = Screen.width + columns;
		int num2 = Screen.height + columns;
		rows = num2 / (num / columns) + 2;
		squares = new AnimRect[columns, rows];
		for (int i = 0; i < columns; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				squares[i, j] = new AnimRect(new Rect(num / columns * i, num2 / rows * j, num / columns, num2 / rows), 0.1f, 1f);
			}
		}
		last_columns = columns;
	}

	protected override void DrawOnGUI()
	{
		if (columns != last_columns)
		{
			Init();
		}
		for (int i = 0; i < columns; i++)
		{
			for (int j = 0; j < rows; j++)
			{
				switch (direction)
				{
				case Direction.DIAGONAL_LEFT_DOWN:
					GUI.DrawTexture(squares[i, j].GetRect(fadeBalance / ((float)(i + j) / (float)(columns + rows))), texture);
					break;
				case Direction.DIAGONAL_LEFT_UP:
					GUI.DrawTexture(squares[columns - i - 1, rows - j - 1].GetRect(fadeBalance / ((float)(i + j) / (float)(columns + rows))), texture);
					break;
				case Direction.DIAGONAL_RIGHT_DOWN:
					GUI.DrawTexture(squares[columns - i - 1, j].GetRect(fadeBalance / ((float)(i + j) / (float)(columns + rows))), texture);
					break;
				case Direction.DIAGONAL_RIGHT_UP:
					GUI.DrawTexture(squares[i, rows - j - 1].GetRect(fadeBalance / ((float)(i + j) / (float)(columns + rows))), texture);
					break;
				case Direction.VERTICAL_DOWN:
					GUI.DrawTexture(squares[i, j].GetRect(GetLinearT(j, rows)), texture);
					break;
				case Direction.VERTICAL_UP:
					GUI.DrawTexture(squares[i, rows - j - 1].GetRect(GetLinearT(j, rows)), texture);
					break;
				case Direction.HORIZONTAL_RIGHT:
					GUI.DrawTexture(squares[i, j].GetRect(GetLinearT(i, columns)), texture);
					break;
				case Direction.HORIZONTAL_LEFT:
					GUI.DrawTexture(squares[columns - i - 1, rows - j - 1].GetRect(GetLinearT(i, columns)), texture);
					break;
				case Direction.NONE:
					GUI.DrawTexture(squares[i, j].GetRect(fadeBalance), texture);
					break;
				}
			}
		}
	}
}
