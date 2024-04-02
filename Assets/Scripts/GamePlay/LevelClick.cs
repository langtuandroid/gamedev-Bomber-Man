using System.Collections.Generic;
using UnityEngine;

namespace XX
{
	public class LevelClick : MonoBehaviour
	{
		public void OnLevelCLick(int level)
		{
			List<LevelComplete> allLevelComplete = FileManager.GetAllLevelComplete();
			LevelComplete levelComplete = allLevelComplete[level - 1];
			if (levelComplete.mCompleted)
			{
				PlayerPrefs.SetInt(FileManager.KEY_CURRENT_LEVEL, level);
				PlayerPrefs.Save();
				Application.LoadLevel("GamePlay");
			}
		}

		public void LoadMenu()
		{
			Application.LoadLevel("Menu");
		}
	}
}
