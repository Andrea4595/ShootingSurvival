using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class Time : Singleton<Time>
    {
        public static float smoothDeltaTime => timeScale * UnityEngine.Time.smoothDeltaTime;
        public static float timeScale = 1f;

        [SerializeField]
        AnimationCurve _fadeCurve;

        Coroutine _fade;

        public void Fade(float targetTimeScale, float duration)
        {
            if (_fade != null)
                StopCoroutine(_fade);

            if (duration == 0)
            {
                timeScale = targetTimeScale;
                return;
            }

            _fade = StartCoroutine(CRun(targetTimeScale, duration));
        }

        IEnumerator CRun(float targetTimeScale, float duration)
        {
            var time = 0f;
            var start = timeScale;
            var different = targetTimeScale - start;

            while (time <= 1)
            {
                time += UnityEngine.Time.smoothDeltaTime / duration;

                timeScale = start + different * _fadeCurve.Evaluate(time);

                yield return null;
            }

            timeScale = targetTimeScale;
        }

        public static IEnumerator WaitForSeconds(float time)
        {
            while (time > 0)
            {
                time -= smoothDeltaTime;
                yield return null;
            }
        }
    }
}