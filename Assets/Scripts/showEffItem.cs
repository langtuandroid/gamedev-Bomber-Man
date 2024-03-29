using System.Collections;
using UnityEngine;

public class showEffItem : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(showEff());
	}

	private IEnumerator showEff()
	{
		yield return new WaitForSeconds(1f);
		Object.Destroy(base.gameObject);
	}
}
