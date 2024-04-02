using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hello : MonoBehaviour, IDragHandler, IEventSystemHandler
{
	private ScrollRect scrollRect;

	private void Start()
	{
		scrollRect = GetComponentInParent<ScrollRect>();
	}

	public void OnDrag(PointerEventData eventData)
	{
		scrollRect.horizontalNormalizedPosition -= eventData.delta.x / (float)Screen.width;
	}
}
