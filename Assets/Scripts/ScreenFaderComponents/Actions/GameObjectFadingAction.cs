using System;
using ScreenFaderComponents.Enumerators;
using UnityEngine;
using UnityEngine.UI;

namespace ScreenFaderComponents.Actions
{
	public class GameObjectFadingAction : IAction
	{
		protected FadeDirection direction;

		protected float time;

		private float start = -1f;

		private float finish = -1f;

		private Renderer[] renderers;

		private Image[] images;

		private Text[] texts;

		public bool Completed { get; set; }

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

		private T[] getComponents<T>(GameObject go) where T : class
		{
			T[] array = go.GetComponentsInChildren<T>();
			T[] components = go.GetComponents<T>();
			if (components != null && components.Length > 0)
			{
				Array.Resize(ref array, array.Length + components.Length);
				for (int i = 0; i < components.Length; i++)
				{
					array[array.Length - 1 + i] = components[i];
				}
			}
			return array;
		}

		public void Execute()
		{
			if (finish == -1f)
			{
				start = Time.time;
				finish = start + time;
			}
			float fadeValue = GetFadeValue();
			Apply(fadeValue);
			if (Time.time > finish)
			{
				Completed = true;
			}
		}

		protected virtual void Apply(float value)
		{
			for (int i = 0; i < renderers.Length; i++)
			{
				Color color = renderers[i].material.color;
				color.a = value;
				renderers[i].material.color = color;
			}
			for (int j = 0; j < images.Length; j++)
			{
				Color color2 = images[j].color;
				color2.a = value;
				images[j].color = color2;
			}
			for (int k = 0; k < texts.Length; k++)
			{
				Color color3 = texts[k].color;
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
