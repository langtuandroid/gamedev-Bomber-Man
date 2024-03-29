using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
	private static T m_Instance;

	public static T instance
	{
		get
		{
			if (m_Instance == null)
			{
				m_Instance = Object.FindObjectOfType(typeof(T)) as T;
				if (m_Instance == null)
				{
					m_Instance = new GameObject("Singleton of " + typeof(T).ToString(), typeof(T)).GetComponent<T>();
					m_Instance.Init();
				}
			}
			return m_Instance;
		}
	}

	private void Awake()
	{
		if (m_Instance == null)
		{
			m_Instance = this as T;
		}
	}

	public virtual void Init()
	{
	}

	private void OnApplicationQuit()
	{
		m_Instance = (T)null;
	}
}
