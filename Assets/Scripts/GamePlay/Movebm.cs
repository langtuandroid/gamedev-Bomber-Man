using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class Movebm : Singleton<Movebm>
    {
        public float speed;

        public float newspeed;

        public GameObject cam;

        public float weight;

        public float hight;

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

        private Animator _animbm;

        private bool _checkColorbm;

        private bool _checkzomclbm;

        private bool _isDownbm;

        private bool _isLeftbm;

        private bool _isRightbm;

        private bool _isUpbm;
        [Header("PhoneSettings")]
        [SerializeField]
        private readonly float _maxSXbm = 17.6f;
        [SerializeField]
        private readonly float _maxSYbm = 11.5f;
        [SerializeField]
        private readonly float _minSXbm = 9.4f;
        [SerializeField]
        private readonly float _minSYbm = 5.35f;
        
        [Header("TabletSettings")]
        [SerializeField]
        private readonly float _maxTXbm = 21f;
        [SerializeField]
        private readonly float _maxTYbm = 11.5f;
        [SerializeField]
        private readonly float _minTXbm = 6f;
        [SerializeField]
        private readonly float _minTYbm = 5.35f;
        
        private float _maxXbm = 17.6f;
        private float _maxYbm = 11.5f;
        private float _minXbm = 9.4f;
        private float _minYbm = 5.35f;

        private Soldierbm _soldierbm;

        private PolygonCollider2D cir2dbm;

        private readonly float spbm = 20f;
        
        // Ограничения по осям, выраженные в процентах относительно ширины и высоты экрана
        private readonly float minXPercentage = 0.54f; // Минимальное положение по X (54% от ширины)
        private readonly float maxXPercentage = 1.0f;  // Максимальное положение по X (100% от ширины)
        private readonly float minYPercentage = 0.46f; // Минимальное положение по Y (46% от высоты)
        private readonly float maxYPercentage = 1.0f;  // Максимальное положение по Y (100% от высоты)

        private void Awake()
        {
            CheckDeviceInches();
        }

        private void Start()
        {
            _isUpbm = _isDownbm = _isRightbm = _isLeftbm = false;
            speed = 2.2f;
            PlayerPrefs.SetFloat("speed", speed);
            newspeed = 2.2f;
            _animbm = GetComponent<Animator>();
            _soldierbm = GameObject.Find("Soldier").GetComponent<Soldierbm>();
            cir2dbm = gameObject.GetComponent<PolygonCollider2D>();
        }
        
        private void CheckDeviceInches()
        {
            float screenSizeInchessr = Mathf.Sqrt(Mathf.Pow(Screen.width / Screen.dpi, 2) + Mathf.Pow(Screen.height / Screen.dpi, 2));
            float aspectRatio = (float)Screen.width / Screen.height; 
           
            if (screenSizeInchessr >= 7.0f)
            {
                _maxXbm = Mathf.Approximately(aspectRatio, 3f / 5f) ? _maxSXbm : _maxTXbm;
                _maxYbm = Mathf.Approximately(aspectRatio, 3f / 5f) ? _maxSYbm : _maxTYbm;
                _minXbm = Mathf.Approximately(aspectRatio, 3f / 5f) ? _minSXbm : _minTXbm;
                _minYbm = Mathf.Approximately(aspectRatio, 3f / 5f) ? _minSYbm : _minTYbm;
            }
            else
            {
                _maxXbm = _maxSXbm;
                _maxYbm = _maxSYbm;
                _minXbm = _minSXbm;
                _minYbm = _minSYbm;
            }
        }
        
        private void Update()
        {
            setOrderLayer();
            if (_checkzomclbm)
            {
                if (!_checkColorbm)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(
                        gameObject.GetComponent<SpriteRenderer>().color.r,
                        gameObject.GetComponent<SpriteRenderer>().color.g,
                        gameObject.GetComponent<SpriteRenderer>().color.b, 0f);
                    _checkColorbm = true;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().color = new Color(
                        gameObject.GetComponent<SpriteRenderer>().color.r,
                        gameObject.GetComponent<SpriteRenderer>().color.g,
                        gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
                    _checkColorbm = false;
                }

                StartCoroutine(NhapNhay());
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().color = new Color(
                    gameObject.GetComponent<SpriteRenderer>().color.r,
                    gameObject.GetComponent<SpriteRenderer>().color.g,
                    gameObject.GetComponent<SpriteRenderer>().color.b, 1f);
            }
        }

        private void FixedUpdate()
        {
            cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -10f);
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, gameObject.transform.position,
                spbm * Time.deltaTime);
            
            cam.transform.position = new Vector3(Mathf.Clamp(cam.transform.position.x, _minXbm, _maxXbm),
                Mathf.Clamp(cam.transform.position.y, _minYbm, _maxYbm), -10f);
        }
        

        private void OnTriggerEnter2D(Collider2D coll)
        {
            if (coll.gameObject.tag == "Zombie")
            {
                if (_soldierbm.armor)
                {
                    _soldierbm.armor = false;
                    cir2dbm.enabled = false;
                    StartCoroutine(Example());
                }
                else
                {
                    _soldierbm.heart--;
                    cir2dbm.enabled = false;
                    audioPlayerDie.Play();
                    StartCoroutine(Example());
                    _checkzomclbm = true;
                    Singleton<BoomSpawnerbm>.Instance.numberOfBombs = 1;
                    Singleton<BoomSpawnerbm>.Instance.firePower = 2;
                    Singleton<Soldierbm>.Instance.speedValue = 1f;
                    Instance.newspeed = 2f;
                }
            }

            if (coll.gameObject.tag == "fire")
            {
                if (_soldierbm.armor)
                {
                    audioPlayerDie.Play();
                    cir2dbm.enabled = false;
                    StartCoroutine(Example());
                }
                else
                {
                    audioPlayerDie.Play();
                    StartCoroutine(Example());
                    _checkzomclbm = true;
                }
            }

            if (_soldierbm.heart < 1) audioPlayerDie.Stop();
        }

        public void change(float x, float y)
        {
            _animbm.SetFloat("moveX", x);
            _animbm.SetFloat("moveY", y);
        }

        public void MoveControl(float x, float y)
        {
            if (x > 0f)
            {
                if (x > Mathf.Abs(y))
                {
                    _animbm.SetBool("isRun", true);
                    _animbm.SetBool("isPlay", true);
                    _isRightbm = true;
                    _isUpbm = _isDownbm = _isLeftbm = false;
                    change(1f, 0f);
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                else if (x <= y)
                {
                    _isUpbm = true;
                    _isDownbm = _isLeftbm = _isRightbm = false;
                    _animbm.SetBool("isRun", true);
                    _animbm.SetBool("isPlay", true);
                    change(0f, 1f);
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                }
                else if (y < 0f && x <= Mathf.Abs(y))
                {
                    _isDownbm = true;
                    _isUpbm = _isLeftbm = _isRightbm = false;
                    _animbm.SetBool("isRun", true);
                    _animbm.SetBool("isPlay", true);
                    change(0f, -1f);
                    transform.Translate(-Vector2.up * speed * Time.deltaTime);
                }
            }
            else if (x < 0f)
            {
                _isLeftbm = true;
                _isUpbm = _isDownbm = _isRightbm = false;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    _animbm.SetBool("isRun", true);
                    _animbm.SetBool("isPlay", true);
                    change(-1f, 0f);
                    transform.Translate(-Vector2.right * speed * Time.deltaTime);
                }
                else if (Mathf.Abs(x) <= y)
                {
                    _isUpbm = true;
                    _isDownbm = _isLeftbm = _isRightbm = false;
                    _animbm.SetBool("isRun", true);
                    _animbm.SetBool("isPlay", true);
                    change(0f, 1f);
                    transform.Translate(Vector2.up * speed * Time.deltaTime);
                }
                else if (y < 0f && Mathf.Abs(x) <= Mathf.Abs(y))
                {
                    _isDownbm = true;
                    _isUpbm = _isLeftbm = _isRightbm = false;
                    _animbm.SetBool("isRun", true);
                    _animbm.SetBool("isPlay", true);
                    change(0f, -1f);
                    transform.Translate(-Vector2.up * speed * Time.deltaTime);
                }
            }
            else if (Mathf.Abs(x) == 0f && Mathf.Abs(y) == 0f)
            {
                _animbm.SetBool("isRun", false);
                _isUpbm = _isDownbm = _isLeftbm = _isRightbm = false;
            }

            point_Up = Physics2D.OverlapCircle(lstAttackCheck[0].position, radiusAttack, whatIsWall);
            point_Down = Physics2D.OverlapCircle(lstAttackCheck[6].position, radiusAttack, whatIsWall);
            point_Right = Physics2D.OverlapCircle(lstAttackCheck[2].position, radiusAttack, whatIsWall);
            point_Left = Physics2D.OverlapCircle(lstAttackCheck[4].position, radiusAttack, whatIsWall);
            point_Up2 = Physics2D.OverlapCircle(lstAttackCheck[1].position, radiusAttack, whatIsWall);
            point_Down2 = Physics2D.OverlapCircle(lstAttackCheck[7].position, radiusAttack, whatIsWall);
            point_Right2 = Physics2D.OverlapCircle(lstAttackCheck[3].position, radiusAttack, whatIsWall);
            point_Left2 = Physics2D.OverlapCircle(lstAttackCheck[5].position, radiusAttack, whatIsWall);
            if (point_Up && _isUpbm)
                speed = 0f;
            else if (point_Down && _isDownbm)
                speed = 0f;
            else if (point_Right && _isRightbm)
                speed = 0f;
            else if (point_Left && _isLeftbm)
                speed = 0f;
            else if (point_Up2 && _isUpbm)
                speed = 0f;
            else if (point_Down2 && _isDownbm)
                speed = 0f;
            else if (point_Right2 && _isRightbm)
                speed = 0f;
            else if (point_Left2 && _isLeftbm)
                speed = 0f;
            else
                speed = newspeed;
            PlayerPrefs.SetFloat("speed", speed);
        }

        public void setOrderLayer()
        {
            var num = transform.position.y;
            if (num <= 2.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 97;
            else if (num <= 3.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 96;
            else if (num <= 4.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 95;
            else if (num <= 5.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 94;
            else if (num <= 6.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 93;
            else if (num <= 7.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 92;
            else if (num <= 8.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 91;
            else if (num <= 9.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
            else if (num <= 10.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
            else if (num <= 11.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 88;
            else if (num <= 12.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 87;
            else if (num <= 13.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 86;
            else if (num <= 14.7f)
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = 85;
            else if (num <= 15.7f) gameObject.GetComponent<SpriteRenderer>().sortingOrder = 84;
        }

        public IEnumerator Example()
        {
            yield return new WaitForSeconds(1.5f);
            cir2dbm.enabled = true;
        }


        public IEnumerator Delay()
        {
            yield return new WaitForSeconds(1f);
            cir2dbm.enabled = true;
        }

        public IEnumerator NhapNhay()
        {
            yield return new WaitForSeconds(1.5f);
            _checkzomclbm = false;
        }
    }
}