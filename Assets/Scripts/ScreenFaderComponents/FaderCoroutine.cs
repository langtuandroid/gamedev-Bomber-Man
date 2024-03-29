using System;
using System.Collections;
using UnityEngine;

namespace ScreenFaderComponents
{
	internal class FaderCoroutine
	{
		private bool completed;

		private Exception exception;

		public Coroutine coroutine;

		public bool Completed
		{
			get
			{
				if (exception != null)
				{
					throw exception;
				}
				return completed;
			}
		}

		public IEnumerator IntCoroutine(IEnumerator coroutine)
		{
			while (true)
			{
				try
				{
					if (!coroutine.MoveNext())
					{
						completed = true;
						break;
					}
				}
				catch (Exception ex)
				{
					exception = ex;
					completed = true;
					break;
				}
				yield return coroutine.Current;
			}
		}
	}
}
