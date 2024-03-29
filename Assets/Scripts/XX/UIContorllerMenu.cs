using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace XX
{
	public class UIContorllerMenu : MonoBehaviour
	{
		public GameObject panelSetting;

		public GameObject panelExit;

		public GameObject eff;

		public Sprite soundOnSprite;

		public Sprite soundOffSprite;

		public Sprite musicOnSprite;

		public Sprite musicOffSprite;

		public Button soundButton;

		public Button musicButton;

		public MusicChange mScriptChangeMusic;

		public SoundChange mScriptChangeSound;

		private bool isShowGameSetting;

		public GameObject bgExit;

		private void Start()
		{
			PlayerPrefs.SetInt("dem", 0);
			PlayerPrefs.Save();
			Setup1();
			Setup2();
		}

		public void LoadPlay()
		{
			int @int = PlayerPrefs.GetInt("dem");
			if (@int == 0)
			{
				Fader.Instance.FadeIn().Pause().LoadLevel("SelectLevel")
					.FadeOut(0.5f);
				@int++;
				PlayerPrefs.SetInt("dem", @int);
				PlayerPrefs.Save();
			}
		}

		public void OpenSetting()
		{
			eff.SetActive(false);
			panelSetting.SetActive(true);
			mScriptChangeSound.PlayAudio();
		}

		public void CloseSetting()
		{
			eff.SetActive(true);
			panelSetting.SetActive(false);
			mScriptChangeSound.PlayAudio();
		}

		public void OpenQuit()
		{
			panelExit.SetActive(true);
			eff.SetActive(false);
			mScriptChangeSound.PlayAudio();
		}

		public void clickQuit(bool check)
		{
			mScriptChangeSound.PlayAudio();
			if (check)
			{
				bgExit.SetActive(true);
				try
				{
					MyAdvertisement.ShowFullNormal();
				}
				catch
				{
				}
				StartCoroutine(delay());
			}
			else
			{
				panelExit.SetActive(false);
				eff.SetActive(true);
			}
		}

		private IEnumerator delay()
		{
			yield return new WaitForSeconds(1.5f);
			Application.Quit();
		}

		public void backMenu()
		{
			mScriptChangeSound.PlayAudio();
			Application.LoadLevel("Menu");
		}

		public void SoundClick()
		{
			mScriptChangeSound.PlayAudio();
			int @int = PlayerPrefs.GetInt(Constains.KEY_SOUND, 1);
			if (@int == 1)
			{
				PlayerPrefs.SetInt(Constains.KEY_SOUND, 0);
				PlayerPrefs.Save();
				soundButton.GetComponent<Image>().sprite = soundOffSprite;
			}
			else
			{
				PlayerPrefs.SetInt(Constains.KEY_SOUND, 1);
				PlayerPrefs.Save();
				soundButton.GetComponent<Image>().sprite = soundOnSprite;
			}
		}

		public void MusicClick()
		{
			mScriptChangeSound.PlayAudio();
			int @int = PlayerPrefs.GetInt(Constains.KEY_MUSIC, 1);
			if (@int == 1)
			{
				PlayerPrefs.SetInt(Constains.KEY_MUSIC, 0);
				PlayerPrefs.Save();
				musicButton.GetComponent<Image>().sprite = musicOffSprite;
				mScriptChangeMusic.StopAudio();
			}
			else
			{
				PlayerPrefs.SetInt(Constains.KEY_MUSIC, 1);
				PlayerPrefs.Save();
				musicButton.GetComponent<Image>().sprite = musicOnSprite;
				mScriptChangeMusic.PlayAudio();
			}
		}

		private void Setup1()
		{
			int @int = PlayerPrefs.GetInt(Constains.KEY_SOUND, 1);
			if (@int == 1)
			{
				soundButton.GetComponent<Image>().sprite = soundOnSprite;
			}
			else
			{
				soundButton.GetComponent<Image>().sprite = soundOffSprite;
			}
		}

		public void Setup2()
		{
			int @int = PlayerPrefs.GetInt(Constains.KEY_MUSIC, 1);
			if (@int == 1)
			{
				musicButton.GetComponent<Image>().sprite = musicOnSprite;
			}
			else
			{
				musicButton.GetComponent<Image>().sprite = musicOffSprite;
			}
		}
	}
}
