using UnityEngine;

namespace GamePlay
{
    public class GameControllerbm : MonoBehaviour
    {
        private const int X = 30;
        private const int Y = 20;
        
        public GameObject levelHolder;

        public GameObject[,] level = new GameObject[X, Y];

        private void Start()
        {
            var componentsInChildren = levelHolder.GetComponentsInChildren<Transform>();
            var array = componentsInChildren;
            foreach (var transform in array)
                if (transform.gameObject.tag != "Floor")
                    level[(int)transform.transform.position.x, (int)transform.transform.position.y] =
                        transform.gameObject;
            level[0, 0] = null;
        }
    }
}