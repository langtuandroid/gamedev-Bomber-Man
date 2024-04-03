using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using ScreenFaderComponents;
using ScreenFaderComponents.Actions;
using ScreenFaderComponents.Enumerators;
using ScreenFaderComponents.Events;
using UnityEngine;
using UnityEngine.UI;

public class Faderbm : MonoBehaviour, IFaderbm
{
	public Color color = Color.black;

	[SerializeField]
	private FadeDirection defaultState = FadeDirection.Out;

	[Range(0f, 10f)]
	public float fadeInDelay;

	[Range(0f, 10f)]
	public float fadeOutDelay;

	[SerializeField]
	[Range(-5f, 5f)]
	private int GUIdepth = -2;

	protected float fadeBalance;

	protected FadeState currentState = FadeState.OutEnd;

	protected FaderTaskbm CurrentTaskbm;

	protected Queue<FaderTaskbm> tasks = new Queue<FaderTaskbm>();

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private EventHandler<FadeEventArgsbm> m_FadeStart;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private EventHandler<FadeEventArgsbm> m_FadeFinish;

	protected static Faderbm instance;

	public FadeState State
	{
		get
		{
			return currentState;
		}
	}

	public static IFaderbm Instance
	{
		get
		{
			if (instance == null)
			{
				instance = UnityEngine.Object.FindObjectOfType(typeof(Faderbm)) as Faderbm;
				if (instance == null)
				{
					instance = FaderFactorybm.CreateDefaultFader(null);
				}
			}
			return instance;
		}
	}

	public event EventHandler<FadeEventArgsbm> FadeStart
	;

	public event EventHandler<FadeEventArgsbm> FadeFinish
;

	public IFaderbm Fade(FadeDirection fadeDirection, float time = 1f)
	{
		AddTask(new FaderTaskbm
		{
			State = ((fadeDirection != 0) ? FadeState.Out : FadeState.In),
			Time = time
		});
		return Instance;
	}

