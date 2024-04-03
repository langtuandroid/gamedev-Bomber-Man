using System.Collections;
using UnityEngine;

public class ShowEffItembm : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(ShowEffbm());
	}

	private IEnumerator ShowEffbm()
	{
		yield return new WaitForSeconds(1f);
		Destroy(gameObject);
	}
}
