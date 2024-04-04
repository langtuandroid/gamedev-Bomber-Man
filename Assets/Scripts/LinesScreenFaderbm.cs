using System.Collections.Generic;
using UnityEngine;

public class LinesScreenFaderbm : Faderbm
{
    public enum Direction
    {
        IN_FROM_LEFT = 0,
        IN_FROM_RIGHT = 1,
        IN_UP_DOWN = 2
    }

    public int numberOfStripes = 10;

    public int space = 10;

    public Direction direction = Direction.IN_FROM_RIGHT;

    private Color last_color = Color.black;

    private int last_numberOfStripes = 10;

    private AnimRect[] rects;

    private readonly TextureCollection textures = new();

    protected override void Update()
    {
        if ((color != last_color) | (numberOfStripes != last_numberOfStripes)) Init();
        base.Update();
    }

    public void AddTextures(Texture[] images)
    {
        for (var i = 0; i < images.Length; i++) textures[i] = images[i];
    }

    protected override void Init()
    {
        base.Init();
        textures.SetDefaultTexture(GetTextureFromColor(color), numberOfStripes);
        rects = new AnimRect[numberOfStripes];
        var num = Screen.width / numberOfStripes;
        for (var i = 0; i < rects.Length; i++)
        {
            var extra = 0;
            if (i == rects.Length - 1) extra = Screen.width - num * rects.Length;
            rects[i] = CreateRect(num, i, extra);
        }

        last_color = color;
        last_numberOfStripes = numberOfStripes;
    }

    private AnimRect CreateRect(int rectW, int index, int extra)
    {
        var startOffset = GetStartOffset(direction, rectW, index);
        var finalOffset = GetFinalOffset(direction, rectW, index);
        var num = direction != 0 ? index : rects.Length - index;
        return new AnimRect(new Rect(rectW * num + startOffset.x, startOffset.y, rectW + extra, Screen.height),
            new Rect(rectW * num + finalOffset.x, finalOffset.y, rectW + extra, Screen.height));
    }

    protected override void DrawOnGUI()
    {
        for (var i = 0; i < rects.Length; i++)
        {
            var time = fadeBalance;
            GUI.DrawTexture(rects[i].GetRect(time), textures[i]);
        }
    }

    protected Vector2 GetStartOffset(Direction direction, int lineWidth, int index)
    {
        var result = Vector2.zero;
        var num = (int)(lineWidth * index * Mathf.Sqrt(index * space));
        switch (direction)
        {
            case Direction.IN_FROM_LEFT:
                result = new Vector2(-Screen.width - lineWidth - num - 1, 0f);
                break;
            case Direction.IN_FROM_RIGHT:
                result = new Vector2(Screen.width + num + 1, 0f);
                break;
            case Direction.IN_UP_DOWN:
                result = new Vector2(0f, (Screen.height + 1) * (index % 2 == 1 ? 1 : -1));
                break;
            default:
                result = Vector2.zero;
                break;
        }

        return result;
    }

    protected Vector2 GetFinalOffset(Direction direction, int lineWidth, int index)
    {
        var zero = Vector2.zero;
        switch (direction)
        {
            case Direction.IN_FROM_LEFT:
                zero = new Vector2(-lineWidth, 0f);
                break;
            default:
                zero = Vector2.zero;
                break;
        }

        return zero;
    }

    private struct AnimRect
    {
        private readonly Rect rectStart;

        private readonly Rect rectFinal;

        public AnimRect(Rect rectStart, Rect rectFinal)
        {
            this.rectStart = rectStart;
            this.rectFinal = rectFinal;
        }

        public Rect GetRect(float time)
        {
            if (time >= 1f) return rectFinal;
            if (time > 0f) return GetRectByT(time);
            return rectStart;
        }

        private Rect GetRectByT(float time)
        {
            return ScreenFaderUtilitybm.Lerp(rectStart, rectFinal, time);
        }
    }

    private class TextureCollection
    {
        private readonly Dictionary<int, Texture> textures = new();

        public Texture this[int index]
        {
            get => textures[index];
            set
            {
                if (textures.ContainsKey(index))
                    textures[index] = value;
                else
                    textures.Add(index, value);
            }
        }

        public void SetDefaultTexture(Texture defaultTexture, int count)
        {
            for (var i = 0; i < count; i++)
                if (!textures.ContainsKey(i))
                    textures.Add(i, defaultTexture);
        }
    }
}