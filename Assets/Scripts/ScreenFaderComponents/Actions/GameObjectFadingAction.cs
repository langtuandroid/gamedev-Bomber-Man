using System;
using ScreenFaderComponents.Enumerators;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenFaderComponents.Actions
{
    public class GameObjectFadingAction : IAction
    {
        protected FadeDirection direction;

        private float finish = -1f;

        private readonly Image[] images;

        private readonly Renderer[] renderers;

        private float start = -1f;

        private readonly Text[] texts;

        protected float time;

        protected GameObjectFadingAction()
        {
        }

        public GameObjectFadingAction(FadeDirection direction, GameObject obj, float time)
        {
            this.direction = direction;
            this.time = time;
            renderers = getComponents<Renderer>(obj);
            images = getComponents<Image>(obj);
            texts = getComponents<Text>(obj);
        }

        public bool Completed { get; set; }

        public void Execute()
        {
            if (finish == -1f)
            {
                start = Time.time;
                finish = start + time;
            }

            var fadeValue = GetFadeValue();
            Apply(fadeValue);
            if (Time.time > finish) Completed = true;
        }

        private T[] getComponents<T>(GameObject go) where T : class
        {
            var array = go.GetComponentsInChildren<T>();
            var components = go.GetComponents<T>();
            if (components != null && components.Length > 0)
            {
                Array.Resize(ref array, array.Length + components.Length);
                for (var i = 0; i < components.Length; i++) array[array.Length - 1 + i] = components[i];
            }

            return array;
        }

        protected virtual void Apply(float value)
        {
            for (var i = 0; i < renderers.Length; i++)
            {
                var color = renderers[i].material.color;
                color.a = value;
                renderers[i].material.color = color;
            }

            for (var j = 0; j < images.Length; j++)
            {
                var color2 = images[j].color;
                color2.a = value;
                images[j].color = color2;
            }

            for (var k = 0; k < texts.Length; k++)
            {
                var color3 = texts[k].color;
                color3.a = value;
                texts[k].color = color3;
            }
        }

        protected float GetFadeValue()
        {
            switch (direction)
            {
                case FadeDirection.In:
                    return 1f - (Time.time - start) / (finish - start);
                case FadeDirection.Out:
                    return (Time.time - start) / (finish - start);
                default:
                    return 0f;
            }
        }
    }
}