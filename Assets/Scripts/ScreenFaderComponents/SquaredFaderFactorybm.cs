using UnityEngine;

namespace ScreenFaderComponents
{
    public class SquaredFaderFactorybm : FaderFactoryBasebm
    {
        private readonly int _columnsbm;

        private readonly SquaredScreenFaderbm.Direction _directionbm;

        private readonly Texture _texturebm;

        public SquaredFaderFactorybm(int columnsbm, SquaredScreenFaderbm.Direction directionbm)
            : this(columnsbm, directionbm, null)
        {
        }

        public SquaredFaderFactorybm(int columnsbm, SquaredScreenFaderbm.Direction directionbm, Texture texturebm)
        {
            _columnsbm = columnsbm;
            _directionbm = directionbm;
            _texturebm = texturebm;
        }

        public override Faderbm CreateFader(GameObject go)
        {
            var faderbm = GetFader<SquaredScreenFaderbm>(go);
            faderbm.columns = _columnsbm;
            faderbm.direction = _directionbm;
            if (_texturebm != null) faderbm.texture = _texturebm;
            return faderbm;
        }
    }
}