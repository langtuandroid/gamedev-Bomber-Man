using System;
using UnityEngine;

namespace ScreenFaderComponents.Actions
{
	public class LoadSceneAction : IParametrizedAction
	{
		public bool Completed { get; set; }

		public void Execute(params object[] args)
		{
			if (args == null || args.Length == 0)
			{
				throw new ArgumentNullException();
			}
			string text = args[0].ToString();
			int result = 0;
			if (int.TryParse(text, out result))
			{
				Application.LoadLevel(result);
			}
			else
			{
				Application.LoadLevel(text);
			}
			Completed = true;
		}
	}
}