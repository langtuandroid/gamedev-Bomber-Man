using UnityEngine;

public class SoundGObm : MonoBehaviour
{
    public AudioSource audioPlayerDie;

    private void Start()
    {
        audioPlayerDie.Play();
    }
}