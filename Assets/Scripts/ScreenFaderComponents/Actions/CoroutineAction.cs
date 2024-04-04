using System;
using UnityEngine;

namespace ScreenFaderComponents.Actions
{
    public class CoroutineAction : IParametrizedAction
    {
        private FaderCoroutinebm result;

        public bool Completed { get; set; }

        public void Execute(params object[] parameters)
        {
            if (parameters == null || parameters.Length < 2) throw new ArgumentOutOfRangeException();
            var monoBehaviour = parameters[0] as MonoBehaviour;
            var text = parameters[1] as string;
            if (monoBehaviour == null || text == null) throw new ArgumentNullException();
            if (result == null)
            {
                result = new FaderCoroutinebm();
                result.Coroutinebm = monoBehaviour.StartCoroutine(text);
            }

            Completed = true;
        }
    }
}