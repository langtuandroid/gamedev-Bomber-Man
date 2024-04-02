using UnityEngine;

namespace XX
{
	public class Zombie : Singleton<Zombie>
	{
		public Vector2[] listVT;

		public float speed;

		public float newspeed;

		private Vector2 vt;

		private Rigidbody2D rb2d;

		public int x;

		private int a;

		private Animator anim;

		private void Start()
		{
			a = 0;
			anim = GetComponent<Animator>();
			listVT = new Vector2[5];
			speed = 1f;
			rb2d = GetComponent<Rigidbody2D>();
			listVT[0] = new Vector2(1f, 0f);
			listVT[1] = new Vector2(-1f, 0f);
			listVT[2] = new Vector2(0f, 1f);
			listVT[3] = new Vector2(0f, -1f);
			vt = listVT[x];
		}

		private void Update()
		{
			setOrderLayer();
			if (vt == listVT[0])
			{
				anim.SetBool("isRun", true);
				anim.SetBool("isPlay", true);
				change(1f, 0f);
			}
			else if (vt == listVT[1])
			{
				anim.SetBool("isRun", true);
				anim.SetBool("isPlay", true);
				change(-1f, 0f);
			}
			else if (vt == listVT[2])
			{
				anim.SetBool("isRun", true);
				anim.SetBool("isPlay", true);
				change(0f, 1f);
			}
			else if (vt == listVT[3])
			{
				anim.SetBool("isRun", true);
				anim.SetBool("isPlay", true);
				change(0f, -1f);
			}
			base.transform.Translate(vt * speed * Time.deltaTime);
		}

		public void change(float x, float y)
		{
			anim.SetFloat("moveX", x);
			anim.SetFloat("moveY", y);
		}

		private void OnTriggerEnter2D(Collider2D coll)
		{
			if (!(coll.gameObject.tag == "Box") && !(coll.gameObject.tag == "Wall") && !(coll.gameObject.tag == "Bomb"))
			{
				return;
			}
			a++;
			Debug.Log("A: " + a);
			if (a % 3 == 0)
			{
				if (vt == listVT[1])
				{
					vt = listVT[2];
				}
				else if (vt == listVT[2])
				{
					vt = listVT[0];
				}
				else if (vt == listVT[0])
				{
					vt = listVT[3];
				}
				else if (vt == listVT[3])
				{
					vt = listVT[1];
				}
			}
			else if (a % 10 == 0)
			{
				if (vt == listVT[1])
				{
					vt = listVT[2];
				}
				else if (vt == listVT[0])
				{
					vt = listVT[2];
				}
				else if (vt == listVT[2])
				{
					vt = listVT[0];
				}
				else if (vt == listVT[3])
				{
					vt = listVT[0];
				}
			}
			else if (a % 20 == 0)
			{
				if (vt == listVT[1])
				{
					vt = listVT[3];
				}
				else if (vt == listVT[0])
				{
					vt = listVT[3];
				}
				else if (vt == listVT[2])
				{
					vt = listVT[1];
				}
				else if (vt == listVT[3])
				{
					vt = listVT[1];
				}
			}
			else if (vt == listVT[0])
			{
				vt = listVT[1];
			}
			else if (vt == listVT[1])
			{
				vt = listVT[0];
			}
			else if (vt == listVT[2])
			{
				vt = listVT[3];
			}
			else if (vt == listVT[3])
			{
				vt = listVT[2];
			}
		}

		public void setOrderLayer()
		{
			float num = base.transform.position.y;
			if (num <= 2.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 97;
			}
			else if (num <= 3.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 96;
			}
			else if (num <= 4.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 95;
			}
			else if (num <= 5.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 94;
			}
			else if (num <= 6.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 93;
			}
			else if (num <= 7.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 92;
			}
			else if (num <= 8.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 91;
			}
			else if (num <= 9.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
			}
			else if (num <= 10.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
			}
			else if (num <= 11.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 88;
			}
			else if (num <= 12.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 87;
			}
			else if (num <= 13.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 86;
			}
			else if (num <= 14.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 85;
			}
			else if (num <= 15.5f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 84;
			}
		}
	}
}