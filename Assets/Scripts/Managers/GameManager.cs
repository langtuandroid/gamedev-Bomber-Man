using System.Collections.Generic;
using UnityEngine;

namespace XX
{
	public class GameManager : Singleton<GameManager>
	{
		public static GameManager instance;

		[HideInInspector]
		public bool playerTurn = true;

		private MapManager mapScript;

		public static int level;

		private new void Awake()
		{
			List<LevelComplete> allLevelComplete = FileManager.GetAllLevelComplete();
			LevelComplete levelComplete = allLevelComplete[PlayerPrefs.GetInt(FileManager.KEY_CURRENT_LEVEL) - 1];
			level = levelComplete.mId;
			Time.timeScale = 1f;
			if (instance == null)
			{
				instance = this;
			}
			else if (instance != this)
			{
				Object.Destroy(base.gameObject);
			}
			Object.DontDestroyOnLoad(base.gameObject);
			mapScript = GetComponent<MapManager>();
			InitGame();
		}

		private void Start()
		{
		}

		public void InitGame()
		{
			mapScript.SetupScene(level);
		}

		public void GameOver()
		{
			base.enabled = false;
		}
	}
}
