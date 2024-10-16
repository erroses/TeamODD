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

    public IEnumerator StartCoroutine()
    {
        var elapsed = Timeout;
        var sum = 0f;
        OnTimerStart.Invoke();
        while (elapsed >= 0)
        {
            sum += Resolution;
            elapsed -= Resolution;
            yield return new WaitForSeconds(Resolution);
            OnTimerElapsed.Invoke(sum);
        }
        OnTimerEnd.Invoke();
    }
}
