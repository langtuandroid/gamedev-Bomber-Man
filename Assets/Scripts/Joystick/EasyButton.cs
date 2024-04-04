using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using Debug = UnityEngine.Debug;

[ExecuteInEditMode]
public class EasyButton : MonoBehaviour
{
    public delegate void ButtonDownHandler(string buttonName);

    public delegate void ButtonPressHandler(string buttonName);

    public delegate void ButtonUpHandler(string buttonName);

    public enum Broadcast
    {
        SendMessage = 0,
        SendMessageUpwards = 1,
        BroadcastMessage = 2
    }

    public enum ButtonAnchor
    {
        UpperLeft = 0,
        UpperCenter = 1,
        UpperRight = 2,
        MiddleLeft = 3,
        MiddleCenter = 4,
        MiddleRight = 5,
        LowerLeft = 6,
        LowerCenter = 7,
        LowerRight = 8
    }

    public enum ButtonState
    {
        Down = 0,
        Press = 1,
        Up = 2,
        None = 3
    }

    public enum InteractionType
    {
        Event = 0,
        Include = 1
    }

    [CompilerGenerated] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static ButtonDownHandler m_On_ButtonDown;

    [CompilerGenerated] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static ButtonPressHandler m_On_ButtonPress;

    [CompilerGenerated] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static ButtonUpHandler m_On_ButtonUp;

    public bool enable = true;

    public bool isActivated = true;

    public bool showDebugArea = true;

    public bool selected;

    public bool isUseGuiLayout = true;

    public ButtonState buttonState = ButtonState.None;

    [SerializeField] private ButtonAnchor anchor = ButtonAnchor.LowerRight;

    [SerializeField] private Vector2 offset = Vector2.zero;

    [SerializeField] private Vector2 scale = Vector2.one;

    public bool isSwipeIn;

    public bool isSwipeOut;

    public InteractionType interaction;

    public bool useBroadcast;

    public GameObject receiverGameObject;

    public Broadcast messageMode;

    public bool useSpecificalMethod;

    public string downMethodName;

    public string pressMethodName;

    public string upMethodName;

    public int guiDepth;

    [SerializeField] private Texture2D normalTexture;

    public Color buttonNormalColor = Color.white;

    [SerializeField] private Texture2D activeTexture;

    public Color buttonActiveColor = Color.white;

    public bool showInspectorProperties = true;

    public bool showInspectorPosition = true;

    public bool showInspectorEvent;

    public bool showInspectorTexture;

    public Rect buttonRect;

    private int buttonFingerIndex = -1;

    private Color currentColor;

    private Texture2D currentTexture;

    private int frame;

    public ButtonAnchor Anchor
    {
        get => anchor;
        set
        {
            anchor = value;
            ComputeButtonAnchor(anchor);
        }
    }

    public Vector2 Offset
    {
        get => offset;
        set
        {
            offset = value;
            ComputeButtonAnchor(anchor);
        }
    }

    public Vector2 Scale
    {
        get => scale;
        set
        {
            scale = value;
            ComputeButtonAnchor(anchor);
        }
    }

    public Texture2D NormalTexture
    {
        get => normalTexture;
        set
        {
            normalTexture = value;
            if (normalTexture != null)
            {
                ComputeButtonAnchor(anchor);
                currentTexture = normalTexture;
            }
        }
    }

    public Texture2D ActiveTexture
    {
        get => activeTexture;
        set => activeTexture = value;
    }

    private void Start()
    {
        currentTexture = normalTexture;
        currentColor = buttonNormalColor;
        buttonState = ButtonState.None;
        VirtualScreenbm.ComputeVirtualScreenbm();
        ComputeButtonAnchor(anchor);
    }

    private void Update()
    {
        if (buttonState == ButtonState.Up) buttonState = ButtonState.None;
        if (EasyTouch.GetTouchCount() == 0)
        {
            buttonFingerIndex = -1;
            currentTexture = normalTexture;
            currentColor = buttonNormalColor;
            buttonState = ButtonState.None;
        }
    }

    private void OnEnable()
    {
        EasyTouch.On_TouchStart += On_TouchStart;
        EasyTouch.On_TouchDown += On_TouchDown;
        EasyTouch.On_TouchUp += On_TouchUp;
    }

