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
			Faderbm faderbm = args[1] as Faderbm;
			if (faderbm != null && args[0] is Color)
			{
				faderbm.color = (Color)args[0];
			}
			Completed = true;
		}
	}
}
