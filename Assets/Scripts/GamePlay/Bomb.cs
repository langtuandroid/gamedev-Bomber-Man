using UnityEngine;

namespace XX
{
	public class Bomb : MonoBehaviour
	{
		public GameObject fire;

		public int firePower;

		public int firePowerMax;

		public float fuse;

		private GameController gc;

		private Vector3[] directions = new Vector3[4]
		{
			Vector3.up,
			Vector3.down,
			Vector3.left,
			Vector3.right
		};

		private AudioSource src1;

		private AudioClip auclip1;

		public GameObject audioSoundF1;

		private void Awake()
		{
			audioSoundF1 = GameObject.Find("SoundBoom");
			src1 = audioSoundF1.GetComponent<AudioSource>();
			src1.Stop();
		}

		private void Start()
		{
			firePowerMax = 4;
			Invoke("Explode", fuse);
			gc = GameObject.Find("GameController").GetComponent<GameController>();
		}

		public void Explode()
		{
			CancelInvoke("Explode");
			Object.Instantiate(fire, base.transform.position, Quaternion.identity);
			Vector3[] array = directions;
			foreach (Vector3 offset in array)
			{
				SpawnFire(offset);
			}
			src1.Play();
			Singleton<BoomSpawner>.Instance.check = true;
			Object.Destroy(base.gameObject);
			Singleton<BoomSpawner>.Instance.numberOfBombs++;
		}

		private void SpawnFire(Vector3 offset, int fire = 1)
		{
			if (firePower <= firePowerMax)
			{
				int value = (int)base.transform.position.x + (int)offset.x * fire;
				int value2 = (int)base.transform.position.y + (int)offset.y * fire;
				value = Mathf.Clamp(value, 0, 29);
				value2 = Mathf.Clamp(value2, 0, 19);
				if (fire < firePower && gc.level[value, value2] == null)
				{
					Object.Instantiate(this.fire, base.transform.position + offset * fire, Quaternion.identity);
					SpawnFire(offset, ++fire);
				}
				else if (fire < firePower && gc.level[value, value2] != null && ((gc.level[value, value2] != null && gc.level[value, value2].tag == "Box") || (gc.level[value, value2] != null && gc.level[value, value2].tag == "Items") || (gc.level[value, value2] != null && gc.level[value, value2].tag == "Zombie") || (gc.level[value, value2] != null && gc.level[value, value2].tag == "ItemsBoot") || (gc.level[value, value2] != null && gc.level[value, value2].tag == "Untagged")))
				{
					Object.Instantiate(this.fire, base.transform.position + offset * fire, Quaternion.identity);
				}
			}
			else if (firePower > firePowerMax)
			{
				int value3 = (int)base.transform.position.x + (int)offset.x * fire;
				int value4 = (int)base.transform.position.y + (int)offset.y * fire;
				value3 = Mathf.Clamp(value3, 0, 29);
				value4 = Mathf.Clamp(value4, 0, 19);
				if (fire < firePowerMax && gc.level[value3, value4] == null)
				{
					Object.Instantiate(this.fire, base.transform.position + offset * fire, Quaternion.identity);
					SpawnFire(offset, ++fire);
				}
				else if (fire < firePowerMax && gc.level[value3, value4] != null && ((gc.level[value3, value4] != null && gc.level[value3, value4].tag == "Box") || (gc.level[value3, value4] != null && gc.level[value3, value4].tag == "Items") || (gc.level[value3, value4] != null && gc.level[value3, value4].tag == "Zombie") || (gc.level[value3, value4] != null && gc.level[value3, value4].tag == "ItemsBoot") || (gc.level[value3, value4] != null && gc.level[value3, value4].tag == "Untagged")))
				{
					Object.Instantiate(this.fire, base.transform.position + offset * fire, Quaternion.identity);
				}
			}
		}

		public void setOrderLayer()
		{
			switch ((int)base.transform.position.y)
			{
			case 2:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 96;
				break;
			case 3:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 95;
				break;
			case 4:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 94;
				break;
			case 5:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 93;
				break;
			case 6:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 92;
				break;
			case 7:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 91;
				break;
			case 8:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
				break;
			case 9:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
				break;
			case 10:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 88;
				break;
			case 11:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 87;
				break;
			case 12:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 86;
				break;
			case 13:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 85;
				break;
			case 14:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 84;
				break;
			case 15:
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 83;
				break;
			}
		}

		private void Update()
		{
			setOrderLayer();
		}

		public void OnTriggerExit2D(Collider2D collision)
		{
		}
	}
}
