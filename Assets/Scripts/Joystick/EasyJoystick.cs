using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

[ExecuteInEditMode]
public class EasyJoystick : MonoBehaviour
{
	public delegate void JoystickMoveStartHandler(MovingJoystick move);

	public delegate void JoystickMoveHandler(MovingJoystick move);

	public delegate void JoystickMoveEndHandler(MovingJoystick move);

	public delegate void JoystickTouchStartHandler(MovingJoystick move);

	public delegate void JoystickTapHandler(MovingJoystick move);

	public delegate void JoystickDoubleTapHandler(MovingJoystick move);

	public delegate void JoystickTouchUpHandler(MovingJoystick move);

	public enum JoystickAnchor
	{
		None = 0,
		UpperLeft = 1,
		UpperCenter = 2,
		UpperRight = 3,
		MiddleLeft = 4,
		MiddleCenter = 5,
		MiddleRight = 6,
		LowerLeft = 7,
		LowerCenter = 8,
		LowerRight = 9
	}

	public enum PropertiesInfluenced
	{
		Rotate = 0,
		RotateLocal = 1,
		Translate = 2,
		TranslateLocal = 3,
		Scale = 4
	}

	public enum AxisInfluenced
	{
		X = 0,
		Y = 1,
		Z = 2,
		XYZ = 3
	}

	public enum DynamicArea
	{
		FullScreen = 0,
		Left = 1,
		Right = 2,
		Top = 3,
		Bottom = 4,
		TopLeft = 5,
		TopRight = 6,
		BottomLeft = 7,
		BottomRight = 8
	}

	public enum InteractionType
	{
		Direct = 0,
		Include = 1,
		EventNotification = 2,
		DirectAndEvent = 3
	}

	public enum Broadcast
	{
		SendMessage = 0,
		SendMessageUpwards = 1,
		BroadcastMessage = 2
	}

	private enum MessageName
	{
		On_JoystickMoveStart = 0,
		On_JoystickTouchStart = 1,
		On_JoystickTouchUp = 2,
		On_JoystickMove = 3,
		On_JoystickMoveEnd = 4,
		On_JoystickTap = 5,
		On_JoystickDoubleTap = 6
	}

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static JoystickMoveStartHandler m_On_JoystickMoveStart;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static JoystickMoveHandler m_On_JoystickMove;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static JoystickMoveEndHandler m_On_JoystickMoveEnd;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static JoystickTouchStartHandler m_On_JoystickTouchStart;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static JoystickTapHandler m_On_JoystickTap;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static JoystickDoubleTapHandler m_On_JoystickDoubleTap;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private static JoystickTouchUpHandler m_On_JoystickTouchUp;

	private Vector2 joystickAxis;

	private Vector2 joystickTouch;

	private Vector2 joystickValue;

	public bool enable = true;

	public bool visible = true;

	public bool isActivated = true;

	public bool showDebugRadius;

	public bool selected;

	public bool useFixedUpdate;

	public bool isUseGuiLayout = true;

	[SerializeField]
	private bool dynamicJoystick;

	public DynamicArea area;

	[SerializeField]
	private JoystickAnchor joyAnchor = JoystickAnchor.LowerLeft;

	[SerializeField]
	private Vector2 joystickPositionOffset = Vector2.zero;

	[SerializeField]
	private float zoneRadius = 100f;

	[SerializeField]
	private float touchSize = 30f;

	public float deadZone = 20f;

	[SerializeField]
	private bool restrictArea;

	public bool resetFingerExit;

	[SerializeField]
	private InteractionType interaction;

	public bool useBroadcast;

	public Broadcast messageMode;

	public GameObject receiverGameObject;

	public Vector2 speed;

	public bool enableXaxis = true;

	[SerializeField]
	private Transform xAxisTransform;

	public CharacterController xAxisCharacterController;

	public float xAxisGravity;

	[SerializeField]
	private PropertiesInfluenced xTI;

	public AxisInfluenced xAI;

	public bool inverseXAxis;

	public bool enableXClamp;

	public float clampXMax;

	public float clampXMin;

	public bool enableXAutoStab;

	[SerializeField]
	private float thresholdX = 0.01f;

	[SerializeField]
	private float stabSpeedX = 20f;

	public bool enableYaxis = true;

	[SerializeField]
	private Transform yAxisTransform;

	public CharacterController yAxisCharacterController;

	public float yAxisGravity;

	[SerializeField]
	private PropertiesInfluenced yTI;

	public AxisInfluenced yAI;

	public bool inverseYAxis;

	public bool enableYClamp;

	public float clampYMax;

	public float clampYMin;

	public bool enableYAutoStab;

	[SerializeField]
	private float thresholdY = 0.01f;

	[SerializeField]
	private float stabSpeedY = 20f;

	public bool enableSmoothing;

