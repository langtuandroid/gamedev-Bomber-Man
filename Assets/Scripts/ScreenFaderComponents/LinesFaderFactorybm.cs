using UnityEngine;

namespace ScreenFaderComponents
{
    public class LinesFaderFactorybm : FaderFactoryBasebm
    {
        private readonly LinesScreenFaderbm.Direction _directionbm;

        private readonly Texture[] _imagesbm;
        private readonly int _stripesbm;

        public LinesFaderFactorybm(int stripesbm, LinesScreenFaderbm.Direction directionbm)
        {
            _stripesbm = stripesbm;
            _directionbm = directionbm;
        }

        public LinesFaderFactorybm(int stripesbm, LinesScreenFaderbm.Direction directionbm, Texture[] imagesbm)
        {
            _stripesbm = stripesbm;
            _directionbm = directionbm;
            _imagesbm = imagesbm;
        }

        public override Faderbm CreateFader(GameObject go)
        {
            var faderbm = GetFader<LinesScreenFaderbm>(go);
            faderbm.direction = _directionbm;
            faderbm.numberOfStripes = _stripesbm;
            if (_imagesbm != null && _imagesbm.Length > 0) faderbm.AddTextures(_imagesbm);
            return faderbm;
        }
    }
}