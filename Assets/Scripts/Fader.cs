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

public class Fader : MonoBehaviour, IFader
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

	protected FaderTask currentTask;

	protected Queue<FaderTask> tasks = new Queue<FaderTask>();

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private EventHandler<FadeEventArgs> m_FadeStart;

	[CompilerGenerated]
	[DebuggerBrowsable(DebuggerBrowsableState.Never)]
	private EventHandler<FadeEventArgs> m_FadeFinish;

	protected static Fader instance;

	public FadeState State
	{
		get
		{
			return currentState;
		}
	}

	public static IFader Instance
	{
		get
		{
			if (instance == null)
			{
				instance = UnityEngine.Object.FindObjectOfType(typeof(Fader)) as Fader;
				if (instance == null)
				{
					instance = FaderFactory.CreateDefaultFader(null);
				}
			}
			return instance;
		}
	}

	public event EventHandler<FadeEventArgs> FadeStart
	;

	public event EventHandler<FadeEventArgs> FadeFinish
;

	public IFader Fade(FadeDirection fadeDirection, float time = 1f)
	{
		AddTask(new FaderTask
		{
			State = ((fadeDirection != 0) ? FadeState.Out : FadeState.In),
			Time = time
		});
		return Instance;
	}

	public IFader FadeIn(float time = 0.2f)
	{
		AddTask(new FaderTask
		{
			State = FadeState.In,
			Time = time
		});
		return Instance;
	}

	public IFader FadeIn(GameObject obj, float time = 0.01f)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			action = new GameObjectFadingAction(FadeDirection.In, obj, time)
		});
		return Instance;
	}

	public IFader FadeIn(Image img, float time = 0.01f)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			action = new CanvasImageFadingAction(FadeDirection.In, img, time)
		});
		return Instance;
	}

	public IFader FadeOut(float time = 0.2f)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Out,
			Time = time
		});
		return Instance;
	}

	public IFader FadeOut(GameObject obj, float time = 0.2f)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			action = new GameObjectFadingAction(FadeDirection.Out, obj, time)
		});
		return Instance;
	}

	public IFader FadeOut(Image img, float time = 0.2f)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			action = new CanvasImageFadingAction(FadeDirection.Out, img, time)
		});
		return Instance;
	}

	public IFader Pause(float pause = 1f)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			PostDelay = pause
		});
		return Instance;
	}

	public IFader StartAction(IAction action)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			action = action
		});
		return Instance;
	}

	public IFader SetColor(Color color)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			pAction = new ChangeColorAction(),
			pActionParameters = new object[2] { color, instance }
		});
		return Instance;
	}

	public IFader LoadLevel(int index)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			pAction = new LoadSceneAction(),
			pActionParameters = new object[1] { index }
		});
		return Instance;
	}

	public IFader LoadLevel(string name)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			pAction = new LoadSceneAction(),
			pActionParameters = new object[1] { name }
		});
		return Instance;
	}

	public IFader StartAction(IParametrizedAction action, params object[] args)
	{
		AddTask(new FaderTask
		{
			State = FadeState.Stop,
			pAction = action,
			pActionParameters = args
		});
		return Instance;
	}

	public IFader Flash(float inTime = 0.075f, float outTime = 0.15f)
	{
		AddTask(new FaderTask
		{
			State = FadeState.In,
			Time = inTime,
			PostDelay = 0.1f
		});
		AddTask(new FaderTask
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

	public IFader StartCoroutine(MonoBehaviour component, string methodName)
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

	public IFader StartCoroutine(MonoBehaviour component, string methodName, object value)
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

	public IFader StartCoroutine(MonoBehaviour component, IEnumerator routine)
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

	public static IFader Fade(FadeDirection fadeDirection, float time, IAction action, float postDelay)
	{
		Instance.AddTask(new FaderTask
		{
			State = ((fadeDirection != 0) ? FadeState.Out : FadeState.In),
			Time = time,
			PostDelay = postDelay,
			action = action
		});
		return Instance;
	}

	public void AddTask(FaderTask task)
	{
		tasks.Enqueue(task);
	}

	protected void StartTask()
	{
		if (currentTask == null && tasks.Count > 0)
		{
			currentTask = tasks.Dequeue();
			if (currentTask.action != null)
			{
				currentTask.action.Completed = false;
			}
			if (currentTask.pAction != null)
			{
				currentTask.pAction.Completed = false;
			}
			OnFadeStart(new FadeEventArgs
			{
				Direction = ((currentTask.State != 0) ? FadeDirection.Out : FadeDirection.In)
			});
		}
	}

	protected void ExecuteTaskAction()
	{
		if (currentTask.action != null && !currentTask.action.Completed)
		{
			currentTask.action.Execute();
		}
		if (currentTask.pAction != null && !currentTask.pAction.Completed)
		{
			currentTask.pAction.Execute(currentTask.pActionParameters);
		}
	}

	protected void FinishTask()
	{
		if (currentTask == null)
		{
			return;
		}
		if ((currentTask.action == null) & (currentTask.pAction == null))
		{
			OnFadeFinish(new FadeEventArgs
			{
				Direction = ((currentTask.State != 0) ? FadeDirection.Out : FadeDirection.In)
			});
			currentTask = null;
		}
		else if (currentTask.action != null)
		{
			if (currentTask.action.Completed)
			{
				OnFadeFinish(new FadeEventArgs
				{
					Direction = ((currentTask.State != 0) ? FadeDirection.Out : FadeDirection.In)
				});
				currentTask = null;
			}
		}
		else if (currentTask.pAction != null && currentTask.pAction.Completed)
		{
			OnFadeFinish(new FadeEventArgs
			{
				Direction = ((currentTask.State != 0) ? FadeDirection.Out : FadeDirection.In)
			});
			currentTask = null;
		}
	}

	protected virtual void OnFadeStart(FadeEventArgs e)
	{
		if (this.FadeStart != null)
		{
			this.FadeStart(this, e);
		}
	}

	protected virtual void OnFadeFinish(FadeEventArgs e)
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
		if (currentTask == null && tasks.Count > 0)
		{
			StartTask();
		}
		if (currentTask == null)
		{
			return;
		}
		ExecuteTaskAction();
		switch (currentTask.State)
		{
		case FadeState.In:
			fadeBalance += Time.deltaTime / currentTask.Time;
			break;
		case FadeState.Out:
			fadeBalance -= Time.deltaTime / currentTask.Time;
			break;
		case FadeState.Stop:
			fadeBalance = fadeBalance;
			currentTask.PostDelay -= Time.deltaTime;
			if (currentTask.PostDelay < 0f)
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
		FaderFactoryBase factory = new ImageFaderFactory(texture);
		CreateFaderInstance(factory);
	}

	public static void SetupAsStripesFader(int stripes)
	{
		SetupAsStripesFader(stripes, StripeScreenFader.Direction.HORIZONTAL_IN);
	}

	public static void SetupAsStripesFader(int stripes, StripeScreenFader.Direction direction)
	{
		FaderFactoryBase factory = new StripesFaderFactory(stripes, direction);
		CreateFaderInstance(factory);
	}

	public static void SetupAsSquaredFader(int columns)
	{
		SetupAsSquaredFader(columns, SquaredScreenFader.Direction.DIAGONAL_RIGHT_UP);
	}

	public static void SetupAsSquaredFader(int columns, SquaredScreenFader.Direction direction)
	{
		FaderFactoryBase factory = new SquaredFaderFactory(columns, direction);
		CreateFaderInstance(factory);
	}

	public static void SetupAsLinesFader(int stripes)
	{
		SetupAsLinesFader(stripes, LinesScreenFader.Direction.IN_FROM_RIGHT);
	}

	public static void SetupAsLinesFader(int stripes, LinesScreenFader.Direction direction)
	{
		FaderFactoryBase faderFactoryBase = new LinesFaderFactory(stripes, direction);
		instance = faderFactoryBase.CreateFader((!(instance != null)) ? null : instance.gameObject);
	}

	public static void SetupAsLinesFader(LinesScreenFader.Direction direction, params Texture[] images)
	{
		FaderFactoryBase faderFactoryBase = new LinesFaderFactory(images.Length, direction, images);
		instance = faderFactoryBase.CreateFader((!(instance != null)) ? null : instance.gameObject);
	}

	public static void SetupAsDefaultFader()
	{
		FaderFactoryBase factory = new DefaultFaderFactory();
		CreateFaderInstance(factory);
	}

	private static void CreateFaderInstance(FaderFactoryBase factory)
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
