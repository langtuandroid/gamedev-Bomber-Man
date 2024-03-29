using UnityEngine;

namespace ScreenFaderComponents
{
	public abstract class FaderFactoryBase
	{
		public abstract Fader CreateFader(GameObject go);

		protected T GetFader<T>(GameObject go) where T : Fader
		{
			T val = FindFader<T>();
			if (val == null)
			{
				val = CreateFader<T>(go);
			}
			return val;
		}

		private T FindFader<T>() where T : Fader
		{
			return Object.FindObjectOfType(typeof(T)) as T;
		}

		private T CreateFader<T>(GameObject go) where T : Fader
		{
			if (go == null)
			{
				go = new GameObject("Patico_ScreenFader");
			}
			return go.AddComponent<T>();
		}
	}
}
