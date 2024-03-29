using UnityEngine;

namespace XX
{
	public class Box : MonoBehaviour
	{
		public GameObject[] items;

		public Transform x;

		public void SpawnPowerUp()
		{
			x = base.gameObject.transform;
			if (Random.Range(0f, 1f) > 0.75f)
			{
				int num = Random.Range(0, items.Length);
				Object.Instantiate(items[num], new Vector3(base.transform.position.x, base.transform.position.y - 0.12f, 0f), Quaternion.identity);
			}
		}

		private void Update()
		{
			setOrderLayer();
		}

		public void setOrderLayer()
		{
			switch ((int)base.transform.position.y)
			{
			case 2:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 97;
				break;
			case 3:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 96;
				break;
			case 4:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 95;
				break;
			case 5:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 94;
				break;
			case 6:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 93;
				break;
			case 7:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 92;
				break;
			case 8:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 91;
				break;
			case 9:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
				break;
			case 10:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
				break;
			case 11:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 88;
				break;
			case 12:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 87;
				break;
			case 13:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 86;
				break;
			case 14:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 85;
				break;
			case 15:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 84;
				break;
			}
		}
	}
}
