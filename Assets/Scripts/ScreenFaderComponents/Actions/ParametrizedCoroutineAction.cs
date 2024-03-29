using System;
using UnityEngine;

namespace ScreenFaderComponents.Actions
{
	public class ParametrizedCoroutineAction : IParametrizedAction
	{
		private FaderCoroutine result;

		public bool Completed { get; set; }

		public void Execute(params object[] parameters)
		{
			if (parameters == null || parameters.Length < 3)
			{
				throw new ArgumentOutOfRangeException();
			}
			MonoBehaviour monoBehaviour = parameters[0] as MonoBehaviour;
			string text = parameters[1] as string;
			object value = parameters[2];
			if (monoBehaviour == null || text == null)
			{
				throw new ArgumentNullException();
			}
			if (result == null)
			{
				result = new FaderCoroutine();
				result.coroutine = monoBehaviour.StartCoroutine(text, value);
			}
			Completed = true;
		}
	}
}
