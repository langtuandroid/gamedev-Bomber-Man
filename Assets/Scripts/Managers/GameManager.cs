using GamePlay;
using UnityEngine;
using XX;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        public static GameManager instance;

        public static int level;

        [HideInInspector] public bool playerTurn = true;

        private MapManagerbm mapScript;

        private new void Awake()
        {
            var allLevelComplete = FileManager.GetAllLevelComplete();
            var levelComplete = allLevelComplete[PlayerPrefs.GetInt(FileManager.KEY_CURRENT_LEVEL) - 1];
            level = levelComplete.mId;
            Time.timeScale = 1f;
            if (instance == null)
                instance = this;
            else if (instance != this) Destroy(gameObject);
            DontDestroyOnLoad(gameObject);
            mapScript = GetComponent<MapManagerbm>();
            InitGame();
        }

        public void InitGame()
        {
            mapScript.SetupScene(level);
        }
    }
}