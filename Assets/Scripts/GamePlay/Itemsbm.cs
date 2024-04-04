using UnityEngine;

namespace GamePlay
{
    public class Itemsbm : MonoBehaviour
    {
        public int bombs;

        public int firePower;

        public float speed;

        public int heart;

        public GameObject eff;

        public GameObject audioSoundF;

        private AudioClip _audioClip;

        private AudioSource _audioSource;

        private GameControllerbm _gameControllerbm;

        private void Awake()
        {
            audioSoundF = GameObject.Find("SoundEatItem");
            _audioSource = audioSoundF.GetComponent<AudioSource>();
            _audioSource.Stop();
        }

        public void Start()
        {
            _gameControllerbm = GameObject.Find("GameController").GetComponent<GameControllerbm>();
            _gameControllerbm.level[(int)transform.position.x, (int)transform.position.y] = gameObject;
        }

        private void Update()
        {
            setOrderLayer();
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                eff.SetActive(true);
                var gameObject = Instantiate(eff, this.gameObject.transform.position, Quaternion.identity);
                _audioSource.Play();
                var component = collision.gameObject.GetComponent<Movebm>();
                var component2 = collision.gameObject.GetComponent<BoomSpawnerbm>();
                var component3 = collision.gameObject.GetComponent<Soldierbm>();
                component3.heart += heart;
                component.newspeed += speed;
                component2.numberOfBombs += bombs;
                component2.firePower += firePower;
                Debug.Log("+Speed!!!" + collision.gameObject.GetComponent<Movebm>().newspeed);
                Destroy(this.gameObject);
            }
        }

        public void setOrderLayer()
        {
            switch ((int)transform.position.y)
            {
                case 2:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 97;
                    break;
                case 3:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 96;
                    break;
                case 4:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 95;
                    break;
                case 5:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 94;
                    break;
                case 6:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 93;
                    break;
                case 7:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 92;
                    break;
                case 8:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 91;
                    break;
                case 9:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
                    break;
                case 10:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
                    break;
                case 11:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 88;
                    break;
                case 12:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 87;
                    break;
                case 13:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 86;
                    break;
                case 14:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 85;
                    break;
                case 15:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 84;
                    break;
            }
        }
    }
}