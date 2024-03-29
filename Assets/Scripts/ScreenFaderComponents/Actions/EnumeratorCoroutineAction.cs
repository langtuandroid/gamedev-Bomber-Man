using System;
using System.Collections;
using UnityEngine;

namespace ScreenFaderComponents.Actions
{
	public class EnumeratorCoroutineAction : IParametrizedAction
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
			IEnumerator enumerator = parameters[1] as IEnumerator;
			if (monoBehaviour == null || enumerator == null)
			{
				throw new ArgumentNullException();
			}
			if (result == null)
			{
				result = new FaderCoroutine();
				result.coroutine = monoBehaviour.StartCoroutine(result.IntCoroutine(enumerator));
			}
			Completed = result.Completed;
		}
	}
}
