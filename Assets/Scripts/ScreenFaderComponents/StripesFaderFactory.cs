using UnityEngine;

namespace ScreenFaderComponents
{
	public class StripesFaderFactory : FaderFactoryBase
	{
		private int stripes;

		private StripeScreenFader.Direction direction;

		public StripesFaderFactory(int stripes, StripeScreenFader.Direction direction)
		{
			this.stripes = stripes;
			this.direction = direction;
		}

		public override Fader CreateFader(GameObject go)
		{
			StripeScreenFader fader = GetFader<StripeScreenFader>(go);
			fader.direction = direction;
			fader.numberOfStripes = stripes;
			return fader;
		}
	}
}
