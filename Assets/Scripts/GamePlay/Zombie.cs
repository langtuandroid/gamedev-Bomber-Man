using UnityEngine;

namespace GamePlay
{
    public class Zombie : Singleton<Zombie>
    {
        public Vector2[] listVT;

        public float speed;

        public float newspeed;

        public int x;

        private int a;

        private Animator anim;

        private Rigidbody2D rb2dbm;

        private Vector2 vectobmr;

        private MapManagerbm _mapManagerbm;
        private void Start()
        {
            a = 0;
            anim = GetComponent<Animator>();
            listVT = new Vector2[5];
            speed = 1f;
            rb2dbm = GetComponent<Rigidbody2D>();
            listVT[0] = new Vector2(1f, 0f);
            listVT[1] = new Vector2(-1f, 0f);
            listVT[2] = new Vector2(0f, 1f);
            listVT[3] = new Vector2(0f, -1f);
            vectobmr = listVT[x];
            _mapManagerbm = GameObject.Find("GameManager(Clone)").GetComponent<MapManagerbm>();
        }

        private void Update()
        {
            setOrderLayer();
            if (vectobmr == listVT[0])
            {
                anim.SetBool("isRun", true);
                anim.SetBool("isPlay", true);
                change(1f, 0f);
            }
            else if (vectobmr == listVT[1])
            {
                anim.SetBool("isRun", true);
                anim.SetBool("isPlay", true);
                change(-1f, 0f);
            }
            else if (vectobmr == listVT[2])
            {
                anim.SetBool("isRun", true);
                anim.SetBool("isPlay", true);
                change(0f, 1f);
            }
            else if (vectobmr == listVT[3])
            {
                anim.SetBool("isRun", true);
                anim.SetBool("isPlay", true);
                change(0f, -1f);
            }

            transform.Translate(vectobmr * speed * Time.deltaTime);
        }

        public void DieZombie()
        {
            if (_mapManagerbm.zombieCount > 0)
            {
                _mapManagerbm.zombieCount--;
                print("_mapManagerbm.zombieCount = " + _mapManagerbm.zombieCount);
            }
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (!(coll.gameObject.tag == "Box") && !(coll.gameObject.tag == "Wall") &&
                !(coll.gameObject.tag == "Bomb")) return;
            a++;
            //Debug.Log("A: " + a);
            if (a % 3 == 0)
            {
                if (vectobmr == listVT[1])
                    vectobmr = listVT[2];
                else if (vectobmr == listVT[2])
                    vectobmr = listVT[0];
                else if (vectobmr == listVT[0])
                    vectobmr = listVT[3];
                else if (vectobmr == listVT[3]) vectobmr = listVT[1];
            }
            else if (a % 10 == 0)
            {
                if (vectobmr == listVT[1])
                    vectobmr = listVT[2];
                else if (vectobmr == listVT[0])
                    vectobmr = listVT[2];
                else if (vectobmr == listVT[2])
                    vectobmr = listVT[0];
                else if (vectobmr == listVT[3]) vectobmr = listVT[0];
            }
            else if (a % 20 == 0)
            {
                if (vectobmr == listVT[1])
                    vectobmr = listVT[3];
                else if (vectobmr == listVT[0])
                    vectobmr = listVT[3];
                else if (vectobmr == listVT[2])
                    vectobmr = listVT[1];
                else if (vectobmr == listVT[3]) vectobmr = listVT[1];
            }
            else if (vectobmr == listVT[0])
            {
                vectobmr = listVT[1];
            }
            else if (vectobmr == listVT[1])
            {
                vectobmr = listVT[0];
            }
            else if (vectobmr == listVT[2])
            {
                vectobmr = listVT[3];
            }
            else if (vectobmr == listVT[3])
            {
                vectobmr = listVT[2];
            }
        }

        public void change(float x, float y)
        {
            anim.SetFloat("moveX", x);
            anim.SetFloat("moveY", y);
        }

        public void setOrderLayer()
        {
            var num = transform.position.y;
            if (num <= 2.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 97;
            else if (num <= 3.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 96;
            else if (num <= 4.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 95;
            else if (num <= 5.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 94;
            else if (num <= 6.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 93;
            else if (num <= 7.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 92;
            else if (num <= 8.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 91;
            else if (num <= 9.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
            else if (num <= 10.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
            else if (num <= 11.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 88;
            else if (num <= 12.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 87;
            else if (num <= 13.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 86;
            else if (num <= 14.5f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 85;
            else if (num <= 15.5f) gameObject.GetComponent<SpriteRenderer>().sortingOrder = 84;
        }
    }
}