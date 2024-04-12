using UnityEngine;

public class Gesturebm
{
    public float actionTime;

    public float deltaPinch;

    public Vector2 deltaPosition;

    public float deltaTime;
    public int fingerIndex;

    public bool isGuiCamera;

    public bool isHoverReservedArea;

    public GameObject otherReceiver;

    public Camera pickCamera;

    public GameObject pickObject;

    public Vector2 position;

    public Vector2 startPosition;

    public EasyTouch.SwipeType swipe;

    public float swipeLength;

    public Vector2 swipeVector;

    public int touchCount;

    public float twistAngle;

    public float twoFingerDistance;

    public bool IsInRect(Rect rect, bool guiRect = false)
    {
        if (guiRect) rect = new Rect(rect.x, Screen.height - rect.y - rect.height, rect.width, rect.height);
        return rect.Contains(position);
    }
    
}