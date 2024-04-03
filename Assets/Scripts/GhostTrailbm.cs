using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrailbm : MonoBehaviour
{
	private List<GameObject> _trailPartsbm = new List<GameObject>();

	private void Start()
	{
		InvokeRepeating("SpawnTrailPart", 0f, 0.055f);
	}

	private void SpawnTrailPart()
	{
		GameObject gameObject = new GameObject();
		SpriteRenderer spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		spriteRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
		gameObject.transform.position = base.transform.position;
		gameObject.transform.localScale = base.transform.localScale;
		_trailPartsbm.Add(gameObject);
		StartCoroutine(FadeTrailPart(spriteRenderer));
		StartCoroutine(DestroyTrailPart(gameObject, 0.3f));
	}

	private IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
	{
		Color color = trailPartRenderer.color;
		color.a -= 0.5f;
		trailPartRenderer.color = color;
		yield return new WaitForEndOfFrame();
	}

	private IEnumerator DestroyTrailPart(GameObject trailPart, float delay)
	{
		yield return new WaitForSeconds(delay);
		_trailPartsbm.Remove(trailPart);
		Object.Destroy(trailPart);
	}

	private void Flip()
	{
		Vector3 localScale = base.transform.localScale;
		localScale.x *= -1f;
		base.transform.localScale = localScale;
		FlipTrail();
	}

	private void FlipTrail()
	{
		foreach (GameObject trailPart in _trailPartsbm)
		{
			Vector3 localScale = trailPart.transform.localScale;
			localScale.x *= -1f;
			trailPart.transform.localScale = localScale;
		}
	}
}
