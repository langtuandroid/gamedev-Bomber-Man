using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Mask))]
[RequireComponent(typeof(ScrollRect))]
public class ScrollSnapRectbm : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IEventSystemHandler
{
	[Tooltip("Set starting page index - starting from 0")]
	public int startingPage;

	[Tooltip("Threshold time for fast swipe in seconds")]
	public float fastSwipeThresholdTime = 0.3f;

	[Tooltip("Threshold time for fast swipe in (unscaled) pixels")]
	public int fastSwipeThresholdDistance = 100;

	[Tooltip("How fast will page lerp to target position")]
	public float decelerationRate = 10f;

	[Tooltip("Button to go to the previous page (optional)")]
	public GameObject prevButton;

	[Tooltip("Button to go to the next page (optional)")]
	public GameObject nextButton;

	[Tooltip("Sprite for unselected page (optional)")]
	public Sprite unselectedPage;

	[Tooltip("Sprite for selected page (optional)")]
	public Sprite selectedPage;

	[Tooltip("Container with page images (optional)")]
	public Transform pageSelectionIcons;

	private int _fastSwipeThresholdMaxLimitbm;

	private ScrollRect _scrollRectComponentbm;

	private RectTransform _scrollRectRectbm;

	private RectTransform _containerbm;

	private bool _horizontalbm;

	private int _pageCountbm;

	private int _currentPagebm;

	private bool _lerpbm;

	private Vector2 _lerpTobm;

	private List<Vector2> _pagePositionsbm = new List<Vector2>();

	private bool _draggingbm;

	private float _timeStampbm;

	private Vector2 _startPositionbm;

	private bool _showPageSelectionbm;

	private int _previousPageSelectionIndexbm;

	private List<Image> _pageSelectionImagesbm;

