using System;
using System.Collections;
using ScreenFaderComponents;
using ScreenFaderComponents.Actions;
using ScreenFaderComponents.Enumerators;
using ScreenFaderComponents.Events;
using UnityEngine;
using UnityEngine.UI;

public interface IFaderbm
{
    FadeState State { get; }

    event EventHandler<FadeEventArgsbm> FadeStart;

    event EventHandler<FadeEventArgsbm> FadeFinish;

    IFaderbm FadeIn(float time = 0.4f);

    IFaderbm FadeIn(GameObject obj, float time = 1f);

    IFaderbm FadeIn(Image img, float time = 1f);

    IFaderbm FadeOut(float time = 0.2f);

    IFaderbm FadeOut(GameObject obj, float time = 1f);

    IFaderbm FadeOut(Image img, float time = 1f);

    IFaderbm Pause(float time = 0.1f);

    IFaderbm Flash(float inTime = 0.075f, float outTime = 0.15f);

    IFaderbm StartAction(IAction action);

    IFaderbm StartAction(IParametrizedAction action, params object[] args);

    void StopAllFadings();

    IFaderbm StartCoroutine(MonoBehaviour component, string methodName);

    IFaderbm StartCoroutine(MonoBehaviour component, string methodName, object value);

    IFaderbm StartCoroutine(MonoBehaviour component, IEnumerator routine);

    IFaderbm LoadLevel(int index);

    IFaderbm LoadLevel(string name);

    IFaderbm SetColor(Color color);

    IFaderbm Fade(FadeDirection fadeDirection, float time = 1f);

    void AddTask(FaderTaskbm taskbm);
}