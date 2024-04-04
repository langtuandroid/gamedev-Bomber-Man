using GamePlay;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace XX
{
    public class LevelManagerbm : MonoBehaviour
    {
        public Button[] allButton;

        [FormerlySerializedAs("mScriptChangeMusic")]
        public MusicChangebm mScriptChangebmMusic;

        [FormerlySerializedAs("mScriptChangeSound")]
        public SoundChangebm mScriptChangebmSound;

        private void Start()
        {
            PlayerPrefs.SetInt("dem", 0);
            PlayerPrefs.Save();
            OpenClockbm();
        }

        private void OpenClockbm()
        {
            var allLevelComplete = FileManager.GetAllLevelComplete();
            for (var i = 0; i < allLevelComplete.Count; i++)
            {
                var levelComplete = allLevelComplete[i];
                if (levelComplete.mCompleted)
                {
                    var componentsInChildren = allButton[i].GetComponentsInChildren<Image>();
                    var levelNumber = allButton[i].GetComponentInChildren<Text>();
                    levelNumber.text = levelComplete.mId.ToString();
                    componentsInChildren[0].enabled = true;
                    componentsInChildren[1].enabled = false;
                }
            }
        }

        public void OnLevelCLick(int level)
        {
            var allLevelComplete = FileManager.GetAllLevelComplete();
            var levelComplete = allLevelComplete[level - 1];
            if (levelComplete.mCompleted)
            {
                PlayerPrefs.SetInt(FileManager.KEY_CURRENT_LEVEL, level);
                PlayerPrefs.Save();
                var @int = PlayerPrefs.GetInt("dem");
                if (@int == 0)
                {
                    Faderbm.Instance.FadeIn().Pause().LoadLevel("GamePlay")
                        .FadeOut(0.5f);
                    @int++;
                    PlayerPrefs.SetInt("dem", @int);
                    PlayerPrefs.Save();
                }
            }
        }

        public void backMenu()
        {
            mScriptChangebmSound.PlayAudio();
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

        public void clickBtnbm()
        {
            mScriptChangebmSound.PlayAudio();
        }
    }
}