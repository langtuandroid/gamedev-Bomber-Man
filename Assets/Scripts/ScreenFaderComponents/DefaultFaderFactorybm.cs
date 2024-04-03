using UnityEngine;

namespace ScreenFaderComponents
{
	public class DefaultFaderFactorybm : FaderFactoryBasebm
	{
		public override Faderbm CreateFader(GameObject go)
		{
			return GetFader<DefaultScreenFaderbm>(go);
		}
	}
}
