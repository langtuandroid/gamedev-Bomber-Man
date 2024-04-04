using UnityEngine;

namespace GamePlay
{
    public class Bombbm : MonoBehaviour
    {
        public GameObject fire;

        public int firePower;

        public int firePowerMax;

        public float fuse;

        public GameObject audioSoundF1;

        private AudioClip _auclip1bm;

        private GameControllerbm _gameControllerbm;

        private AudioSource _src1bm;

        private readonly Vector3[] directions = new Vector3[4]
        {
            Vector3.up,
            Vector3.down,
            Vector3.left,
            Vector3.right
        };

        private void Awake()
        {
            audioSoundF1 = GameObject.Find("SoundBoom");
            _src1bm = audioSoundF1.GetComponent<AudioSource>();
            _src1bm.Stop();
        }

        private void Start()
        {
            firePowerMax = 4;
            Invoke("Explode", fuse);
            _gameControllerbm = GameObject.Find("GameController").GetComponent<GameControllerbm>();
        }

        private void Update()
        {
            setOrderLayer();
        }

        public void Explode()
        {
            CancelInvoke("Explode");
            Instantiate(fire, transform.position, Quaternion.identity);
            var array = directions;
            foreach (var offset in array) SpawnFirebm(offset);
            _src1bm.Play();
            Singleton<BoomSpawnerbm>.Instance.check = true;
            Destroy(gameObject);
            Singleton<BoomSpawnerbm>.Instance.numberOfBombs++;
        }

        private void SpawnFirebm(Vector3 offset, int fire = 1)
        {
            if (firePower <= firePowerMax)
            {
                var value = (int)transform.position.x + (int)offset.x * fire;
                var value2 = (int)transform.position.y + (int)offset.y * fire;
                value = Mathf.Clamp(value, 0, 29);
                value2 = Mathf.Clamp(value2, 0, 19);
                if (fire < firePower && _gameControllerbm.level[value, value2] == null)
                {
                    Instantiate(this.fire, transform.position + offset * fire, Quaternion.identity);
                    SpawnFirebm(offset, ++fire);
                }
                else if (fire < firePower && _gameControllerbm.level[value, value2] != null &&
                         ((_gameControllerbm.level[value, value2] != null &&
                           _gameControllerbm.level[value, value2].tag == "Box") ||
                          (_gameControllerbm.level[value, value2] != null &&
                           _gameControllerbm.level[value, value2].tag == "Items") ||
                          (_gameControllerbm.level[value, value2] != null &&
                           _gameControllerbm.level[value, value2].tag == "Zombie") ||
                          (_gameControllerbm.level[value, value2] != null &&
                           _gameControllerbm.level[value, value2].tag == "ItemsBoot") ||
                          (_gameControllerbm.level[value, value2] != null &&
                           _gameControllerbm.level[value, value2].tag == "Untagged")))
                {
                    Instantiate(this.fire, transform.position + offset * fire, Quaternion.identity);
                }
            }
            else if (firePower > firePowerMax)
            {
                var value3 = (int)transform.position.x + (int)offset.x * fire;
                var value4 = (int)transform.position.y + (int)offset.y * fire;
                value3 = Mathf.Clamp(value3, 0, 29);
                value4 = Mathf.Clamp(value4, 0, 19);
                if (fire < firePowerMax && _gameControllerbm.level[value3, value4] == null)
                {
                    Instantiate(this.fire, transform.position + offset * fire, Quaternion.identity);
                    SpawnFirebm(offset, ++fire);
                }
                else if (fire < firePowerMax && _gameControllerbm.level[value3, value4] != null &&
                         ((_gameControllerbm.level[value3, value4] != null &&
                           _gameControllerbm.level[value3, value4].tag == "Box") ||
                          (_gameControllerbm.level[value3, value4] != null &&
                           _gameControllerbm.level[value3, value4].tag == "Items") ||
                          (_gameControllerbm.level[value3, value4] != null &&
                           _gameControllerbm.level[value3, value4].tag == "Zombie") ||
                          (_gameControllerbm.level[value3, value4] != null &&
                           _gameControllerbm.level[value3, value4].tag == "ItemsBoot") ||
                          (_gameControllerbm.level[value3, value4] != null &&
                           _gameControllerbm.level[value3, value4].tag == "Untagged")))
                {
                    Instantiate(this.fire, transform.position + offset * fire, Quaternion.identity);
                }
            }
        }

        public void setOrderLayer()
        {
            switch ((int)transform.position.y)
            {
                case 2:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 96;
                    break;
                case 3:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 95;
                    break;
                case 4:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 94;
                    break;
                case 5:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 93;
                    break;
                case 6:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 92;
                    break;
                case 7:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 91;
                    break;
                case 8:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 90;
                    break;
                case 9:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 89;
                    break;
                case 10:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 88;
                    break;
                case 11:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 87;
                    break;
                case 12:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 86;
                    break;
                case 13:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 85;
                    break;
                case 14:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 84;
                    break;
                case 15:
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 83;
                    break;
            }
        }
    }
}