	[SerializeField]
	public Vector2 smoothing = new Vector2(2f, 2f);

	public bool enableInertia;

	[SerializeField]
	public Vector2 inertia = new Vector2(100f, 100f);

	public int guiDepth;

	public bool showZone = true;

	public bool showTouch = true;

	public bool showDeadZone = true;

	public Texture areaTexture;

	public Color areaColor = Color.white;

	public Texture touchTexture;

	public Color touchColor = Color.white;

	public Texture deadTexture;

	public bool showProperties = true;

	public bool showInteraction;

	public bool showAppearance;

	public bool showPosition = true;

	private Vector2 joystickCenter;

	private Rect areaRect;

	private Rect deadRect;

	private Vector2 anchorPosition = Vector2.zero;

	private bool virtualJoystick = true;

	private int joystickIndex = -1;

	private float touchSizeCoef;

	private bool sendEnd = true;

	private float startXLocalAngle;

	private float startYLocalAngle;

	public Vector2 JoystickAxis
	{
		get
		{
			return joystickAxis;
		}
	}

	public Vector2 JoystickTouch
	{
		get
		{
			return new Vector2(joystickTouch.x / zoneRadius, joystickTouch.y / zoneRadius);
		}
		set
		{
			float x = Mathf.Clamp(value.x, -1f, 1f) * zoneRadius;
			float y = Mathf.Clamp(value.y, -1f, 1f) * zoneRadius;
			joystickTouch = new Vector2(x, y);
		}
	}

	public Vector2 JoystickValue
	{
		get
		{
			return joystickValue;
		}
	}

	public bool DynamicJoystick
	{
		get
		{
			return dynamicJoystick;
		}
		set
		{
			joystickIndex = -1;
			dynamicJoystick = value;
			if (dynamicJoystick)
			{
				virtualJoystick = false;
				return;
			}
			virtualJoystick = true;
			joystickCenter = joystickPositionOffset;
		}
	}

