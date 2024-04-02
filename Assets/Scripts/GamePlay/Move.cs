using System.Collections;
using UnityEngine;

namespace XX
{
	public class Move : Singleton<Move>
	{
		public float speed;

		public float newspeed;

		private Soldier soldier;

		private PolygonCollider2D cir2d;

		private bool checkColor;

		private bool checkzomcl;

		public GameObject cam;

		private float _minX = 9.4f;

		private float _maxX = 17.6f;

		private float _maxY = 11.6f;

		private float _minY = 5.36f;

		private float sp = 20f;

		public float weight;

		public float hight;

		private Animator anim;

		private bool isUp;

		private bool isDown;

		private bool isRight;

		private bool isLeft;

		public bool point_Up;

		public bool point_Down;

		public bool point_Right;

		public bool point_Left;

		public bool point_Up2;

		public bool point_Down2;

		public bool point_Right2;

		public bool point_Left2;

		public Transform[] lstAttackCheck;

		public float radiusAttack;

		public LayerMask whatIsWall;

		public AudioSource audioPlayerDie;

		private void Start()
		{
			isUp = (isDown = (isRight = (isLeft = false)));
			speed = 2.2f;
			PlayerPrefs.SetFloat("speed", speed);
			newspeed = 2.2f;
			anim = GetComponent<Animator>();
			soldier = GameObject.Find("Soldier").GetComponent<Soldier>();
			cir2d = base.gameObject.GetComponent<PolygonCollider2D>();
		}

		private void Update()
		{
			setOrderLayer();
			if (checkzomcl)
			{
				if (!checkColor)
				{
					base.gameObject.GetComponent<SpriteRenderer>().color = new Color(base.gameObject.GetComponent<SpriteRenderer>().color.r, base.gameObject.GetComponent<SpriteRenderer>().color.g, base.gameObject.GetComponent<SpriteRenderer>().color.b, 0f);
					checkColor = true;
				}
				else
				{
					base.gameObject.GetComponent<SpriteRenderer>().color = new Color(base.gameObject.GetComponent<SpriteRenderer>().color.r, base.gameObject.GetComponent<SpriteRenderer>().color.g, base.gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
					checkColor = false;
				}
				StartCoroutine(NhapNhay());
			}
			else
			{
				base.gameObject.GetComponent<SpriteRenderer>().color = new Color(base.gameObject.GetComponent<SpriteRenderer>().color.r, base.gameObject.GetComponent<SpriteRenderer>().color.g, base.gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
			}
		}

		private void FixedUpdate()
		{
			cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10f);
			cam.transform.position = Vector3.MoveTowards(cam.transform.position, base.gameObject.transform.position, sp * Time.deltaTime);
			cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, _minX, _maxX), Mathf.Clamp(cam.transform.position.y, _minY, _maxY), -10f);
		}

		public void change(float x, float y)
		{
			anim.SetFloat("moveX", x);
			anim.SetFloat("moveY", y);
		}

