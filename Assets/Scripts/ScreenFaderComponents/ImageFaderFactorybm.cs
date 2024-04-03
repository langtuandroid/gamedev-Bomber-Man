using UnityEngine;

namespace ScreenFaderComponents
{
	public class ImageFaderFactorybm : FaderFactoryBasebm
	{
		private Texture _texturebm;

		public ImageFaderFactorybm(Texture texturebm)
		{
			this._texturebm = texturebm;
		}

		public override Faderbm CreateFader(GameObject go)
		{
			ImageScreenFaderbm faderbm = GetFader<ImageScreenFaderbm>(go);
			faderbm.image = _texturebm;
			return faderbm;
		}
	}
}
