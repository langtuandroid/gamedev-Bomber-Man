using UnityEngine;

namespace XX
{
	public class GameController : MonoBehaviour
	{
		public GameObject levelHolder;

		public const int X = 30;

		public const int Y = 20;

		public GameObject[,] level = new GameObject[30, 20];

		private void Start()
		{
			Transform[] componentsInChildren = levelHolder.GetComponentsInChildren<Transform>();
			Transform[] array = componentsInChildren;
			foreach (Transform transform in array)
			{
				if (transform.gameObject.tag != "Floor")
				{
					level[(int)transform.transform.position.x, (int)transform.transform.position.y] = transform.gameObject;
				}
			}
			level[0, 0] = null;
		}
	}
}
