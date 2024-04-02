using UnityEngine;

namespace XX
{
	public class SoundChange : MonoBehaviour
	{
		public AudioSource audio;

		public void StopAudio()
		{
			audio.Stop();
		}

		public void PlayAudio()
		{
			int @int = PlayerPrefs.GetInt(Constains.KEY_SOUND, 1);
			if (@int == 1)
			{
				audio.Play();
			}
		}
	}
}
