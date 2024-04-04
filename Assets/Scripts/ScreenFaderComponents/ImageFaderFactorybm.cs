using UnityEngine;

namespace ScreenFaderComponents
{
    public class ImageFaderFactorybm : FaderFactoryBasebm
    {
        private readonly Texture _texturebm;

        public ImageFaderFactorybm(Texture texturebm)
        {
            _texturebm = texturebm;
        }

        public override Faderbm CreateFader(GameObject go)
        {
            var faderbm = GetFader<ImageScreenFaderbm>(go);
            faderbm.image = _texturebm;
            return faderbm;
        }
    }
}