    private void OnDisable()
    {
        EasyTouch.On_TouchStart -= On_TouchStart;
        EasyTouch.On_TouchDown -= On_TouchDown;
        EasyTouch.On_TouchUp -= On_TouchUp;
        if (Application.isPlaying && EasyTouch.instance != null)
            EasyTouch.instance.reservedVirtualAreas.Remove(buttonRect);
    }

    private void OnDestroy()
    {
        EasyTouch.On_TouchStart -= On_TouchStart;
        EasyTouch.On_TouchDown -= On_TouchDown;
        EasyTouch.On_TouchUp -= On_TouchUp;
        if (Application.isPlaying && EasyTouch.instance != null)
            EasyTouch.instance.reservedVirtualAreas.Remove(buttonRect);
    }

    private void OnGUI()
    {
        if (enable)
        {
            GUI.depth = guiDepth;
            useGUILayout = isUseGuiLayout;
            VirtualScreenbm.ComputeVirtualScreenbm();
            VirtualScreenbm.SetGuiScaleMatrixbm();
            if (!(normalTexture != null) || !(activeTexture != null)) return;
            ComputeButtonAnchor(anchor);
            if (!(normalTexture != null)) return;
            if (Application.isEditor && !Application.isPlaying) currentTexture = normalTexture;
            if (showDebugArea && Application.isEditor && selected && !Application.isPlaying)
                GUI.Box(buttonRect, string.Empty);
            if (!(currentTexture != null)) return;
            if (isActivated)
            {
                GUI.color = currentColor;
                if (Application.isPlaying)
                {
                    EasyTouch.instance.reservedVirtualAreas.Remove(buttonRect);
                    EasyTouch.instance.reservedVirtualAreas.Add(buttonRect);
                }
            }
            else
            {
                GUI.color = new Color(currentColor.r, currentColor.g, currentColor.b, 0.2f);
                if (Application.isPlaying) EasyTouch.instance.reservedVirtualAreas.Remove(buttonRect);
            }

            GUI.DrawTexture(buttonRect, currentTexture);
            GUI.color = Color.white;
        }
        else if (Application.isPlaying)
        {
            EasyTouch.instance.reservedVirtualAreas.Remove(buttonRect);
        }
    }

    private void OnDrawGizmos()
    {
    }

    public static event ButtonDownHandler On_ButtonDown;

    public static event ButtonPressHandler On_ButtonPress;

    public static event ButtonUpHandler On_ButtonUp;

    private void ComputeButtonAnchor(ButtonAnchor anchor)
    {
        if (normalTexture != null)
        {
            var vector = new Vector2(normalTexture.width * scale.x, normalTexture.height * scale.y);
            var vector2 = Vector2.zero;
            switch (anchor)
            {
                case ButtonAnchor.UpperLeft:
                    vector2 = new Vector2(0f, 0f);
                    break;
                case ButtonAnchor.UpperCenter:
                    vector2 = new Vector2(VirtualScreenbm.width / 2f - vector.x / 2f, offset.y);
                    break;
                case ButtonAnchor.UpperRight:
                    vector2 = new Vector2(VirtualScreenbm.width - vector.x, 0f);
                    break;
                case ButtonAnchor.MiddleLeft:
                    vector2 = new Vector2(0f, VirtualScreenbm.height / 2f - vector.y / 2f);
                    break;
                case ButtonAnchor.MiddleCenter:
                    vector2 = new Vector2(VirtualScreenbm.width / 2f - vector.x / 2f,
                        VirtualScreenbm.height / 2f - vector.y / 2f);
                    break;
                case ButtonAnchor.MiddleRight:
                    vector2 = new Vector2(VirtualScreenbm.width - vector.x,
                        VirtualScreenbm.height / 2f - vector.y / 2f);
                    break;
                case ButtonAnchor.LowerLeft:
                    vector2 = new Vector2(0f, VirtualScreenbm.height - vector.y);
                    break;
                case ButtonAnchor.LowerCenter:
                    vector2 = new Vector2(VirtualScreenbm.width / 2f - vector.x / 2f,
                        VirtualScreenbm.height - vector.y);
                    break;
                case ButtonAnchor.LowerRight:
                    vector2 = new Vector2(VirtualScreenbm.width - vector.x, VirtualScreenbm.height - vector.y);
                    break;
            }

            buttonRect = new Rect(vector2.x + offset.x, vector2.y + offset.y, vector.x, vector.y);
        }
    }

