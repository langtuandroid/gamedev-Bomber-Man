using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay
{
    public class MapManagerbm : Singleton<MapManagerbm>
    {
        public static int columns = 25;

        public static int rows = 15;

        public static GameObject[,] mapObject = new GameObject[columns, rows];

        public static GameObject[,] mapObjectWall = new GameObject[columns, rows];

        public static GameObject[,] mapObjectItems = new GameObject[columns, rows];

        public Count wallCount = new(8, 12);

        public Count boxCount = new(5, 10);

        public GameObject[] floorTiles;

        public GameObject[] wallTiles;

        public GameObject[] outerTiles;

        public GameObject[] outerBotTiles;

        public GameObject[] boxTiles;

        public GameObject[] zombieTiles;

        public GameObject[] itemsTiles;

        public int zombieCount;

        public List<Vector3> wallPositions = new();

        public List<Vector3> itemPosition = new();

        private readonly List<Vector3> gridPosition = new();

        private Loaderbm map;

        private GameObject obj;

        public new void Awake()
        {
            mapObject = new GameObject[columns, rows];
            mapObjectWall = new GameObject[columns, rows];
            mapObjectItems = new GameObject[columns, rows];
        }

        private void Start()
        {
            print(obj.gameObject.name);
            map = Singleton<Loaderbm>.Instance;
        }

        private void InitialiseList()
        {
            gridPosition.Clear();
            for (var i = 3; i < columns; i++)
            for (var j = 3; j < rows; j++)
                gridPosition.Add(new Vector3(i, j + 0.12f, 0f));
        }

        private void MapSetup()
        {
            obj = GameObject.Find("Level");
            for (var i = 0; i < columns + 3; i++)
            for (var j = 1; j < rows + 2; j++)
            {
                var gameObject = floorTiles[Random.Range(0, 0)];
                if ((i + j) % 2 == 0) gameObject = floorTiles[Random.Range(1, 1)];
                if (i == 1 || i == columns + 1 || j == rows + 1 || i == 0 || i == columns + 2 || j == 1)
                    gameObject = outerTiles[Random.Range(0, outerTiles.Length)];
                if (j == rows + 1)
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10 - j;
                else
                    gameObject.GetComponent<SpriteRenderer>().sortingOrder = 200 - j;
                var gameObject2 = Instantiate(gameObject, new Vector3(i, j, 0f), Quaternion.identity);
                gameObject2.transform.SetParent(obj.gameObject.transform);
            }
        }

        private Vector3 RandomPostion()
        {
            var index = Random.Range(0, gridPosition.Count);
            var result = gridPosition[index];
            gridPosition.RemoveAt(index);
            return result;
        }

        private void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
        {
            obj = GameObject.Find("Level");
            var num = Random.Range(minimum, maximum + 1);
            
            //zombieCount = num;
            for (var i = 0; i < num; i++)
            {
                var position = RandomPostion();
                var gameObject = tileArray[Random.Range(0, tileArray.Length)];
                gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(100f - position.y);
                var gameObject2 = Instantiate(gameObject, position, Quaternion.identity);
                
                gameObject2.transform.SetParent(obj.transform);
                if (gameObject.gameObject.CompareTag("Zombie"))
                {
                    //Debug.Log((int)position.x + " " + (int)position.y);
                }
                if (gameObject.gameObject.name == "Wall")
                    wallPositions.Add(new Vector3((int)position.x, (int)position.y, 0f));
                if (gameObject.gameObject.CompareTag("Wall"))
                    mapObjectWall[(int)position.x, (int)position.y] = gameObject;
                else
                    mapObject[(int)position.x, (int)position.y] = gameObject;
            }
        }

        public void SetupScene(int level)
        {
            MapSetup();
            InitialiseList();
            LayoutObjectAtRandom(wallTiles, wallCount.minimum, wallCount.maximum);
            LayoutObjectAtRandom(boxTiles, boxCount.minimum, boxCount.maximum);
            if (level < 15)
            {
                float f = level / 2 + 5;
                zombieCount = (int)Mathf.Floor(f);
                LayoutObjectAtRandom(zombieTiles, zombieCount, zombieCount);
            }
            else if (level < 30)
            {
                float f2 = level / 2 + 4;
                zombieCount = (int)Mathf.Floor(f2);
                LayoutObjectAtRandom(zombieTiles, zombieCount, zombieCount);
            }
            else if (level < 45)
            {
                float f3 = level / 2 + 2;
                zombieCount = (int)Mathf.Floor(f3);
                LayoutObjectAtRandom(zombieTiles, zombieCount, zombieCount);
            }
            else if (level < 61)
            {
                float f4 = level / 2 + 1;
                zombieCount = (int)Mathf.Floor(f4);
                LayoutObjectAtRandom(zombieTiles, zombieCount, zombieCount);
            }
        }

        [Serializable]
        public class Count
        {
            public int minimum;

            public int maximum;

            public Count(int min, int max)
            {
                minimum = min;
                maximum = max;
            }
        }
    }
}