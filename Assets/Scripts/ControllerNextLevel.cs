using UnityEngine;

public class ControllerNextLevel : MonoBehaviour
{
	public GameObject obj;

	public float speed;

	public Animator anim;

	private void Start()
	{
		anim = GetComponent<Animator>();
	}

	private void Update()
	{
	}

	public void nextBtn()
	{
		anim.SetBool("isNext", false);
	}

	public void backBtn()
	{
	}
}