	public IFaderbm FadeIn(float time = 0.2f)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.In,
			Time = time
		});
		return Instance;
	}

	public IFaderbm FadeIn(GameObject obj, float time = 0.01f)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			action = new GameObjectFadingAction(FadeDirection.In, obj, time)
		});
		return Instance;
	}

	public IFaderbm FadeIn(Image img, float time = 0.01f)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			action = new CanvasImageFadingAction(FadeDirection.In, img, time)
		});
		return Instance;
	}

	public IFaderbm FadeOut(float time = 0.2f)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Out,
			Time = time
		});
		return Instance;
	}

	public IFaderbm FadeOut(GameObject obj, float time = 0.2f)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			action = new GameObjectFadingAction(FadeDirection.Out, obj, time)
		});
		return Instance;
	}

	public IFaderbm FadeOut(Image img, float time = 0.2f)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			action = new CanvasImageFadingAction(FadeDirection.Out, img, time)
		});
		return Instance;
	}

	public IFaderbm Pause(float pause = 1f)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			PostDelay = pause
		});
		return Instance;
	}

	public IFaderbm StartAction(IAction action)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			action = action
		});
		return Instance;
	}

	public IFaderbm SetColor(Color color)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			pAction = new ChangeColorAction(),
			pActionParameters = new object[2] { color, instance }
		});
		return Instance;
	}

	public IFaderbm LoadLevel(int index)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			pAction = new LoadSceneAction(),
			pActionParameters = new object[1] { index }
		});
		return Instance;
	}

	public IFaderbm LoadLevel(string name)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			pAction = new LoadSceneAction(),
			pActionParameters = new object[1] { name }
		});
		return Instance;
	}

	public IFaderbm StartAction(IParametrizedAction action, params object[] args)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.Stop,
			pAction = action,
			pActionParameters = args
		});
		return Instance;
	}

	public IFaderbm Flash(float inTime = 0.075f, float outTime = 0.15f)
	{
		AddTask(new FaderTaskbm
		{
			State = FadeState.In,
			Time = inTime,
			PostDelay = 0.1f
		});
		AddTask(new FaderTaskbm
		{
			State = FadeState.Out,
			Time = outTime
		});
		return Instance;
	}

	public void StopAllFadings()
	{
		tasks.Clear();
	}

	public IFaderbm StartCoroutine(MonoBehaviour component, string methodName)
	{
		if (component == null)
		{
			throw new ArgumentNullException();
		}
		if (string.IsNullOrEmpty(methodName))
		{
			throw new ArgumentNullException();
		}
		return StartAction(new CoroutineAction(), component, methodName);
	}

	public IFaderbm StartCoroutine(MonoBehaviour component, string methodName, object value)
	{
		if (component == null)
		{
			throw new ArgumentNullException();
		}
		if (string.IsNullOrEmpty(methodName))
		{
			throw new ArgumentNullException();
		}
		return StartAction(new ParametrizedCoroutineAction(), component, methodName, value);
	}

	public IFaderbm StartCoroutine(MonoBehaviour component, IEnumerator routine)
	{
		if (component == null)
		{
			throw new ArgumentNullException();
		}
		if (routine == null)
		{
			throw new ArgumentNullException();
		}
		return StartAction(new EnumeratorCoroutineAction(), component, routine);
	}

	public static IFaderbm Fade(FadeDirection fadeDirection, float time, IAction action, float postDelay)
	{
		Instance.AddTask(new FaderTaskbm
		{
			State = ((fadeDirection != 0) ? FadeState.Out : FadeState.In),
			Time = time,
			PostDelay = postDelay,
			action = action
		});
		return Instance;
	}

	public void AddTask(FaderTaskbm taskbm)
	{
		tasks.Enqueue(taskbm);
	}

	protected void StartTask()
	{
		if (CurrentTaskbm == null && tasks.Count > 0)
		{
			CurrentTaskbm = tasks.Dequeue();
			if (CurrentTaskbm.action != null)
			{
				CurrentTaskbm.action.Completed = false;
			}
			if (CurrentTaskbm.pAction != null)
			{
				CurrentTaskbm.pAction.Completed = false;
			}
			OnFadeStart(new FadeEventArgsbm
			{
				Direction = ((CurrentTaskbm.State != 0) ? FadeDirection.Out : FadeDirection.In)
			});
		}
	}

	protected void ExecuteTaskAction()
	{
		if (CurrentTaskbm.action != null && !CurrentTaskbm.action.Completed)
		{
			CurrentTaskbm.action.Execute();
		}
		if (CurrentTaskbm.pAction != null && !CurrentTaskbm.pAction.Completed)
		{
			CurrentTaskbm.pAction.Execute(CurrentTaskbm.pActionParameters);
		}
	}

	protected void FinishTask()
	{
		if (CurrentTaskbm == null)
		{
			return;
		}
		if ((CurrentTaskbm.action == null) & (CurrentTaskbm.pAction == null))
		{
			OnFadeFinish(new FadeEventArgsbm
			{
				Direction = ((CurrentTaskbm.State != 0) ? FadeDirection.Out : FadeDirection.In)
			});
			CurrentTaskbm = null;
		}
		else if (CurrentTaskbm.action != null)
		{
			if (CurrentTaskbm.action.Completed)
			{
				OnFadeFinish(new FadeEventArgsbm
				{
					Direction = ((CurrentTaskbm.State != 0) ? FadeDirection.Out : FadeDirection.In)
				});
				CurrentTaskbm = null;
			}
		}
		else if (CurrentTaskbm.pAction != null && CurrentTaskbm.pAction.Completed)
		{
			OnFadeFinish(new FadeEventArgsbm
			{
				Direction = ((CurrentTaskbm.State != 0) ? FadeDirection.Out : FadeDirection.In)
			});
			CurrentTaskbm = null;
		}
	}

	protected virtual void OnFadeStart(FadeEventArgsbm e)
	{
		if (this.FadeStart != null)
		{
			this.FadeStart(this, e);
		}
	}

	protected virtual void OnFadeFinish(FadeEventArgsbm e)
	{
		if (this.FadeFinish != null)
		{
			this.FadeFinish(this, e);
		}
	}

	protected void Awake()
	{
		if (defaultState == FadeDirection.Out)
		{
			fadeBalance = 0f;
		}
		else
		{
			fadeBalance = 1f;
		}
		Init();
		UnityEngine.Object.DontDestroyOnLoad(GetComponent<Transform>().gameObject);
	}

	protected void OnGUI()
	{
		GUI.depth = GUIdepth;
		DrawOnGUI();
	}

	protected virtual void Update()
	{
		if (CurrentTaskbm == null && tasks.Count > 0)
		{
			StartTask();
		}
		if (CurrentTaskbm == null)
		{
			return;
		}
		ExecuteTaskAction();
		switch (CurrentTaskbm.State)
		{
		case FadeState.In:
			fadeBalance += Time.deltaTime / CurrentTaskbm.Time;
			break;
		case FadeState.Out:
			fadeBalance -= Time.deltaTime / CurrentTaskbm.Time;
			break;
		case FadeState.Stop:
			fadeBalance = fadeBalance;
			CurrentTaskbm.PostDelay -= Time.deltaTime;
			if (CurrentTaskbm.PostDelay < 0f)
			{
				FinishTask();
			}
			break;
		case FadeState.InEnd:
			fadeBalance = 1f + fadeOutDelay;
			break;
		case FadeState.OutEnd:
			fadeBalance = 0f - fadeInDelay;
			break;
		}
		if (fadeBalance > 1f)
		{
			fadeBalance = 1f;
			currentState = FadeState.InEnd;
			FinishTask();
		}
		if (fadeBalance < 0f)
		{
			fadeBalance = 0f;
			currentState = FadeState.OutEnd;
			FinishTask();
		}
	}

	protected virtual void Init()
	{
		instance = this;
	}

	protected virtual void DrawOnGUI()
	{
	}

	public static void SetupAsImageFader(Texture texture)
	{
		FaderFactoryBasebm factory = new ImageFaderFactorybm(texture);
		CreateFaderInstance(factory);
	}

	public static void SetupAsStripesFader(int stripes)
	{
		SetupAsStripesFader(stripes, StripeScreenFaderbm.Direction.HORIZONTAL_IN);
	}

	public static void SetupAsStripesFader(int stripes, StripeScreenFaderbm.Direction direction)
	{
		FaderFactoryBasebm factory = new StripesFaderFactorybm(stripes, direction);
		CreateFaderInstance(factory);
	}

	public static void SetupAsSquaredFader(int columns)
	{
		SetupAsSquaredFader(columns, SquaredScreenFaderbm.Direction.DIAGONAL_RIGHT_UP);
	}

	public static void SetupAsSquaredFader(int columns, SquaredScreenFaderbm.Direction direction)
	{
		FaderFactoryBasebm factory = new SquaredFaderFactorybm(columns, direction);
		CreateFaderInstance(factory);
	}

	public static void SetupAsLinesFader(int stripes)
	{
		SetupAsLinesFader(stripes, LinesScreenFaderbm.Direction.IN_FROM_RIGHT);
	}

	public static void SetupAsLinesFader(int stripes, LinesScreenFaderbm.Direction direction)
	{
		FaderFactoryBasebm faderFactoryBasebm = new LinesFaderFactorybm(stripes, direction);
		instance = faderFactoryBasebm.CreateFader((!(instance != null)) ? null : instance.gameObject);
	}

	public static void SetupAsLinesFader(LinesScreenFaderbm.Direction direction, params Texture[] images)
	{
		FaderFactoryBasebm faderFactoryBasebm = new LinesFaderFactorybm(images.Length, direction, images);
		instance = faderFactoryBasebm.CreateFader((!(instance != null)) ? null : instance.gameObject);
	}

	public static void SetupAsDefaultFader()
	{
		FaderFactoryBasebm factory = new DefaultFaderFactorybm();
		CreateFaderInstance(factory);
	}

	private static void CreateFaderInstance(FaderFactoryBasebm factory)
	{
		instance = factory.CreateFader((!(instance != null)) ? null : instance.gameObject);
	}

	protected Texture GetTextureFromColor(Color color)
	{
		Texture2D texture2D = new Texture2D(1, 1);
		texture2D.SetPixel(0, 0, color);
		texture2D.Apply();
		return texture2D;
	}

	protected float GetLinearT(int i, int from)
	{
		return fadeBalance / ((float)i / (float)from);
	}

	protected float GetNonLinearT(int i, int from)
	{
		return fadeBalance * (float)from - fadeBalance * (float)i;
	}
}
