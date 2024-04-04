using System.Collections;
using UnityEngine;

namespace GamePlay
{
    public class Soldierbm : Singleton<Soldierbm>
    {
        public int heart;

        public bool armor;

        public GameObject obj;

        public float speedValue;

        private void Start()
        {
            heart = 3;
            armor = false;
            speedValue = 1f;
        }

        private void Update()
        {
            if (heart <= 0) Debug.Log("Lose!");
            if (armor)
                obj.SetActive(true);
            else
                obj.SetActive(false);
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "ItemsArmor")
            {
                armor = true;
                StartCoroutine(timeArmor());
            }

            if (collision.gameObject.tag == "ItemsBoot") speedValue += 1f;
        }

        public IEnumerator timeArmor()
        {
            yield return new WaitForSeconds(20f);
            armor = false;
        }
    }
}