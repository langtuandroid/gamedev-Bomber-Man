using System.Collections;
using UnityEngine;

namespace XX
{
	public class Loader : Singleton<Loader>
	{
		public GameObject gameManager;

		public GameObject mapholder;

		public float width;

		public float height;

		public AudioSource audioStartLevel;

		private new void Awake()
		{
			if (GameManager.instance == null)
			{
				Object.Instantiate(gameManager);
			}
		}

		private void Start()
		{
			float num = 1.7777778f;
			float num2 = (float)Screen.width / (float)Screen.height;
			float num3 = num2 / num;
			Camera component = GetComponent<Camera>();
			if (num3 < 1f)
			{
				Rect rect = component.rect;
				rect.width = 1f;
				rect.height = num3;
				rect.x = 0f;
				rect.y = (1f - num3) / 2f;
				component.rect = rect;
			}
			else
			{
				float num4 = 1f / num3;
				Rect rect2 = component.rect;
				rect2.width = num4;
				rect2.height = 1f;
				rect2.x = (1f - num4) / 2f;
				rect2.y = 0f;
				component.rect = rect2;
			}
		}

		private void Update()
		{
		}

		private IEnumerator setDelay()
		{
			yield return new WaitForSeconds(10f);
		}
	}
}
