using System;
using System.Collections;
using UnityEngine;

namespace ScreenFaderComponents
{
    internal class FaderCoroutinebm
    {
        private Exception _exceptionbm;
        private bool _isCompleted;

        public Coroutine Coroutinebm;

        public bool IsCompleted
        {
            get
            {
                if (_exceptionbm != null) throw _exceptionbm;
                return _isCompleted;
            }
        }

        public IEnumerator IntCoroutine(IEnumerator coroutine)
        {
            while (true)
            {
                try
                {
                    if (!coroutine.MoveNext())
                    {
                        _isCompleted = true;
                        break;
                    }
                }
                catch (Exception ex)
                {
                    _exceptionbm = ex;
                    _isCompleted = true;
                    break;
                }

                yield return coroutine.Current;
            }
        }
    }
}