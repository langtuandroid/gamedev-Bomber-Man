using UnityEngine;
using XX;

namespace GamePlay
{
    public class LevelClick : MonoBehaviour
    {
        public void OnLevelCLick(int level)
        {
            var allLevelComplete = FileManager.GetAllLevelComplete();
            var levelComplete = allLevelComplete[level - 1];
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