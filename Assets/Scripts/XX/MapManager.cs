using System;
using System.Collections.Generic;
using UnityEngine;

namespace XX
{
	public class MapManager : Singleton<MapManager>
	{
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

		public static int columns = 25;

		public static int rows = 15;

		public Count wallCount = new Count(8, 12);

		public Count boxCount = new Count(5, 10);

		public GameObject[] floorTiles;

		public GameObject[] wallTiles;

		public GameObject[] outerTiles;

		public GameObject[] outerBotTiles;

		public GameObject[] boxTiles;

		public GameObject[] zombieTiles;

		public GameObject[] itemsTiles;

		private GameObject obj;

		public int zombieCount;

		public static GameObject[,] mapObject = new GameObject[columns, rows];

		public static GameObject[,] mapObjectWall = new GameObject[columns, rows];

		public static GameObject[,] mapObjectItems = new GameObject[columns, rows];

		private Loader map;

		private List<Vector3> gridPosition = new List<Vector3>();

		public List<Vector3> wallPositions = new List<Vector3>();

		public List<Vector3> itemPosition = new List<Vector3>();

		public new void Awake()
		{
			mapObject = new GameObject[columns, rows];
			mapObjectWall = new GameObject[columns, rows];
			mapObjectItems = new GameObject[columns, rows];
		}

		private void InitialiseList()
		{
			gridPosition.Clear();
			for (int i = 3; i < columns; i++)
			{
				for (int j = 3; j < rows; j++)
				{
					gridPosition.Add(new Vector3(i, (float)j + 0.12f, 0f));
				}
			}
		}

		private void Start()
		{
			MonoBehaviour.print(obj.gameObject.name);
			map = Singleton<Loader>.Instance;
		}

		private void MapSetup()
		{
			obj = GameObject.Find("Level");
			for (int i = 0; i < columns + 3; i++)
			{
				for (int j = 1; j < rows + 2; j++)
				{
					GameObject gameObject = floorTiles[UnityEngine.Random.Range(0, 0)];
					if ((i + j) % 2 == 0)
					{
						gameObject = floorTiles[UnityEngine.Random.Range(1, 1)];
					}
					if (i == 1 || i == columns + 1 || j == rows + 1 || i == 0 || i == columns + 2 || j == 1)
					{
						gameObject = outerTiles[UnityEngine.Random.Range(0, outerTiles.Length)];
					}
					if (j == rows + 1)
					{
						gameObject.GetComponent<SpriteRenderer>().sortingOrder = 10 - j;
					}
					else
					{
						gameObject.GetComponent<SpriteRenderer>().sortingOrder = 200 - j;
					}
					GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, new Vector3(i, j, 0f), Quaternion.identity);
					gameObject2.transform.SetParent(obj.gameObject.transform);
				}
			}
		}

		private Vector3 RandomPostion()
		{
			int index = UnityEngine.Random.Range(0, gridPosition.Count);
			Vector3 result = gridPosition[index];
			gridPosition.RemoveAt(index);
			return result;
		}

		private void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximum)
		{
			obj = GameObject.Find("Level");
			int num = UnityEngine.Random.Range(minimum, maximum + 1);
			for (int i = 0; i < num; i++)
			{
				Vector3 position = RandomPostion();
				GameObject gameObject = tileArray[UnityEngine.Random.Range(0, tileArray.Length)];
				gameObject.GetComponent<SpriteRenderer>().sortingOrder = (int)(100f - position.y);
				GameObject gameObject2 = UnityEngine.Object.Instantiate(gameObject, position, Quaternion.identity);
				gameObject2.transform.SetParent(obj.transform);
				if (gameObject.gameObject.CompareTag("Zombie"))
				{
					Debug.Log((int)position.x + " " + (int)position.y);
				}
				if (gameObject.gameObject.name == "Wall")
				{
					wallPositions.Add(new Vector3((int)position.x, (int)position.y, 0f));
				}
				if (gameObject.gameObject.CompareTag("Wall"))
				{
					mapObjectWall[(int)position.x, (int)position.y] = gameObject;
				}
				else
				{
					mapObject[(int)position.x, (int)position.y] = gameObject;
				}
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
			else if (level < 45)
			{
				float f4 = level / 2 + 1;
				zombieCount = (int)Mathf.Floor(f4);
				LayoutObjectAtRandom(zombieTiles, zombieCount, zombieCount);
			}
		}
	}
}
