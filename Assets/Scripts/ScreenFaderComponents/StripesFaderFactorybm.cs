using UnityEngine;

namespace ScreenFaderComponents
{
    public class StripesFaderFactorybm : FaderFactoryBasebm
    {
        private readonly StripeScreenFaderbm.Direction _directionbm;
        private readonly int _stripesbm;

        public StripesFaderFactorybm(int stripesbm, StripeScreenFaderbm.Direction directionbm)
        {
            _stripesbm = stripesbm;
            _directionbm = directionbm;
        }

        public override Faderbm CreateFader(GameObject go)
        {
            var faderbm = GetFader<StripeScreenFaderbm>(go);
            faderbm.direction = _directionbm;
            faderbm.numberOfStripes = _stripesbm;
            return faderbm;
        }
    }
}