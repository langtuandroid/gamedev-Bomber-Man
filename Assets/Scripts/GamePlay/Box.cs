using UnityEngine;

namespace GamePlay
{
    public class Box : MonoBehaviour
    {
        public GameObject[] items;

        private void Update()
        {
            setOrderLayer();
        }

        public void SpawnPowerUp()
        {
            if (Random.Range(0f, 1f) > 0.75f)
            {
                var num = Random.Range(0, items.Length);
                Instantiate(items[num], new Vector3(transform.position.x, transform.position.y - 0.12f, 0f),
                    Quaternion.identity);
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