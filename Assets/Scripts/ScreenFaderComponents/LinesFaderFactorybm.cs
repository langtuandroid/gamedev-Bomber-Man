using UnityEngine;

namespace ScreenFaderComponents
{
	public class LinesFaderFactorybm : FaderFactoryBasebm
	{
		private int _stripesbm;

		private LinesScreenFaderbm.Direction _directionbm;

		private Texture[] _imagesbm;

		public LinesFaderFactorybm(int stripesbm, LinesScreenFaderbm.Direction directionbm)
		{
			this._stripesbm = stripesbm;
			this._directionbm = directionbm;
		}

		public LinesFaderFactorybm(int stripesbm, LinesScreenFaderbm.Direction directionbm, Texture[] imagesbm)
		{
			this._stripesbm = stripesbm;
			this._directionbm = directionbm;
			this._imagesbm = imagesbm;
		}

		public override Faderbm CreateFader(GameObject go)
		{
			LinesScreenFaderbm faderbm = GetFader<LinesScreenFaderbm>(go);
			faderbm.direction = _directionbm;
			faderbm.numberOfStripes = _stripesbm;
			if (_imagesbm != null && _imagesbm.Length > 0)
			{
				faderbm.AddTextures(_imagesbm);
			}
			return faderbm;
		}
	}
}
