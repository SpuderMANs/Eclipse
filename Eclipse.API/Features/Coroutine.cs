namespace Eclipse.API.Features
{
    using UnityEngine;
    using System.Collections;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CoroutineRunner : MonoBehaviour
    {
        private static CoroutineRunner _instance;

        public static CoroutineRunner Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject("EclipseAPI_CoroutineRunner");
                    _instance = obj.AddComponent<CoroutineRunner>();
                    DontDestroyOnLoad(obj);
                }
                return _instance;
            }
        }
    }
    public static class Coroutine
    {
        public static UnityEngine.Coroutine Start(IEnumerator routine) => CoroutineRunner.Instance.StartCoroutine(routine);

        public static void Stop(UnityEngine.Coroutine coroutine) => CoroutineRunner.Instance.StopCoroutine(coroutine);

        public static void StopAll() => CoroutineRunner.Instance.StopAllCoroutines();
        public static UnityEngine.Coroutine CallDelayed(float delay, Action action)
        {
            return Start(CallDelayedRoutine(delay, action));
        }

        private static IEnumerator CallDelayedRoutine(float delay, Action action)
        {
            yield return new WaitForSeconds(delay);

            try
            {
                action?.Invoke();
            }
            catch (Exception e)
            {
                Log.Error($"Coroutine CallDelayed exception: {e}");
            }
        }

    }
}
