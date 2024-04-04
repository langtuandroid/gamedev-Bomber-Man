using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using XX;

namespace GamePlay
{
    public class UIControllerPlayerbm : MonoBehaviour
    {
        public static UIControllerPlayerbm UI;

        public static bool checkActiveBom;
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

        public GameObject player;

        public EasyJoystick easy;

        public Button[] allButton;

        [FormerlySerializedAs("mScriptChangeMusic")]
        public MusicChangebm mScriptChangebmMusic;

        [FormerlySerializedAs("mScriptChangeSound")]
        public SoundChangebm mScriptChangebmSound;

        private Movebm _movebm;

        private Soldierbm _soldierbm;

        private BoomSpawnerbm _boomSpawnerbm;

        private GameObject gameManager;

        private MapManagerbm map;

        private float x;

        private GameObject xx;

        private void Awake()
        {
            UI = this;
        }

        private void Start()
        {
            PlayerPrefs.SetInt("dem", 0);
            PlayerPrefs.Save();
            map = GameObject.Find("GameManager(Clone)").GetComponent<MapManagerbm>();
            xx = GameObject.Find("GameManager(Clone)");
            if (Time.timeScale == 0f) Time.timeScale = 1f;
        }

        private void Update()
        {
            MovePlayer();
            if (Singleton<Soldierbm>.Instance.heart >= 0)
                txtLife.text = "x" + Singleton<Soldierbm>.Instance.heart;
            else
                txtLife.text = "0";
            if (Singleton<Soldierbm>.Instance.armor)
                txtArmor.text = "x1";
            else
                txtArmor.text = "x0";
            txtBomb.text = "x" + Singleton<BoomSpawnerbm>.Instance.numberOfBombs;
            if (Singleton<BoomSpawnerbm>.Instance.firePower <= 4)
                txtFire.text = "x" + (Singleton<BoomSpawnerbm>.Instance.firePower - 1);
            else
                txtFire.text = "3";
            txtBoot.text = "x" + Singleton<Soldierbm>.Instance.speedValue;
            txtLevel.text = "Level: " + GameManager.level;
            nextLevel();
            gameOver();
        }

        public void gameOver()
        {
            if (Singleton<Soldierbm>.Instance.heart < 1)
            {
                Singleton<Soldierbm>.Instance.gameObject.SetActive(false);
                PanelGameOver.SetActive(true);
                btn_Bomb.SetActive(false);
                btn_Pause.SetActive(false);
                easy.GetComponent<EasyJoystick>().visible = false;
                Singleton<Soldierbm>.Instance.heart = 1;
            }
        }

        public void nextLevel()
        {
            if (map.zombieCount == 0)
            {
                PanelNextLevel.SetActive(true);
                btn_Bomb.SetActive(false);
                btn_Pause.SetActive(false);
                easy.GetComponent<EasyJoystick>().visible = false;
                map.zombieCount = 1;
            }
        }

        public void loadMenu()
        {
            Destroy(xx.gameObject);
            Time.timeScale = 1f;
            var @int = PlayerPrefs.GetInt("dem");
            if (@int == 0)
            {
                Faderbm.Instance.FadeIn().Pause().LoadLevel("Menu")
                    .FadeOut(0.5f);
                @int++;
                PlayerPrefs.SetInt("dem", @int);
                PlayerPrefs.Save();
            }
        }

        public void pauseClickDisnable()
        {
            mScriptChangebmSound.PlayAudio();
            PanelPause.SetActive(false);
            Time.timeScale = 1f;
            easy.GetComponent<EasyJoystick>().visible = true;
        }

        public void pauseClickEnable()
        {
            mScriptChangebmSound.PlayAudio();
            PanelPause.SetActive(true);
            Time.timeScale = 0f;
            easy.GetComponent<EasyJoystick>().visible = false;
        }

        public void MovePlayer()
        {
            player.GetComponent<Movebm>().MoveControl(easy.JoystickAxis.x, easy.JoystickAxis.y);
        }

        public void SetActiveBom(bool check)
        {
            checkActiveBom = check;
        }

        public void OnLevelClick()
        {
            var allLevelComplete = FileManager.GetAllLevelComplete();
            var levelComplete = allLevelComplete[PlayerPrefs.GetInt(FileManager.KEY_CURRENT_LEVEL) - 1];
            var mId = levelComplete.mId;
            if (mId < 60)
            {
                FileManager.UpdateLevel(PlayerPrefs.GetInt(FileManager.KEY_CURRENT_LEVEL) + 1);
                if (allLevelComplete[mId - 1].mCompleted)
                {
                    PlayerPrefs.SetInt(FileManager.KEY_CURRENT_LEVEL, mId + 1);
                    PlayerPrefs.Save();
                    Destroy(xx.gameObject);
                    Application.LoadLevel("GamePlay");
                }
            }
            else
            {
                Application.LoadLevel("Menu");
            }
           
        }

        public void ReTry()
        {
            mScriptChangebmSound.PlayAudio();
            Destroy(xx.gameObject);
            Application.LoadLevel("GamePlay");
        }
    }
}