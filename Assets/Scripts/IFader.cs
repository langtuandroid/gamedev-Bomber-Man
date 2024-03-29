using System;
using System.Collections;
using ScreenFaderComponents;
using ScreenFaderComponents.Actions;
using ScreenFaderComponents.Enumerators;
using ScreenFaderComponents.Events;
using UnityEngine;
using UnityEngine.UI;

public interface IFader
{
	FadeState State { get; }

	event EventHandler<FadeEventArgs> FadeStart;

	event EventHandler<FadeEventArgs> FadeFinish;

	IFader FadeIn(float time = 0.4f);

	IFader FadeIn(GameObject obj, float time = 1f);

	IFader FadeIn(Image img, float time = 1f);

	IFader FadeOut(float time = 0.2f);

	IFader FadeOut(GameObject obj, float time = 1f);

	IFader FadeOut(Image img, float time = 1f);

	IFader Pause(float time = 0.1f);

	IFader Flash(float inTime = 0.075f, float outTime = 0.15f);

	IFader StartAction(IAction action);

	IFader StartAction(IParametrizedAction action, params object[] args);

	void StopAllFadings();

	IFader StartCoroutine(MonoBehaviour component, string methodName);

	IFader StartCoroutine(MonoBehaviour component, string methodName, object value);

	IFader StartCoroutine(MonoBehaviour component, IEnumerator routine);

	IFader LoadLevel(int index);

	IFader LoadLevel(string name);

	IFader SetColor(Color color);

	IFader Fade(FadeDirection fadeDirection, float time = 1f);

	void AddTask(FaderTask task);
}
