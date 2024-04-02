using System.Collections;
using UnityEngine;

namespace XX
{
	public class BoomSpawner : Singleton<BoomSpawner>
	{
		public GameObject bomb;

		private GameController gc;

		public int firePower = 1;

		public int numberOfBombs = 1;

		public float fuse = 3f;

		private AudioSource src1;

		private AudioClip auclip1;

		public GameObject audioSoundF1;

		public bool check;

		public bool check1;

		private void Start()
		{
			check = true;
			check1 = true;
			audioSoundF1 = GameObject.Find("SoundClickBoom");
			src1 = audioSoundF1.GetComponent<AudioSource>();
			src1.Stop();
		}

		private void Update()
		{
		}

		public void clickBomb()
		{
			if (check1)
			{
				if (numberOfBombs >= 1 && check)
				{
					Vector2 vector = new Vector2(Mathf.Round(base.transform.position.x), Mathf.Round(base.transform.position.y - 0.3f));
					GameObject gameObject = Object.Instantiate(bomb, vector, Quaternion.identity);
					gameObject.GetComponent<Bomb>().firePower = firePower;
					gameObject.GetComponent<Bomb>().fuse = fuse;
					numberOfBombs--;
					src1.Play();
				}
				StartCoroutine(timeClickBomb());
			}
		}

		public void OnTriggerStay2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Bomb")
			{
				check = false;
				StartCoroutine(timeDestroy());
			}
		}

		public void OnTriggerExit2D(Collider2D collision)
		{
			if (collision.gameObject.tag == "Bomb")
			{
				check = true;
			}
		}

		private IEnumerator timeDestroy()
		{
			yield return new WaitForSeconds(3f);
			check = true;
		}

		private IEnumerator timeClickBomb()
		{
			yield return new WaitForSeconds(0f);
			check1 = false;
			yield return new WaitForSeconds(0.5f);
			check1 = true;
		}
	}
}
