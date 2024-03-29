using System;
using UnityEngine;

namespace ScreenFaderComponents.Actions
{
	public class ChangeColorAction : IParametrizedAction
	{
		public bool Completed { get; set; }

		public void Execute(params object[] args)
		{
			if (args == null || args.Length == 0)
			{
				throw new ArgumentNullException();
			}
			Fader fader = args[1] as Fader;
			if (fader != null && args[0] is Color)
			{
				fader.color = (Color)args[0];
			}
			Completed = true;
		}
	}
}
