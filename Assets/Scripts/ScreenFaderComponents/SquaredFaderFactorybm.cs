using UnityEngine;

namespace ScreenFaderComponents
{
	public class SquaredFaderFactorybm : FaderFactoryBasebm
	{
		private int _columnsbm;

		private SquaredScreenFaderbm.Direction _directionbm;

		private Texture _texturebm;

		public SquaredFaderFactorybm(int columnsbm, SquaredScreenFaderbm.Direction directionbm)
			: this(columnsbm, directionbm, null)
		{
		}

		public SquaredFaderFactorybm(int columnsbm, SquaredScreenFaderbm.Direction directionbm, Texture texturebm)
		{
			this._columnsbm = columnsbm;
			this._directionbm = directionbm;
			this._texturebm = texturebm;
		}

		public override Faderbm CreateFader(GameObject go)
		{
			SquaredScreenFaderbm faderbm = GetFader<SquaredScreenFaderbm>(go);
			faderbm.columns = _columnsbm;
			faderbm.direction = _directionbm;
			if (_texturebm != null)
			{
				faderbm.texture = _texturebm;
			}
			return faderbm;
		}
	}
}
