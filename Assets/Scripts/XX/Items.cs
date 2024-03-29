using UnityEngine;

namespace XX
{
	public class Items : MonoBehaviour
	{
		public int bombs;

		public int firePower;

		public float speed;

		public int heart;

		public GameObject eff;

		private GameController gc;

		private AudioSource src;

		private AudioClip auclip;

		public GameObject audioSoundF;

		private void Awake()
		{
			audioSoundF = GameObject.Find("SoundEatItem");
			src = audioSoundF.GetComponent<AudioSource>();
			src.Stop();
		}

		private void Update()
		{
			setOrderLayer();
		}

		public void Start()
		{
			gc = GameObject.Find("GameController").GetComponent<GameController>();
			gc.level[(int)base.transform.position.x, (int)base.transform.position.y] = base.gameObject;
		}

		public void OnTriggerEnter2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Player")
			{
				eff.SetActive(true);
				GameObject gameObject = Object.Instantiate(eff, base.gameObject.transform.position, Quaternion.identity);
				src.Play();
				Move component = collision.gameObject.GetComponent<Move>();
				BoomSpawner component2 = collision.gameObject.GetComponent<BoomSpawner>();
				Soldier component3 = collision.gameObject.GetComponent<Soldier>();
				component3.heart += heart;
				component.newspeed += speed;
				component2.numberOfBombs += bombs;
				component2.firePower += firePower;
				Debug.Log("+Speed!!!" + collision.gameObject.GetComponent<Move>().newspeed);
				Object.Destroy(base.gameObject);
			}
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
