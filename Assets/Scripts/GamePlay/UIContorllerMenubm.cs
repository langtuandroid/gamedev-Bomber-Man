using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GamePlay
{
    public class UIContorllerMenubm : MonoBehaviour
    {
        public GameObject panelSetting;

        public GameObject panelExit;

        public GameObject eff;

        public Sprite soundOnSprite;

        public Sprite soundOffSprite;

        public Button soundButton;

        public Button musicButton;

        [FormerlySerializedAs("mScriptChangeMusic")]
        public MusicChangebm mScriptChangebmMusic;

        [FormerlySerializedAs("mScriptChangeSound")]
        public SoundChangebm mScriptChangebmSound;

        public GameObject bgExit;

        private bool isShowGameSettingbm;

        private void Start()
        {
            PlayerPrefs.SetInt("dem", 0);
            PlayerPrefs.Save();
            Setup1();
            //Setup2();
        }

        public void LoadPlay()
        {
            var @int = PlayerPrefs.GetInt("dem");
            if (@int == 0)
            {
                Faderbm.Instance.FadeIn().Pause().LoadLevel("SelectLevel")
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
            mScriptChangebmSound.PlayAudio();
        }

        public void CloseSetting()
        {
            eff.SetActive(true);
            panelSetting.SetActive(false);
            mScriptChangebmSound.PlayAudio();
        }

        public void OpenQuit()
        {
            panelExit.SetActive(true);
            eff.SetActive(false);
            mScriptChangebmSound.PlayAudio();
        }

        public void clickQuit(bool check)
        {
            //mScriptChangebmSound.PlayAudio();
            if (check)
            {
                bgExit.SetActive(true);
                StartCoroutine(Delay());
            }
            else
            {
                panelExit.SetActive(false);
                eff.SetActive(true);
            }
        }

        private IEnumerator Delay()
        {
            yield return new WaitForSeconds(1.5f);
            Application.Quit();
        }

        public void BackMenu()
        {
            mScriptChangebmSound.PlayAudio();
            Application.LoadLevel("Menu");
        }

        public void SoundClick()
        {
            //mScriptChangebmSound.PlayAudio();
            var @int = PlayerPrefs.GetInt(Constains.KEY_SOUND, 1);
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

            MusicClick();
        }

        public void MusicClick()
        {
            //mScriptChangebmSound.PlayAudio();
            var @int = PlayerPrefs.GetInt(Constains.KEY_MUSIC, 1);
            if (@int == 1)
            {
                PlayerPrefs.SetInt(Constains.KEY_MUSIC, 0);
                PlayerPrefs.Save();
                //musicButton.GetComponent<Image>().sprite = musicOffSprite;
                mScriptChangebmMusic.StopAudiobm();
            }
            else
            {
                PlayerPrefs.SetInt(Constains.KEY_MUSIC, 1);
                PlayerPrefs.Save();
                //musicButton.GetComponent<Image>().sprite = musicOnSprite;
                mScriptChangebmMusic.PlayAudio();
            }
        }

        private void Setup1()
        {
            var @int = PlayerPrefs.GetInt(Constains.KEY_SOUND, 1);
            if (@int == 1)
                soundButton.GetComponent<Image>().sprite = soundOnSprite;
            else
                soundButton.GetComponent<Image>().sprite = soundOffSprite;
        }

        // public void Setup2()
        // {
        //     var @int = PlayerPrefs.GetInt(Constains.KEY_MUSIC, 1);
        //     if (@int == 1)
        //         musicButton.GetComponent<Image>().sprite = musicOnSprite;
        //     else
        //         musicButton.GetComponent<Image>().sprite = musicOffSprite;
        // }
    }
}