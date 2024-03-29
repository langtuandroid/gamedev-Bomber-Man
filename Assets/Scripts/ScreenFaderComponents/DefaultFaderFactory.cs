using UnityEngine;

namespace ScreenFaderComponents
{
	public class DefaultFaderFactory : FaderFactoryBase
	{
		public override Fader CreateFader(GameObject go)
		{
			return GetFader<DefaultScreenFader>(go);
		}
	}
}
