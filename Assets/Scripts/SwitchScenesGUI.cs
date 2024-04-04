using ScreenFaderComponents.Actions;
using UnityEngine;

internal class SwitchScenesGUI : MonoBehaviour
{
    private void OnGUI()
    {
        GUI.depth = -3;
        GUI.Window(0, new Rect(0f, 0f, 220f, 140f), DoWindowbm, "Screen Fader Types");
    }

    private void DoWindowbm(int id)
    {
        if (GUI.Button(new Rect(10f, 30f, 200f, 30f), "Default Fading"))
            Faderbm.Instance.FadeIn().StartAction(new LoadSceneAction(), 1);
        if (GUI.Button(new Rect(10f, 65f, 200f, 30f), "Squares Effect"))
            Faderbm.Instance.FadeIn().StartAction(new LoadSceneAction(), 2);
        if (GUI.Button(new Rect(10f, 100f, 200f, 30f), "Stripes Effect"))
            Faderbm.Instance.FadeIn().StartAction(new LoadSceneAction(), 3);
    }
}