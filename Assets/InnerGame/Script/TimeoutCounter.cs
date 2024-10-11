using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public class TimeoutCounter
{
    public TimeoutCounter(float timeout, float resolution)
    {
        Timeout = timeout;
        Resolution = resolution;
        OnTimerStart = new UnityEvent();
        OnTimerElapsed = new UnityEvent<float>();
        OnTimerEnd = new UnityEvent();
    }

    public float Timeout { get; }

    public float Resolution { get; }

    public UnityEvent OnTimerStart { get; }

    public UnityEvent<float> OnTimerElapsed { get; }

    public UnityEvent OnTimerEnd { get; }

    public IEnumerable StartCoroutine()
    {
        float elapsed = Timeout;
        OnTimerStart.Invoke();
        while (elapsed >= 0)
        {
            yield return new WaitForSeconds(Resolution);
            elapsed -= Resolution;
        }
        OnTimerEnd.Invoke();
    }
}
