using ScreenFaderComponents.Actions;
using ScreenFaderComponents.Enumerators;

namespace ScreenFaderComponents
{
    public class FaderTaskbm
    {
        public IAction action;

        public IParametrizedAction pAction;

        public object[] pActionParameters;

        public float PostDelay;
        public FadeState State;

        public float Time;
    }
}