		public void MoveControl(float x, float y)
		{
			if (x > 0f)
			{
				if (x > Mathf.Abs(y))
				{
					anim.SetBool("isRun", true);
					anim.SetBool("isPlay", true);
					isRight = true;
					isUp = (isDown = (isLeft = false));
					change(1f, 0f);
					base.transform.Translate(Vector2.right * speed * Time.deltaTime);
				}
				else if (x <= y)
				{
					isUp = true;
					isDown = (isLeft = (isRight = false));
					anim.SetBool("isRun", true);
					anim.SetBool("isPlay", true);
					change(0f, 1f);
					base.transform.Translate(Vector2.up * speed * Time.deltaTime);
				}
				else if (y < 0f && x <= Mathf.Abs(y))
				{
					isDown = true;
					isUp = (isLeft = (isRight = false));
					anim.SetBool("isRun", true);
					anim.SetBool("isPlay", true);
					change(0f, -1f);
					base.transform.Translate(-Vector2.up * speed * Time.deltaTime);
				}
			}
			else if (x < 0f)
			{
				isLeft = true;
				isUp = (isDown = (isRight = false));
				if (Mathf.Abs(x) > Mathf.Abs(y))
				{
					anim.SetBool("isRun", true);
					anim.SetBool("isPlay", true);
					change(-1f, 0f);
					base.transform.Translate(-Vector2.right * speed * Time.deltaTime);
				}
				else if (Mathf.Abs(x) <= y)
				{
					isUp = true;
					isDown = (isLeft = (isRight = false));
					anim.SetBool("isRun", true);
					anim.SetBool("isPlay", true);
					change(0f, 1f);
					base.transform.Translate(Vector2.up * speed * Time.deltaTime);
				}
				else if (y < 0f && Mathf.Abs(x) <= Mathf.Abs(y))
				{
					isDown = true;
					isUp = (isLeft = (isRight = false));
					anim.SetBool("isRun", true);
					anim.SetBool("isPlay", true);
					change(0f, -1f);
					base.transform.Translate(-Vector2.up * speed * Time.deltaTime);
				}
			}
			else if (Mathf.Abs(x) == 0f && Mathf.Abs(y) == 0f)
			{
				anim.SetBool("isRun", false);
				isUp = (isDown = (isLeft = (isRight = false)));
			}
			point_Up = Physics2D.OverlapCircle(lstAttackCheck[0].position, radiusAttack, whatIsWall);
			point_Down = Physics2D.OverlapCircle(lstAttackCheck[6].position, radiusAttack, whatIsWall);
			point_Right = Physics2D.OverlapCircle(lstAttackCheck[2].position, radiusAttack, whatIsWall);
			point_Left = Physics2D.OverlapCircle(lstAttackCheck[4].position, radiusAttack, whatIsWall);
			point_Up2 = Physics2D.OverlapCircle(lstAttackCheck[1].position, radiusAttack, whatIsWall);
			point_Down2 = Physics2D.OverlapCircle(lstAttackCheck[7].position, radiusAttack, whatIsWall);
			point_Right2 = Physics2D.OverlapCircle(lstAttackCheck[3].position, radiusAttack, whatIsWall);
			point_Left2 = Physics2D.OverlapCircle(lstAttackCheck[5].position, radiusAttack, whatIsWall);
			if (point_Up && isUp)
			{
				speed = 0f;
			}
			else if (point_Down && isDown)
			{
				speed = 0f;
			}
			else if (point_Right && isRight)
			{
				speed = 0f;
			}
			else if (point_Left && isLeft)
			{
				speed = 0f;
			}
			else if (point_Up2 && isUp)
			{
				speed = 0f;
			}
			else if (point_Down2 && isDown)
			{
				speed = 0f;
			}
			else if (point_Right2 && isRight)
			{
				speed = 0f;
			}
			else if (point_Left2 && isLeft)
			{
				speed = 0f;
			}
			else
			{
				speed = newspeed;
			}
			PlayerPrefs.SetFloat("speed", speed);
		}

		public void setOrderLayer()
		{
			float num = base.transform.position.y;
			if (num <= 2.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 97;
			}
			else if (num <= 3.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 96;
			}
			else if (num <= 4.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 95;
			}
			else if (num <= 5.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 94;
			}
			else if (num <= 6.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 93;
			}
			else if (num <= 7.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 92;
			}
			else if (num <= 8.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 91;
			}
			else if (num <= 9.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
			}
			else if (num <= 10.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
			}
			else if (num <= 11.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 88;
			}
			else if (num <= 12.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 87;
			}
			else if (num <= 13.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 86;
			}
			else if (num <= 14.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 85;
			}
			else if (num <= 15.7f)
			{
				base.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 84;
			}
		}

		private void OnTriggerEnter2D(Collider2D coll)
		{
			if (coll.gameObject.tag == "Zombie")
			{
				if (soldier.armor)
				{
					soldier.armor = false;
					cir2d.enabled = false;
					StartCoroutine(Example());
				}
				else
				{
					soldier.heart--;
					cir2d.enabled = false;
					audioPlayerDie.Play();
					StartCoroutine(Example());
					checkzomcl = true;
					Singleton<BoomSpawner>.Instance.numberOfBombs = 1;
					Singleton<BoomSpawner>.Instance.firePower = 2;
					Singleton<Soldier>.Instance.speedValue = 1f;
					Singleton<Move>.Instance.newspeed = 2f;
				}
			}
			if (coll.gameObject.tag == "fire")
			{
				if (soldier.armor)
				{
					audioPlayerDie.Play();
					cir2d.enabled = false;
					StartCoroutine(Example());
				}
				else
				{
					audioPlayerDie.Play();
					StartCoroutine(Example());
					checkzomcl = true;
				}
			}
			if (soldier.heart < 1)
			{
				audioPlayerDie.Stop();
			}
		}

		public IEnumerator Example()
		{
			yield return new WaitForSeconds(1.5f);
			cir2d.enabled = true;
		}

		public IEnumerator Delay()
		{
			yield return new WaitForSeconds(1f);
			cir2d.enabled = true;
		}

		public IEnumerator NhapNhay()
		{
			yield return new WaitForSeconds(1.5f);
			checkzomcl = false;
		}
	}
}
