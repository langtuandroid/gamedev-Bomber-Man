using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class BoomSpawnerbm : Singleton<BoomSpawnerbm>
    {
        public GameObject bomb;

        public int firePower = 1;

        public int numberOfBombs = 1;

        public float fuse = 3f;

        public GameObject audioSoundF1;

        public bool check;

        public bool check1;

        private AudioClip _audioClip;

        private AudioSource _audioSource1;

        private GameControllerbm _gameControllerbm;

        private void Start()
        {
            check = true;
            check1 = true;
            audioSoundF1 = GameObject.Find("SoundClickBoom");
            _audioSource1 = audioSoundF1.GetComponent<AudioSource>();
            _audioSource1.Stop();
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Bomb") check = true;
        }

        public void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Bomb")
            {
                check = false;
                StartCoroutine(timeDestroybm());
            }
        }

        public void clickBomb()
        {
            if (check1)
            {
                if (numberOfBombs >= 1 && check)
                {
                    var vector = new Vector2(Mathf.Round(transform.position.x),
                        Mathf.Round(transform.position.y - 0.3f));
                    var gameObject = Instantiate(bomb, vector, Quaternion.identity);
                    gameObject.GetComponent<Bombbm>().firePower = firePower;
                    gameObject.GetComponent<Bombbm>().fuse = fuse;
                    numberOfBombs--;
                    _audioSource1.Play();
                }

                StartCoroutine(timeClickBombbm());
            }
        }

        private IEnumerator timeDestroybm()
        {
            yield return new WaitForSeconds(3f);
            check = true;
        }

        private IEnumerator timeClickBombbm()
        {
            yield return new WaitForSeconds(0f);
            check1 = false;
            yield return new WaitForSeconds(0.5f);
            check1 = true;
        }
    }
}