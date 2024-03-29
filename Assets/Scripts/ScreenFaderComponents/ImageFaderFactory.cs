using UnityEngine;

namespace ScreenFaderComponents
{
	public class ImageFaderFactory : FaderFactoryBase
	{
		private Texture texture;

		public ImageFaderFactory(Texture texture)
		{
			this.texture = texture;
		}

		public override Fader CreateFader(GameObject go)
		{
			ImageScreenFader fader = GetFader<ImageScreenFader>(go);
			fader.image = texture;
			return fader;
		}
	}
}
