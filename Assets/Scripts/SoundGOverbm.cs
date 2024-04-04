using UnityEngine;

public class SoundGOverbm : MonoBehaviour
{
    public AudioSource audioPlayerDie;

    private void Start()
    {
        audioPlayerDie.Play();
    }
}