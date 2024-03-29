using System;
using UnityEngine;

namespace ScreenFaderComponents.Actions
{
	public class CoroutineAction : IParametrizedAction
	{
		private FaderCoroutine result;

		public bool Completed { get; set; }

		public void Execute(params object[] parameters)
		{
			if (parameters == null || parameters.Length < 2)
			{
				throw new ArgumentOutOfRangeException();
			}
			MonoBehaviour monoBehaviour = parameters[0] as MonoBehaviour;
			string text = parameters[1] as string;
			if (monoBehaviour == null || text == null)
			{
				throw new ArgumentNullException();
			}
			if (result == null)
			{
				result = new FaderCoroutine();
				result.coroutine = monoBehaviour.StartCoroutine(text);
			}
			Completed = true;
		}
	}
}