    private void RaiseEvent(MessageName msg)
    {
        if (interaction != 0) return;
        if (!useBroadcast)
        {
            switch (msg)
            {
                case MessageName.On_ButtonDown:
                    if (On_ButtonDown != null) On_ButtonDown(gameObject.name);
                    break;
                case MessageName.On_ButtonUp:
                    if (On_ButtonUp != null) On_ButtonUp(gameObject.name);
                    break;
                case MessageName.On_ButtonPress:
                    if (On_ButtonPress != null) On_ButtonPress(gameObject.name);
                    break;
            }

            return;
        }

        var methodName = msg.ToString();
        if (msg == MessageName.On_ButtonDown && downMethodName != string.Empty && useSpecificalMethod)
            methodName = downMethodName;
        if (msg == MessageName.On_ButtonPress && pressMethodName != string.Empty && useSpecificalMethod)
            methodName = pressMethodName;
        if (msg == MessageName.On_ButtonUp && upMethodName != string.Empty && useSpecificalMethod)
            methodName = upMethodName;
        if (receiverGameObject != null)
            switch (messageMode)
            {
                case Broadcast.BroadcastMessage:
                    receiverGameObject.BroadcastMessage(methodName, name, SendMessageOptions.DontRequireReceiver);
                    break;
                case Broadcast.SendMessage:
                    receiverGameObject.SendMessage(methodName, name, SendMessageOptions.DontRequireReceiver);
                    break;
                case Broadcast.SendMessageUpwards:
                    receiverGameObject.SendMessageUpwards(methodName, name, SendMessageOptions.DontRequireReceiver);
                    break;
            }
        else
            Debug.LogError("Button : " + gameObject.name + " : you must setup receiver gameobject");
    }

    private void On_TouchStart(Gesturebm gesturebm)
    {
        if (gesturebm.IsInRect(VirtualScreenbm.GetRealRectbm(buttonRect), true) && enable && isActivated)
        {
            buttonFingerIndex = gesturebm.fingerIndex;
            currentTexture = activeTexture;
            currentColor = buttonActiveColor;
            buttonState = ButtonState.Down;
            frame = 0;
            RaiseEvent(MessageName.On_ButtonDown);
        }
    }

    private void On_TouchDown(Gesturebm gesturebm)
    {
        if (gesturebm.fingerIndex != buttonFingerIndex && (!isSwipeIn || buttonState != ButtonState.None)) return;
        if (gesturebm.IsInRect(VirtualScreenbm.GetRealRectbm(buttonRect), true) && enable && isActivated)
        {
            currentTexture = activeTexture;
            currentColor = buttonActiveColor;
            frame++;
            if ((buttonState == ButtonState.Down || buttonState == ButtonState.Press) && frame >= 2)
            {
                RaiseEvent(MessageName.On_ButtonPress);
                buttonState = ButtonState.Press;
            }

            if (buttonState == ButtonState.None)
            {
                buttonFingerIndex = gesturebm.fingerIndex;
                buttonState = ButtonState.Down;
                frame = 0;
                RaiseEvent(MessageName.On_ButtonDown);
            }
        }
        else if ((isSwipeIn || !isSwipeIn) && !isSwipeOut && buttonState == ButtonState.Press)
        {
            buttonFingerIndex = -1;
            currentTexture = normalTexture;
            currentColor = buttonNormalColor;
            buttonState = ButtonState.None;
        }
        else if (isSwipeOut && buttonState == ButtonState.Press)
        {
            RaiseEvent(MessageName.On_ButtonPress);
            buttonState = ButtonState.Press;
        }
    }

    private void On_TouchUp(Gesturebm gesturebm)
    {
        if (gesturebm.fingerIndex == buttonFingerIndex)
        {
            if ((gesturebm.IsInRect(VirtualScreenbm.GetRealRectbm(buttonRect), true) ||
                 (isSwipeOut && buttonState == ButtonState.Press)) && enable &&
                isActivated) RaiseEvent(MessageName.On_ButtonUp);
            buttonState = ButtonState.Up;
            buttonFingerIndex = -1;
            currentTexture = normalTexture;
            currentColor = buttonNormalColor;
        }
    }

    private enum MessageName
    {
        On_ButtonDown = 0,
        On_ButtonPress = 1,
        On_ButtonUp = 2
    }
}