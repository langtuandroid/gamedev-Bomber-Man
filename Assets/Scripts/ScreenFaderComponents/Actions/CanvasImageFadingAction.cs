using ScreenFaderComponents.Enumerators;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenFaderComponents.Actions
{
	public class CanvasImageFadingAction : GameObjectFadingAction
	{
		private Image _obj;

		public CanvasImageFadingAction(FadeDirection direction, Image obj, float time)
		{
			base.direction = direction;
			base.time = time;
			_obj = obj;
		}

		protected override void Apply(float value)
		{
			Color color = _obj.color;
			color.a = value;
			_obj.color = color;
		}
	}
}
