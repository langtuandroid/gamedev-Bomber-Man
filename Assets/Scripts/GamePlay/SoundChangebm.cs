using UnityEngine;

namespace GamePlay
{
    public class SoundChangebm : MonoBehaviour
    {
        public AudioSource audio;

        public void StopAudio()
        {
            audio.Stop();
        }

        public void PlayAudio()
        {
            var @int = PlayerPrefs.GetInt(Constains.KEY_SOUND, 1);
            if (@int == 1) audio.Play();
        }
    }
}