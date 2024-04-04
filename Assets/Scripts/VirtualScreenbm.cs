using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class VirtualScreenbm : MonoSingleton<VirtualScreenbm>
{
    public delegate void On_ScreenResizeHandler();

    public enum ScreenResolution
    {
        IPhoneTall = 0,
        IPhoneWide = 1,
        IPhone4GTall = 2,
        IPhone4GWide = 3,
        IPadTall = 4,
        IPadWide = 5
    }

    [CompilerGenerated] [DebuggerBrowsable(DebuggerBrowsableState.Never)]
    private static On_ScreenResizeHandler m_On_ScreenResize;

    public static float width = 1024f;

    public static float height = 768f;

    public static float xRatio = 1f;

    public static float yRatio = 1f;

    [FormerlySerializedAs("virtualWidth")] public float virtualWidthbm = 1024f;

    [FormerlySerializedAs("virtualHeight")]
    public float virtualHeightbm = 768f;

    private float oldRealHeightbm;

    private float oldRealWidthbm;

    private float realHeightbm;

    private float realWidthbm;

    private void Awake()
    {
        realWidthbm = oldRealWidthbm = Screen.width;
        realHeightbm = oldRealHeightbm = Screen.height;
        ComputeScreenbm();
    }

    private void Update()
    {
        realWidthbm = Screen.width;
        realHeightbm = Screen.height;
        if (realWidthbm != oldRealWidthbm || realHeightbm != oldRealHeightbm)
        {
            ComputeScreenbm();
            if (On_ScreenResize != null) On_ScreenResize();
        }

        oldRealWidthbm = realWidthbm;
        oldRealHeightbm = realHeightbm;
    }

    public static event On_ScreenResizeHandler On_ScreenResize;

    public void ComputeScreenbm()
    {
        width = virtualWidthbm;
        height = virtualHeightbm;
        xRatio = 1f;
        yRatio = 1f;
        var num = 0f;
        var num2 = 0f;
        if (Screen.width > Screen.height)
        {
            num = Screen.width / (float)Screen.height;
            num2 = width;
        }
        else
        {
            num = Screen.height / (float)Screen.width;
            num2 = height;
        }

        var num3 = 0f;
        num3 = num2 / num;
        if (Screen.width > Screen.height)
        {
            height = num3;
            xRatio = Screen.width / width;
            yRatio = Screen.height / height;
        }
        else
        {
            width = num3;
            xRatio = Screen.width / width;
            yRatio = Screen.height / height;
        }
    }

    public static void ComputeVirtualScreenbm()
    {
        instance.ComputeScreenbm();
    }

    public static void SetGuiScaleMatrixbm()
    {
        GUI.matrix = Matrix4x4.Scale(new Vector3(xRatio, yRatio, 1f));
    }

    public static Rect GetRealRectbm(Rect rect)
    {
        return new Rect(rect.x * xRatio, rect.y * yRatio, rect.width * xRatio, rect.height * yRatio);
    }
}