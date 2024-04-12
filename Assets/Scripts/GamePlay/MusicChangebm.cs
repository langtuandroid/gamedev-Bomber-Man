using UnityEngine;

namespace GamePlay
{
    public class MusicChangebm : MonoBehaviour
    {
        public AudioSource audio;

        private void Start()
        {
            if (PlayerPrefs.GetInt(Constains.KEY_MUSIC, 1) == 0)
                audio.Stop();
            else
                audio.Play();
        }

        public void StopAudiobm()
        {
            audio.Stop();
        }

        public void PlayAudio()
        {
            var @int = PlayerPrefs.GetInt(Constains.KEY_MUSIC, 1);
            if (@int == 1) audio.Play();
        }
    }
}