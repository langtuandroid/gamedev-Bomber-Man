using UnityEngine;

namespace ScreenFaderComponents
{
	public class StripesFaderFactorybm : FaderFactoryBasebm
	{
		private int _stripesbm;

		private StripeScreenFaderbm.Direction _directionbm;

		public StripesFaderFactorybm(int stripesbm, StripeScreenFaderbm.Direction directionbm)
		{
			this._stripesbm = stripesbm;
			this._directionbm = directionbm;
		}

		public override Faderbm CreateFader(GameObject go)
		{
			StripeScreenFaderbm faderbm = GetFader<StripeScreenFaderbm>(go);
			faderbm.direction = _directionbm;
			faderbm.numberOfStripes = _stripesbm;
			return faderbm;
		}
	}
}
