using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XX
{
	public class LevelManager : MonoBehaviour
	{
		public Button[] allButton;

		public MusicChange mScriptChangeMusic;

		public SoundChange mScriptChangeSound;

		private void Start()
		{
			PlayerPrefs.SetInt("dem", 0);
			PlayerPrefs.Save();
			OpenClock();
		}

		private void OpenClock()
		{
			List<LevelComplete> allLevelComplete = FileManager.GetAllLevelComplete();
			for (int i = 0; i < allLevelComplete.Count; i++)
			{
				LevelComplete levelComplete = allLevelComplete[i];
				if (levelComplete.mCompleted)
				{
					Image[] componentsInChildren = allButton[i].GetComponentsInChildren<Image>();
					var levelNumber = allButton[i].GetComponentInChildren<Text>();
					levelNumber.text = levelComplete.mId.ToString();
					componentsInChildren[0].enabled = true;
					componentsInChildren[1].enabled = false;
				}
			}
		}

		public void OnLevelCLick(int level)
		{
			List<LevelComplete> allLevelComplete = FileManager.GetAllLevelComplete();
			LevelComplete levelComplete = allLevelComplete[level - 1];
			if (levelComplete.mCompleted)
			{
				PlayerPrefs.SetInt(FileManager.KEY_CURRENT_LEVEL, level);
				PlayerPrefs.Save();
				int @int = PlayerPrefs.GetInt("dem");
				if (@int == 0)
				{
					Fader.Instance.FadeIn().Pause().LoadLevel("GamePlay")
						.FadeOut(0.5f);
					@int++;
					PlayerPrefs.SetInt("dem", @int);
					PlayerPrefs.Save();
				}
			}
		}

		public void backMenu()
		{
			mScriptChangeSound.PlayAudio();
			int @int = PlayerPrefs.GetInt("dem");
			if (@int == 0)
			{
				Fader.Instance.FadeIn().Pause().LoadLevel("Menu")
					.FadeOut(0.5f);
				@int++;
				PlayerPrefs.SetInt("dem", @int);
				PlayerPrefs.Save();
			}
		}

		public void clickBtn()
		{
			mScriptChangeSound.PlayAudio();
		}
	}
}