	public JoystickAnchor JoyAnchor
	{
		get
		{
			return joyAnchor;
		}
		set
		{
			joyAnchor = value;
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public Vector2 JoystickPositionOffset
	{
		get
		{
			return joystickPositionOffset;
		}
		set
		{
			joystickPositionOffset = value;
			joystickCenter = joystickPositionOffset;
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public float ZoneRadius
	{
		get
		{
			return zoneRadius;
		}
		set
		{
			zoneRadius = value;
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public float TouchSize
	{
		get
		{
			return touchSize;
		}
		set
		{
			touchSize = value;
			if (touchSize > zoneRadius / 2f && restrictArea)
			{
				touchSize = zoneRadius / 2f;
			}
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public bool RestrictArea
	{
		get
		{
			return restrictArea;
		}
		set
		{
			restrictArea = value;
			if (restrictArea)
			{
				touchSizeCoef = touchSize;
			}
			else
			{
				touchSizeCoef = 0f;
			}
			ComputeJoystickAnchor(joyAnchor);
		}
	}

	public InteractionType Interaction
	{
		get
		{
			return interaction;
		}
		set
		{
			interaction = value;
			if (interaction == InteractionType.Direct || interaction == InteractionType.Include)
			{
				useBroadcast = false;
			}
		}
	}

	public Transform XAxisTransform
	{
		get
		{
			return xAxisTransform;
		}
		set
		{
			xAxisTransform = value;
			if (xAxisTransform != null)
			{
				xAxisCharacterController = xAxisTransform.GetComponent<CharacterController>();
				return;
			}
			xAxisCharacterController = null;
			xAxisGravity = 0f;
		}
	}

	public PropertiesInfluenced XTI
	{
		get
		{
			return xTI;
		}
		set
		{
			xTI = value;
			if (xTI != PropertiesInfluenced.RotateLocal)
			{
				enableXAutoStab = false;
				enableXClamp = false;
			}
		}
	}

	public float ThresholdX
	{
		get
		{
			return thresholdX;
		}
		set
		{
			if (value <= 0f)
			{
				thresholdX = value * -1f;
			}
			else
			{
				thresholdX = value;
			}
		}
	}

	public float StabSpeedX
	{
		get
		{
			return stabSpeedX;
		}
		set
		{
			if (value <= 0f)
			{
				stabSpeedX = value * -1f;
			}
			else
			{
				stabSpeedX = value;
			}
		}
	}

	public Transform YAxisTransform
	{
		get
		{
			return yAxisTransform;
		}
		set
		{
			yAxisTransform = value;
			if (yAxisTransform != null)
			{
				yAxisCharacterController = yAxisTransform.GetComponent<CharacterController>();
				return;
			}
			yAxisCharacterController = null;
			yAxisGravity = 0f;
		}
	}

	public PropertiesInfluenced YTI
	{
		get
		{
			return yTI;
		}
		set
		{
			yTI = value;
			if (yTI != PropertiesInfluenced.RotateLocal)
			{
				enableYAutoStab = false;
				enableYClamp = false;
			}
		}
	}

	public float ThresholdY
	{
		get
		{
			return thresholdY;
		}
		set
		{
			if (value <= 0f)
			{
				thresholdY = value * -1f;
			}
			else
			{
				thresholdY = value;
			}
		}
	}

	public float StabSpeedY
	{
		get
		{
			return stabSpeedY;
		}
		set
		{
			if (value <= 0f)
			{
				stabSpeedY = value * -1f;
			}
			else
			{
				stabSpeedY = value;
			}
		}
	}

	public Vector2 Smoothing
	{
		get
		{
			return smoothing;
		}
		set
		{
			smoothing = value;
			if (smoothing.x < 0f)
			{
				smoothing.x = 0f;
			}
			if (smoothing.y < 0f)
			{
				smoothing.y = 0f;
			}
		}
	}

	public Vector2 Inertia
	{
		get
		{
			return inertia;
		}
		set
		{
			inertia = value;
			if (inertia.x <= 0f)
			{
				inertia.x = 1f;
			}
			if (inertia.y <= 0f)
			{
				inertia.y = 1f;
			}
		}
	}

	public static event JoystickMoveStartHandler On_JoystickMoveStart
	;

	public static event JoystickMoveHandler On_JoystickMove
	;

	public static event JoystickMoveEndHandler On_JoystickMoveEnd
	;

	public static event JoystickTouchStartHandler On_JoystickTouchStart
	;

	public static event JoystickTapHandler On_JoystickTap
	;

	public static event JoystickDoubleTapHandler On_JoystickDoubleTap
	;

	public static event JoystickTouchUpHandler On_JoystickTouchUp
	;

	private void OnLevelWasLoaded()
	{
		joystickIndex = -1;
	}

	private void OnEnable()
	{
		EasyTouch.On_TouchStart += On_TouchStart;
		EasyTouch.On_TouchUp += On_TouchUp;
		EasyTouch.On_TouchDown += On_TouchDown;
		EasyTouch.On_SimpleTap += On_SimpleTap;
		EasyTouch.On_DoubleTap += On_DoubleTap;
	}

	private void OnDisable()
	{
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchUp -= On_TouchUp;
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_SimpleTap -= On_SimpleTap;
		EasyTouch.On_DoubleTap -= On_DoubleTap;
		if (Application.isPlaying && EasyTouch.instance != null)
		{
			EasyTouch.instance.reservedVirtualAreas.Remove(areaRect);
		}
	}

	private void OnDestroy()
	{
		EasyTouch.On_TouchStart -= On_TouchStart;
		EasyTouch.On_TouchUp -= On_TouchUp;
		EasyTouch.On_TouchDown -= On_TouchDown;
		EasyTouch.On_SimpleTap -= On_SimpleTap;
		EasyTouch.On_DoubleTap -= On_DoubleTap;
		if (Application.isPlaying && EasyTouch.instance != null)
		{
			EasyTouch.instance.reservedVirtualAreas.Remove(areaRect);
		}
	}

	private void Start()
	{
		if (!dynamicJoystick)
		{
			joystickCenter = joystickPositionOffset;
			ComputeJoystickAnchor(joyAnchor);
			virtualJoystick = true;
		}
		else
		{
			virtualJoystick = false;
		}
		VirtualScreenbm.ComputeVirtualScreenbm();
		startXLocalAngle = GetStartAutoStabAngle(xAxisTransform, xAI);
		startYLocalAngle = GetStartAutoStabAngle(yAxisTransform, yAI);
		RestrictArea = restrictArea;
	}

	private void Update()
	{
		if (!useFixedUpdate && enable)
		{
			UpdateJoystick();
		}
	}

	private void FixedUpdate()
	{
		if (useFixedUpdate && enable)
		{
			UpdateJoystick();
		}
	}

	private void UpdateJoystick()
	{
		if (!Application.isPlaying)
		{
			return;
		}
		if (EasyTouch.GetTouchCount() == 0)
		{
			joystickIndex = -1;
			if (dynamicJoystick)
			{
				virtualJoystick = false;
			}
		}
		if (!isActivated)
		{
			return;
		}
		if (joystickIndex == -1 || (joystickAxis == Vector2.zero && joystickIndex > -1))
		{
			if (enableXAutoStab)
			{
				DoAutoStabilisation(xAxisTransform, xAI, thresholdX, stabSpeedX, startXLocalAngle);
			}
			if (enableYAutoStab)
			{
				DoAutoStabilisation(yAxisTransform, yAI, thresholdY, stabSpeedY, startYLocalAngle);
			}
		}
		if (!dynamicJoystick)
		{
			joystickCenter = joystickPositionOffset;
		}
		if (joystickIndex == -1)
		{
			if (!enableSmoothing)
			{
				joystickTouch = Vector2.zero;
			}
			else if ((double)joystickTouch.sqrMagnitude > 0.0001)
			{
				joystickTouch = new Vector2(joystickTouch.x - joystickTouch.x * smoothing.x * Time.deltaTime, joystickTouch.y - joystickTouch.y * smoothing.y * Time.deltaTime);
			}
			else
			{
				joystickTouch = Vector2.zero;
			}
		}
		Vector2 vector = new Vector2(joystickAxis.x, joystickAxis.y);
		float num = ComputeDeadZone();
		joystickAxis = new Vector2(joystickTouch.x * num, joystickTouch.y * num);
		if (inverseXAxis)
		{
			joystickAxis.x *= -1f;
		}
		if (inverseYAxis)
		{
			joystickAxis.y *= -1f;
		}
		Vector2 vector2 = new Vector2(speed.x * joystickAxis.x, speed.y * joystickAxis.y);
		if (enableInertia)
		{
			Vector2 vector3 = vector2 - joystickValue;
			vector3.x /= inertia.x;
			vector3.y /= inertia.y;
			joystickValue += vector3;
		}
		else
		{
			joystickValue = vector2;
		}
		if (vector == Vector2.zero && joystickAxis != Vector2.zero && interaction != 0 && interaction != InteractionType.Include)
		{
			CreateEvent(MessageName.On_JoystickMoveStart);
		}
		UpdateGravity();
		if (joystickAxis != Vector2.zero)
		{
			sendEnd = false;
			switch (interaction)
			{
			case InteractionType.Direct:
				UpdateDirect();
				break;
			case InteractionType.EventNotification:
				CreateEvent(MessageName.On_JoystickMove);
				break;
			case InteractionType.DirectAndEvent:
				UpdateDirect();
				CreateEvent(MessageName.On_JoystickMove);
				break;
			}
		}
		else if (!sendEnd)
		{
			CreateEvent(MessageName.On_JoystickMoveEnd);
			sendEnd = true;
		}
	}

	private void OnGUI()
	{
		if (enable && visible)
		{
			GUI.depth = guiDepth;
			base.useGUILayout = isUseGuiLayout;
			if (dynamicJoystick && Application.isEditor && !Application.isPlaying)
			{
				switch (area)
				{
				case DynamicArea.Bottom:
					ComputeJoystickAnchor(JoystickAnchor.LowerCenter);
					break;
				case DynamicArea.BottomLeft:
					ComputeJoystickAnchor(JoystickAnchor.LowerLeft);
					break;
				case DynamicArea.BottomRight:
					ComputeJoystickAnchor(JoystickAnchor.LowerRight);
					break;
				case DynamicArea.FullScreen:
					ComputeJoystickAnchor(JoystickAnchor.MiddleCenter);
					break;
				case DynamicArea.Left:
					ComputeJoystickAnchor(JoystickAnchor.MiddleLeft);
					break;
				case DynamicArea.Right:
					ComputeJoystickAnchor(JoystickAnchor.MiddleRight);
					break;
				case DynamicArea.Top:
					ComputeJoystickAnchor(JoystickAnchor.UpperCenter);
					break;
				case DynamicArea.TopLeft:
					ComputeJoystickAnchor(JoystickAnchor.UpperLeft);
					break;
				case DynamicArea.TopRight:
					ComputeJoystickAnchor(JoystickAnchor.UpperRight);
					break;
				}
			}
			if (Application.isEditor && !Application.isPlaying)
			{
				VirtualScreenbm.ComputeVirtualScreenbm();
				ComputeJoystickAnchor(joyAnchor);
			}
			VirtualScreenbm.SetGuiScaleMatrixbm();
			ComputeJoystickAnchor(joyAnchor);
			if ((showZone && areaTexture != null && !dynamicJoystick) || (showZone && dynamicJoystick && virtualJoystick && areaTexture != null) || (dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				if (isActivated)
				{
					GUI.color = areaColor;
					if (Application.isPlaying && !dynamicJoystick)
					{
						EasyTouch.instance.reservedVirtualAreas.Remove(areaRect);
						EasyTouch.instance.reservedVirtualAreas.Add(areaRect);
					}
				}
				else
				{
					GUI.color = new Color(areaColor.r, areaColor.g, areaColor.b, 0.2f);
					if (Application.isPlaying && !dynamicJoystick)
					{
						EasyTouch.instance.reservedVirtualAreas.Remove(areaRect);
					}
				}
				if (showDebugRadius && Application.isEditor && selected && !Application.isPlaying)
				{
					GUI.Box(areaRect, string.Empty);
				}
				GUI.DrawTexture(areaRect, areaTexture, ScaleMode.StretchToFill, true);
			}
			if ((showTouch && touchTexture != null && !dynamicJoystick) || (showTouch && dynamicJoystick && virtualJoystick && touchTexture != null) || (dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				if (isActivated)
				{
					GUI.color = touchColor;
				}
				else
				{
					GUI.color = new Color(touchColor.r, touchColor.g, touchColor.b, 0.2f);
				}
				GUI.DrawTexture(new Rect(anchorPosition.x + joystickCenter.x + (joystickTouch.x - touchSize), anchorPosition.y + joystickCenter.y - (joystickTouch.y + touchSize), touchSize * 2f, touchSize * 2f), touchTexture, ScaleMode.ScaleToFit, true);
			}
			if ((showDeadZone && deadTexture != null && !dynamicJoystick) || (showDeadZone && dynamicJoystick && virtualJoystick && deadTexture != null) || (dynamicJoystick && Application.isEditor && !Application.isPlaying))
			{
				GUI.DrawTexture(deadRect, deadTexture, ScaleMode.ScaleToFit, true);
			}
			GUI.color = Color.white;
		}
		else if (Application.isPlaying)
		{
			EasyTouch.instance.reservedVirtualAreas.Remove(areaRect);
		}
	}

	private void OnDrawGizmos()
	{
	}

	private void CreateEvent(MessageName message)
	{
		MovingJoystick movingJoystick = new MovingJoystick();
		movingJoystick.joystickName = base.gameObject.name;
		movingJoystick.joystickAxis = joystickAxis;
		movingJoystick.joystickValue = joystickValue;
		movingJoystick.fingerIndex = joystickIndex;
		movingJoystick.joystick = this;
		if (!useBroadcast)
		{
			switch (message)
			{
			case MessageName.On_JoystickMoveStart:
				if (EasyJoystick.On_JoystickMoveStart != null)
				{
					EasyJoystick.On_JoystickMoveStart(movingJoystick);
				}
				break;
			case MessageName.On_JoystickMove:
				if (EasyJoystick.On_JoystickMove != null)
				{
					EasyJoystick.On_JoystickMove(movingJoystick);
				}
				break;
			case MessageName.On_JoystickMoveEnd:
				if (EasyJoystick.On_JoystickMoveEnd != null)
				{
					EasyJoystick.On_JoystickMoveEnd(movingJoystick);
				}
				break;
			case MessageName.On_JoystickTouchStart:
				if (EasyJoystick.On_JoystickTouchStart != null)
				{
					EasyJoystick.On_JoystickTouchStart(movingJoystick);
				}
				break;
			case MessageName.On_JoystickTap:
				if (EasyJoystick.On_JoystickTap != null)
				{
					EasyJoystick.On_JoystickTap(movingJoystick);
				}
				break;
			case MessageName.On_JoystickDoubleTap:
				if (EasyJoystick.On_JoystickDoubleTap != null)
				{
					EasyJoystick.On_JoystickDoubleTap(movingJoystick);
				}
				break;
			case MessageName.On_JoystickTouchUp:
				if (EasyJoystick.On_JoystickTouchUp != null)
				{
					EasyJoystick.On_JoystickTouchUp(movingJoystick);
				}
				break;
			}
		}
		else
		{
			if (!useBroadcast)
			{
				return;
			}
			if (receiverGameObject != null)
			{
				switch (messageMode)
				{
				case Broadcast.BroadcastMessage:
					receiverGameObject.BroadcastMessage(message.ToString(), movingJoystick, SendMessageOptions.DontRequireReceiver);
					break;
				case Broadcast.SendMessage:
					receiverGameObject.SendMessage(message.ToString(), movingJoystick, SendMessageOptions.DontRequireReceiver);
					break;
				case Broadcast.SendMessageUpwards:
					receiverGameObject.SendMessageUpwards(message.ToString(), movingJoystick, SendMessageOptions.DontRequireReceiver);
					break;
				}
			}
			else
			{
				UnityEngine.Debug.LogError("Joystick : " + base.gameObject.name + " : you must setup receiver gameobject");
			}
		}
	}

	private void UpdateDirect()
	{
		if (xAxisTransform != null)
		{
			Vector3 influencedAxis = GetInfluencedAxis(xAI);
			DoActionDirect(xAxisTransform, xTI, influencedAxis, joystickValue.x, xAxisCharacterController);
			if (enableXClamp && xTI == PropertiesInfluenced.RotateLocal)
			{
				DoAngleLimitation(xAxisTransform, xAI, clampXMin, clampXMax, startXLocalAngle);
			}
		}
		if (YAxisTransform != null)
		{
			Vector3 influencedAxis2 = GetInfluencedAxis(yAI);
			DoActionDirect(yAxisTransform, yTI, influencedAxis2, joystickValue.y, yAxisCharacterController);
			if (enableYClamp && yTI == PropertiesInfluenced.RotateLocal)
			{
				DoAngleLimitation(yAxisTransform, yAI, clampYMin, clampYMax, startYLocalAngle);
			}
		}
	}

	private void UpdateGravity()
	{
		if (joystickAxis == Vector2.zero)
		{
			if (xAxisCharacterController != null && xAxisGravity > 0f)
			{
				xAxisCharacterController.Move(Vector3.down * xAxisGravity * Time.deltaTime);
			}
			if (yAxisCharacterController != null && yAxisGravity > 0f)
			{
				yAxisCharacterController.Move(Vector3.down * yAxisGravity * Time.deltaTime);
			}
		}
	}

	private Vector3 GetInfluencedAxis(AxisInfluenced axisInfluenced)
	{
		Vector3 result = Vector3.zero;
		switch (axisInfluenced)
		{
		case AxisInfluenced.X:
			result = Vector3.right;
			break;
		case AxisInfluenced.Y:
			result = Vector3.up;
			break;
		case AxisInfluenced.Z:
			result = Vector3.forward;
			break;
		case AxisInfluenced.XYZ:
			result = Vector3.one;
			break;
		}
		return result;
	}

	private void DoActionDirect(Transform axisTransform, PropertiesInfluenced inlfuencedProperty, Vector3 axis, float sensibility, CharacterController charact)
	{
		switch (inlfuencedProperty)
		{
		case PropertiesInfluenced.Rotate:
			axisTransform.Rotate(axis * sensibility * Time.deltaTime, Space.World);
			break;
		case PropertiesInfluenced.RotateLocal:
			axisTransform.Rotate(axis * sensibility * Time.deltaTime, Space.Self);
			break;
		case PropertiesInfluenced.Translate:
		{
			if (charact == null)
			{
				axisTransform.Translate(axis * sensibility * Time.deltaTime, Space.World);
				break;
			}
			Vector3 vector2 = new Vector3(axis.x, axis.y, axis.z);
			vector2.y = 0f - (yAxisGravity + xAxisGravity);
			charact.Move(vector2 * sensibility * Time.deltaTime);
			break;
		}
		case PropertiesInfluenced.TranslateLocal:
		{
			if (charact == null)
			{
				axisTransform.Translate(axis * sensibility * Time.deltaTime, Space.Self);
				break;
			}
			Vector3 vector = charact.transform.TransformDirection(axis) * sensibility;
			vector.y = 0f - (yAxisGravity + xAxisGravity);
			charact.Move(vector * Time.deltaTime);
			break;
		}
		case PropertiesInfluenced.Scale:
			axisTransform.localScale += axis * sensibility * Time.deltaTime;
			break;
		}
	}

	private void DoAngleLimitation(Transform axisTransform, AxisInfluenced axisInfluenced, float clampMin, float clampMax, float startAngle)
	{
		float num = 0f;
		switch (axisInfluenced)
		{
		case AxisInfluenced.X:
			num = axisTransform.localRotation.eulerAngles.x;
			break;
		case AxisInfluenced.Y:
			num = axisTransform.localRotation.eulerAngles.y;
			break;
		case AxisInfluenced.Z:
			num = axisTransform.localRotation.eulerAngles.z;
			break;
		}
		if (num <= 360f && num >= 180f)
		{
			num -= 360f;
		}
		num = Mathf.Clamp(num, 0f - clampMax, clampMin);
		switch (axisInfluenced)
		{
		case AxisInfluenced.X:
			axisTransform.localEulerAngles = new Vector3(num, axisTransform.localEulerAngles.y, axisTransform.localEulerAngles.z);
			break;
		case AxisInfluenced.Y:
			axisTransform.localEulerAngles = new Vector3(axisTransform.localEulerAngles.x, num, axisTransform.localEulerAngles.z);
			break;
		case AxisInfluenced.Z:
			axisTransform.localEulerAngles = new Vector3(axisTransform.localEulerAngles.x, axisTransform.localEulerAngles.y, num);
			break;
		}
	}

	private void DoAutoStabilisation(Transform axisTransform, AxisInfluenced axisInfluenced, float threshold, float speed, float startAngle)
	{
		float num = 0f;
		switch (axisInfluenced)
		{
		case AxisInfluenced.X:
			num = axisTransform.localRotation.eulerAngles.x;
			break;
		case AxisInfluenced.Y:
			num = axisTransform.localRotation.eulerAngles.y;
			break;
		case AxisInfluenced.Z:
			num = axisTransform.localRotation.eulerAngles.z;
			break;
		}
		if (num <= 360f && num >= 180f)
		{
			num -= 360f;
		}
		if (num > startAngle - threshold || num < startAngle + threshold)
		{
			float num2 = 0f;
			Vector3 euler = Vector3.zero;
			if (num > startAngle - threshold)
			{
				num2 = num + speed / 100f * Mathf.Abs(num - startAngle) * Time.deltaTime * -1f;
			}
			if (num < startAngle + threshold)
			{
				num2 = num + speed / 100f * Mathf.Abs(num - startAngle) * Time.deltaTime;
			}
			switch (axisInfluenced)
			{
			case AxisInfluenced.X:
				euler = new Vector3(num2, axisTransform.localRotation.eulerAngles.y, axisTransform.localRotation.eulerAngles.z);
				break;
			case AxisInfluenced.Y:
				euler = new Vector3(axisTransform.localRotation.eulerAngles.x, num2, axisTransform.localRotation.eulerAngles.z);
				break;
			case AxisInfluenced.Z:
				euler = new Vector3(axisTransform.localRotation.eulerAngles.x, axisTransform.localRotation.eulerAngles.y, num2);
				break;
			}
			axisTransform.localRotation = Quaternion.Euler(euler);
		}
	}

	private float GetStartAutoStabAngle(Transform axisTransform, AxisInfluenced axisInfluenced)
	{
		float num = 0f;
		if (axisTransform != null)
		{
			switch (axisInfluenced)
			{
			case AxisInfluenced.X:
				num = axisTransform.localRotation.eulerAngles.x;
				break;
			case AxisInfluenced.Y:
				num = axisTransform.localRotation.eulerAngles.y;
				break;
			case AxisInfluenced.Z:
				num = axisTransform.localRotation.eulerAngles.z;
				break;
			}
			if (num <= 360f && num >= 180f)
			{
				num -= 360f;
			}
		}
		return num;
	}

	private float ComputeDeadZone()
	{
		float num = 0f;
		float num2 = Mathf.Max(joystickTouch.magnitude, 0.1f);
		if (restrictArea)
		{
			return Mathf.Max(num2 - deadZone, 0f) / (zoneRadius - touchSize - deadZone) / num2;
		}
		return Mathf.Max(num2 - deadZone, 0f) / (zoneRadius - deadZone) / num2;
	}

	private void ComputeJoystickAnchor(JoystickAnchor anchor)
	{
		float num = 0f;
		if (!restrictArea)
		{
			num = touchSize;
		}
		switch (anchor)
		{
		case JoystickAnchor.UpperLeft:
			anchorPosition = new Vector2(zoneRadius + num, zoneRadius + num);
			break;
		case JoystickAnchor.UpperCenter:
			anchorPosition = new Vector2(VirtualScreenbm.width / 2f, zoneRadius + num);
			break;
		case JoystickAnchor.UpperRight:
			anchorPosition = new Vector2(VirtualScreenbm.width - zoneRadius - num, zoneRadius + num);
			break;
		case JoystickAnchor.MiddleLeft:
			anchorPosition = new Vector2(zoneRadius + num, VirtualScreenbm.height / 2f);
			break;
		case JoystickAnchor.MiddleCenter:
			anchorPosition = new Vector2(VirtualScreenbm.width / 2f, VirtualScreenbm.height / 2f);
			break;
		case JoystickAnchor.MiddleRight:
			anchorPosition = new Vector2(VirtualScreenbm.width - zoneRadius - num, VirtualScreenbm.height / 2f);
			break;
		case JoystickAnchor.LowerLeft:
			anchorPosition = new Vector2(zoneRadius + num, VirtualScreenbm.height - zoneRadius - num);
			break;
		case JoystickAnchor.LowerCenter:
			anchorPosition = new Vector2(VirtualScreenbm.width / 2f, VirtualScreenbm.height - zoneRadius - num);
			break;
		case JoystickAnchor.LowerRight:
			anchorPosition = new Vector2(VirtualScreenbm.width - zoneRadius - num, VirtualScreenbm.height - zoneRadius - num);
			break;
		case JoystickAnchor.None:
			anchorPosition = Vector2.zero;
			break;
		}
		areaRect = new Rect(anchorPosition.x + joystickCenter.x - zoneRadius, anchorPosition.y + joystickCenter.y - zoneRadius, zoneRadius * 2f, zoneRadius * 2f);
		deadRect = new Rect(anchorPosition.x + joystickCenter.x - deadZone, anchorPosition.y + joystickCenter.y - deadZone, deadZone * 2f, deadZone * 2f);
	}

	private void On_TouchStart(Gesturebm gesturebm)
	{
		if (!visible || ((gesturebm.isHoverReservedArea || !dynamicJoystick) && dynamicJoystick) || !isActivated)
		{
			return;
		}
		if (!dynamicJoystick)
		{
			Vector2 vector = new Vector2((anchorPosition.x + joystickCenter.x) * VirtualScreenbm.xRatio, (VirtualScreenbm.height - anchorPosition.y - joystickCenter.y) * VirtualScreenbm.yRatio);
			if ((gesturebm.position - vector).sqrMagnitude < zoneRadius * VirtualScreenbm.xRatio * (zoneRadius * VirtualScreenbm.xRatio))
			{
				joystickIndex = gesturebm.fingerIndex;
				CreateEvent(MessageName.On_JoystickTouchStart);
			}
		}
		else
		{
			if (virtualJoystick)
			{
				return;
			}
			switch (area)
			{
			case DynamicArea.FullScreen:
				virtualJoystick = true;
				break;
			case DynamicArea.Bottom:
				if (gesturebm.position.y < (float)(Screen.height / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.Top:
				if (gesturebm.position.y > (float)(Screen.height / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.Right:
				if (gesturebm.position.x > (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.Left:
				if (gesturebm.position.x < (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.TopRight:
				if (gesturebm.position.y > (float)(Screen.height / 2) && gesturebm.position.x > (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.TopLeft:
				if (gesturebm.position.y > (float)(Screen.height / 2) && gesturebm.position.x < (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.BottomRight:
				if (gesturebm.position.y < (float)(Screen.height / 2) && gesturebm.position.x > (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			case DynamicArea.BottomLeft:
				if (gesturebm.position.y < (float)(Screen.height / 2) && gesturebm.position.x < (float)(Screen.width / 2))
				{
					virtualJoystick = true;
				}
				break;
			}
			if (virtualJoystick)
			{
				joystickCenter = new Vector2(gesturebm.position.x / VirtualScreenbm.xRatio, VirtualScreenbm.height - gesturebm.position.y / VirtualScreenbm.yRatio);
				JoyAnchor = JoystickAnchor.None;
				joystickIndex = gesturebm.fingerIndex;
			}
		}
	}

	private void On_SimpleTap(Gesturebm gesturebm)
	{
		if (visible && ((!gesturebm.isHoverReservedArea && dynamicJoystick) || !dynamicJoystick) && isActivated && gesturebm.fingerIndex == joystickIndex)
		{
			CreateEvent(MessageName.On_JoystickTap);
		}
	}

	private void On_DoubleTap(Gesturebm gesturebm)
	{
		if (visible && ((!gesturebm.isHoverReservedArea && dynamicJoystick) || !dynamicJoystick) && isActivated && gesturebm.fingerIndex == joystickIndex)
		{
			CreateEvent(MessageName.On_JoystickDoubleTap);
		}
	}

	private void On_TouchDown(Gesturebm gesturebm)
	{
		if (!visible || ((gesturebm.isHoverReservedArea || !dynamicJoystick) && dynamicJoystick) || !isActivated)
		{
			return;
		}
		Vector2 vector = new Vector2((anchorPosition.x + joystickCenter.x) * VirtualScreenbm.xRatio, (VirtualScreenbm.height - (anchorPosition.y + joystickCenter.y)) * VirtualScreenbm.yRatio);
		if (gesturebm.fingerIndex != joystickIndex)
		{
			return;
		}
		if (((gesturebm.position - vector).sqrMagnitude < zoneRadius * VirtualScreenbm.xRatio * (zoneRadius * VirtualScreenbm.xRatio) && resetFingerExit) || !resetFingerExit)
		{
			joystickTouch = new Vector2(gesturebm.position.x, gesturebm.position.y) - vector;
			joystickTouch = new Vector2(joystickTouch.x / VirtualScreenbm.xRatio, joystickTouch.y / VirtualScreenbm.yRatio);
			if (!enableXaxis)
			{
				joystickTouch.x = 0f;
			}
			if (!enableYaxis)
			{
				joystickTouch.y = 0f;
			}
			if ((joystickTouch / (zoneRadius - touchSizeCoef)).sqrMagnitude > 1f)
			{
				joystickTouch.Normalize();
				joystickTouch *= zoneRadius - touchSizeCoef;
			}
		}
		else
		{
			On_TouchUp(gesturebm);
		}
	}

	private void On_TouchUp(Gesturebm gesturebm)
	{
		if (visible && gesturebm.fingerIndex == joystickIndex)
		{
			joystickIndex = -1;
			if (dynamicJoystick)
			{
				virtualJoystick = false;
			}
			CreateEvent(MessageName.On_JoystickTouchUp);
		}
	}

	public void On_Manual(Vector2 movement)
	{
		if (!isActivated)
		{
			return;
		}
		if (movement != Vector2.zero)
		{
			if (!virtualJoystick)
			{
				virtualJoystick = true;
				CreateEvent(MessageName.On_JoystickTouchStart);
			}
			joystickIndex = 0;
			joystickTouch.x = movement.x * (areaRect.width / 2f);
			joystickTouch.y = movement.y * (areaRect.height / 2f);
		}
		else if (virtualJoystick)
		{
			virtualJoystick = false;
			joystickIndex = -1;
			CreateEvent(MessageName.On_JoystickTouchUp);
		}
	}
}
