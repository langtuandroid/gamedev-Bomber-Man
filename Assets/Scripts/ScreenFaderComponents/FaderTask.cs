using ScreenFaderComponents.Actions;
using ScreenFaderComponents.Enumerators;

namespace ScreenFaderComponents
{
	public class FaderTask
	{
		public FadeState State;

		public float Time;

		public float PostDelay;

		public IAction action;

		public IParametrizedAction pAction;

		public object[] pActionParameters;
	}
}
