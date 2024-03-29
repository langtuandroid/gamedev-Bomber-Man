using UnityEngine;

namespace ScreenFaderComponents
{
	public class LinesFaderFactory : FaderFactoryBase
	{
		private int stripes;

		private LinesScreenFader.Direction direction;

		private Texture[] images;

		public LinesFaderFactory(int stripes, LinesScreenFader.Direction direction)
		{
			this.stripes = stripes;
			this.direction = direction;
		}

		public LinesFaderFactory(int stripes, LinesScreenFader.Direction direction, Texture[] images)
		{
			this.stripes = stripes;
			this.direction = direction;
			this.images = images;
		}

		public override Fader CreateFader(GameObject go)
		{
			LinesScreenFader fader = GetFader<LinesScreenFader>(go);
			fader.direction = direction;
			fader.numberOfStripes = stripes;
			if (images != null && images.Length > 0)
			{
				fader.AddTextures(images);
			}
			return fader;
		}
	}
}
