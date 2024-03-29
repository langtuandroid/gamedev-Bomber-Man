using UnityEngine;

public class soundGO : MonoBehaviour
{
	public AudioSource audioPlayerDie;

	private void Start()
	{
		audioPlayerDie.Play();
	}

	private void Update()
	{
	}
}