	private void Start()
	{
		_scrollRectComponentbm = GetComponent<ScrollRect>();
		_scrollRectRectbm = GetComponent<RectTransform>();
		_containerbm = _scrollRectComponentbm.content;
		_pageCountbm = _containerbm.childCount;
		if (_scrollRectComponentbm.horizontal && !_scrollRectComponentbm.vertical)
		{
			_horizontalbm = true;
		}
		else if (!_scrollRectComponentbm.horizontal && _scrollRectComponentbm.vertical)
		{
			_horizontalbm = false;
		}
		else
		{
			Debug.LogWarning("Confusing setting of horizontal/vertical direction. Default set to horizontal.");
			_horizontalbm = true;
		}
		_lerpbm = false;
		SetPagePositionsbm();
		SetPagebm(startingPage);
		InitPageSelectionbm();
		SetPageSelectionbm(startingPage);
		if ((bool)nextButton)
		{
			nextButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				NextScreen();
			});
		}
		if ((bool)prevButton)
		{
			prevButton.GetComponent<Button>().onClick.AddListener(delegate
			{
				PreviousScreen();
			});
		}
	}

	private void Update()
	{
		if (_lerpbm)
		{
			float t = Mathf.Min(decelerationRate * Time.deltaTime, 1f);
			_containerbm.anchoredPosition = Vector2.Lerp(_containerbm.anchoredPosition, _lerpTobm, t);
			if (Vector2.SqrMagnitude(_containerbm.anchoredPosition - _lerpTobm) < 0.25f)
			{
				_containerbm.anchoredPosition = _lerpTobm;
				_lerpbm = false;
				_scrollRectComponentbm.velocity = Vector2.zero;
			}
			if (_showPageSelectionbm)
			{
				SetPageSelectionbm(GetNearestPage());
			}
		}
	}

	private void SetPagePositionsbm()
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		int num5 = 0;
		int num6 = 0;
		if (_horizontalbm)
		{
			num = (int)_scrollRectRectbm.rect.width;
			num3 = num / 2;
			num5 = num * _pageCountbm;
			_fastSwipeThresholdMaxLimitbm = num;
		}
		else
		{
			num2 = (int)_scrollRectRectbm.rect.height;
			num4 = num2 / 2;
			num6 = num2 * _pageCountbm;
			_fastSwipeThresholdMaxLimitbm = num2;
		}
		Vector2 sizeDelta = new Vector2(num5, num6);
		_containerbm.sizeDelta = sizeDelta;
		Vector2 anchoredPosition = new Vector2(num5 / 2, num6 / 2);
		_containerbm.anchoredPosition = anchoredPosition;
		_pagePositionsbm.Clear();
		for (int i = 0; i < _pageCountbm; i++)
		{
			RectTransform component = _containerbm.GetChild(i).GetComponent<RectTransform>();
			Vector2 vector2 = (component.anchoredPosition = ((!_horizontalbm) ? new Vector2(0f, -(i * num2 - num6 / 2 + num4)) : new Vector2(i * num - num5 / 2 + num3, 0f)));
			_pagePositionsbm.Add(-vector2);
		}
	}

	private void SetPagebm(int aPageIndex)
	{
		aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCountbm - 1);
		_containerbm.anchoredPosition = _pagePositionsbm[aPageIndex];
		_currentPagebm = aPageIndex;
	}

	private void LerpToPagebm(int aPageIndex)
	{
		aPageIndex = Mathf.Clamp(aPageIndex, 0, _pageCountbm - 1);
		_lerpTobm = _pagePositionsbm[aPageIndex];
		_lerpbm = true;
		_currentPagebm = aPageIndex;
	}

	private void InitPageSelectionbm()
	{
		_showPageSelectionbm = unselectedPage != null && selectedPage != null;
		if (!_showPageSelectionbm)
		{
			return;
		}
		if (pageSelectionIcons == null || pageSelectionIcons.childCount != _pageCountbm)
		{
			Debug.LogWarning("Different count of pages and selection icons - will not show page selection");
			_showPageSelectionbm = false;
			return;
		}
		_previousPageSelectionIndexbm = -1;
		_pageSelectionImagesbm = new List<Image>();
		for (int i = 0; i < pageSelectionIcons.childCount; i++)
		{
			Image component = pageSelectionIcons.GetChild(i).GetComponent<Image>();
			if (component == null)
			{
				Debug.LogWarning("Page selection icon at position " + i + " is missing Image component");
			}
			_pageSelectionImagesbm.Add(component);
		}
	}

	private void SetPageSelectionbm(int aPageIndex)
	{
		if (_previousPageSelectionIndexbm != aPageIndex)
		{
			if (_previousPageSelectionIndexbm >= 0)
			{
				_pageSelectionImagesbm[_previousPageSelectionIndexbm].sprite = unselectedPage;
				//_pageSelectionImages[_previousPageSelectionIndex].SetNativeSize();
			}
			_pageSelectionImagesbm[aPageIndex].sprite = selectedPage;
			//_pageSelectionImages[aPageIndex].SetNativeSize();
			_previousPageSelectionIndexbm = aPageIndex;
		}
	}

	private void NextScreen()
	{
		LerpToPagebm(_currentPagebm + 1);
	}

	private void PreviousScreen()
	{
		LerpToPagebm(_currentPagebm - 1);
	}

	private int GetNearestPage()
	{
		Vector2 anchoredPosition = _containerbm.anchoredPosition;
		float num = float.MaxValue;
		int result = _currentPagebm;
		for (int i = 0; i < _pagePositionsbm.Count; i++)
		{
			float num2 = Vector2.SqrMagnitude(anchoredPosition - _pagePositionsbm[i]);
			if (num2 < num)
			{
				num = num2;
				result = i;
			}
		}
		return result;
	}

	public void OnBeginDrag(PointerEventData aEventData)
	{
		_lerpbm = false;
		_draggingbm = false;
	}

	public void OnEndDrag(PointerEventData aEventData)
	{
		float num = ((!_horizontalbm) ? (0f - (_startPositionbm.y - _containerbm.anchoredPosition.y)) : (_startPositionbm.x - _containerbm.anchoredPosition.x));
		if (Time.unscaledTime - _timeStampbm < fastSwipeThresholdTime && Mathf.Abs(num) > (float)fastSwipeThresholdDistance && Mathf.Abs(num) < (float)_fastSwipeThresholdMaxLimitbm)
		{
			if (num > 0f)
			{
				NextScreen();
			}
			else
			{
				PreviousScreen();
			}
		}
		else
		{
			LerpToPagebm(GetNearestPage());
		}
		_draggingbm = false;
	}

	public void OnDrag(PointerEventData aEventData)
	{
		if (!_draggingbm)
		{
			_draggingbm = true;
			_timeStampbm = Time.unscaledTime;
			_startPositionbm = _containerbm.anchoredPosition;
		}
		else if (_showPageSelectionbm)
		{
			SetPageSelectionbm(GetNearestPage());
		}
	}
}
