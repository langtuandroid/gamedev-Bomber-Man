using Managers;
using UnityEngine;

namespace GamePlay
{
    public class Loaderbm : Singleton<Loaderbm>
    {
        public GameObject gameManager;

        public GameObject mapholder;

        public float width;

        public float height;

        public AudioSource audioStartLevel;

        private new void Awake()
        {
            if (GameManager.instance == null) Instantiate(gameManager);
        }

        // private void Start()
        // {
        //     var num = 1.7777778f;
        //     var num2 = Screen.width / (float)Screen.height;
        //     var num3 = num2 / num;
        //     var component = GetComponent<Camera>();
        //     if (num3 < 1f)
        //     {
        //         var rect = component.rect;
        //         rect.width = 1f;
        //         rect.height = num3;
        //         rect.x = 0f;
        //         rect.y = (1f - num3) / 2f;
        //         component.rect = rect;
        //     }
        //     else
        //     {
        //         var num4 = 1f / num3;
        //         var rect2 = component.rect;
        //         rect2.width = num4;
        //         rect2.height = 1f;
        //         rect2.x = (1f - num4) / 2f;
        //         rect2.y = 0f;
        //         component.rect = rect2;
        //     }
        // }
    }
}