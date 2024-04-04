using ScreenFaderComponents.Enumerators;
using UnityEngine.UI;

namespace ScreenFaderComponents.Actions
{
    public class CanvasImageFadingAction : GameObjectFadingAction
    {
        private readonly Image _obj;

        public CanvasImageFadingAction(FadeDirection direction, Image obj, float time)
        {
            this.direction = direction;
            this.time = time;
            _obj = obj;
        }

        protected override void Apply(float value)
        {
            var color = _obj.color;
            color.a = value;
            _obj.color = color;
        }
    }
}