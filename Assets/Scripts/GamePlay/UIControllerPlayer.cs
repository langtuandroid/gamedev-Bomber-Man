using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace XX
{
	public class UIControllerPlayer : MonoBehaviour
	{
		public GameObject PanelPause;

		public GameObject PanelGameOver;

		public GameObject PanelNextLevel;

		public GameObject btn_Bomb;

		public GameObject btn_Pause;

		public Text txtFire;

		public Text txtLife;

		public Text txtBoot;

		public Text txtBomb;

		public Text txtArmor;

		public Text txtLevel;

		public static UIControllerPlayer UI;

		public GameObject player;

		private GameObject gameManager;

		public EasyJoystick easy;

		public static bool checkActiveBom;

		private Soldier soldier;

		private MapManager map;

		private GameObject xx;

		public Button[] allButton;

		public MusicChange mScriptChangeMusic;

		public SoundChange mScriptChangeSound;

		private BoomSpawner boomSpawner;

		private Move move;

		private float x;

		private void Awake()
		{
			UI = this;
		}

		private void Start()
		{
			PlayerPrefs.SetInt("dem", 0);
			PlayerPrefs.Save();
			map = GameObject.Find("GameManager(Clone)").GetComponent<MapManager>();
			xx = GameObject.Find("GameManager(Clone)");
			if (Time.timeScale == 0f)
			{
				Time.timeScale = 1f;
			}
		}

		private void Update()
		{
			MovePlayer();
			if (Singleton<Soldier>.Instance.heart >= 0)
			{
				txtLife.text = "x" + Singleton<Soldier>.Instance.heart;
			}
			else
			{
				txtLife.text = "0";
			}
			if (Singleton<Soldier>.Instance.armor)
			{
				txtArmor.text = "x1";
			}
			else
			{
				txtArmor.text = "x0";
			}
			txtBomb.text = "x" + Singleton<BoomSpawner>.Instance.numberOfBombs;
			if (Singleton<BoomSpawner>.Instance.firePower <= 4)
			{
				txtFire.text = "x" + (Singleton<BoomSpawner>.Instance.firePower - 1);
			}
			else
			{
				txtFire.text = "3";
			}
			txtBoot.text = "x" + Singleton<Soldier>.Instance.speedValue;
			txtLevel.text = "Level: " + GameManager.level;
			nextLevel();
			gameOver();
		}

		public void gameOver()
		{
			if (Singleton<Soldier>.Instance.heart < 1)
			{
				try
				{
					MyAdvertisement.ShowFullNormal();
				}
				catch
				{
				}
				PanelGameOver.SetActive(true);
				btn_Bomb.SetActive(false);
				btn_Pause.SetActive(false);
				easy.GetComponent<EasyJoystick>().visible = false;
				Singleton<Soldier>.Instance.heart = 1;
			}
		}

		public void nextLevel()
		{
			if (map.zombieCount == 0)
			{
				try
				{
					MyAdvertisement.ShowFullNormal();
				}
				catch
				{
				}
				PanelNextLevel.SetActive(true);
				btn_Bomb.SetActive(false);
				btn_Pause.SetActive(false);
				easy.GetComponent<EasyJoystick>().visible = false;
				map.zombieCount = 1;
			}
		}

		public void loadMenu()
		{
			Object.Destroy(xx.gameObject);
			Time.timeScale = 1f;
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

		public void pauseClickDisnable()
		{
			mScriptChangeSound.PlayAudio();
			PanelPause.SetActive(false);
			Time.timeScale = 1f;
			easy.GetComponent<EasyJoystick>().visible = true;
		}

		public void pauseClickEnable()
		{
			mScriptChangeSound.PlayAudio();
			PanelPause.SetActive(true);
			Time.timeScale = 0f;
			easy.GetComponent<EasyJoystick>().visible = false;
		}

		public void MovePlayer()
		{
			player.GetComponent<Move>().MoveControl(easy.JoystickAxis.x, easy.JoystickAxis.y);
		}

		public void SetActiveBom(bool check)
		{
			checkActiveBom = check;
		}

		public void OnLevelClick()
		{
			List<LevelComplete> allLevelComplete = FileManager.GetAllLevelComplete();
			LevelComplete levelComplete = allLevelComplete[PlayerPrefs.GetInt(FileManager.KEY_CURRENT_LEVEL) - 1];
			int mId = levelComplete.mId;
			FileManager.UpdateLevel(PlayerPrefs.GetInt(FileManager.KEY_CURRENT_LEVEL) + 1);
			if (allLevelComplete[mId - 1].mCompleted)
			{
				PlayerPrefs.SetInt(FileManager.KEY_CURRENT_LEVEL, mId + 1);
				PlayerPrefs.Save();
				Destroy(xx.gameObject);
				Application.LoadLevel("GamePlay");
			}
		}

		public void ReTry()
		{
			mScriptChangeSound.PlayAudio();
			Destroy(xx.gameObject);
			Application.LoadLevel("GamePlay");
		}
	}
}
