using UnityEngine;

namespace ScreenFaderComponents
{
	public class SquaredFaderFactory : FaderFactoryBase
	{
		private int columns;

		private SquaredScreenFader.Direction direction;

		private Texture texture;

		public SquaredFaderFactory(int columns, SquaredScreenFader.Direction direction)
			: this(columns, direction, null)
		{
		}

		public SquaredFaderFactory(int columns, SquaredScreenFader.Direction direction, Texture texture)
		{
			this.columns = columns;
			this.direction = direction;
			this.texture = texture;
		}

		public override Fader CreateFader(GameObject go)
		{
			SquaredScreenFader fader = GetFader<SquaredScreenFader>(go);
			fader.columns = columns;
			fader.direction = direction;
			if (texture != null)
			{
				fader.texture = texture;
			}
			return fader;
		}
	}
}
