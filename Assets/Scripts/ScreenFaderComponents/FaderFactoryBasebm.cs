using UnityEngine;

namespace ScreenFaderComponents
{
	public abstract class FaderFactoryBasebm
	{
		public abstract Faderbm CreateFader(GameObject go);

		protected T GetFader<T>(GameObject go) where T : Faderbm
		{
			T val = FindFaderbm<T>();
			if (val == null)
			{
				val = CreateFader<T>(go);
			}
			return val;
		}

		private T FindFaderbm<T>() where T : Faderbm
		{
			return Object.FindObjectOfType(typeof(T)) as T;
		}

		private T CreateFader<T>(GameObject go) where T : Faderbm
		{
			if (go == null)
			{
				go = new GameObject("Patico_ScreenFader");
			}
			return go.AddComponent<T>();
		}
	}
}
