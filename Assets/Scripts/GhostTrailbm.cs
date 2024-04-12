using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrailbm : MonoBehaviour
{
    private readonly List<GameObject> _trailPartsbm = new();

    private void Start()
    {
        InvokeRepeating("SpawnTrailPart", 0f, 0.055f);
    }

    private void SpawnTrailPart()
    {
        var gameObject = new GameObject();
        var spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = GetComponent<SpriteRenderer>().sprite;
        gameObject.transform.position = transform.position;
        gameObject.transform.localScale = transform.localScale;
        _trailPartsbm.Add(gameObject);
        StartCoroutine(FadeTrailPart(spriteRenderer));
        StartCoroutine(DestroyTrailPart(gameObject, 0.3f));
    }

    private IEnumerator FadeTrailPart(SpriteRenderer trailPartRenderer)
    {
        var color = trailPartRenderer.color;
        color.a -= 0.5f;
        trailPartRenderer.color = color;
        yield return new WaitForEndOfFrame();
    }

    private IEnumerator DestroyTrailPart(GameObject trailPart, float delay)
    {
        yield return new WaitForSeconds(delay);
        _trailPartsbm.Remove(trailPart);
        Destroy(trailPart);
    }
    
}