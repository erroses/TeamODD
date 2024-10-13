using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerDiagnostics : MonoBehaviour
{
    private static readonly Func<int>[] DiagnosticValueGenerator = {
        () => GameStatistics.Instance.Player1JarAttackCount,
        () => GameStatistics.Instance.Player2JarAttackCount,
        () => GameStatistics.Instance.Player1AttackCount,
        () => GameStatistics.Instance.Player2AttackCount,
    };

    [SerializeField]
    private TextSpan[] _textSpans;

    public UnityEvent OnDialogDisplay { get; private set; }

    private void Awake()
    {
        foreach (TextSpan textSpan in _textSpans)
        {
            textSpan.gameObject.SetActive(false);
        }
        OnDialogDisplay = new UnityEvent();
    }

    private void Start()
    {
        StartDisplayDiagnostics(0.75f);
    }

    public void StartDisplayDiagnostics(float delay)
    {
        StartCoroutine(StartDisplayDiagnosticsAnimation(delay));
    }

    private IEnumerator StartDisplayDiagnosticsAnimation(float delay)
    {
        for (int i = 0; i < _textSpans.Length; i++)
        {
            yield return new WaitForSeconds(delay);
            TextSpan textSpan = _textSpans[i];
            textSpan.SetValue(DiagnosticValueGenerator[i]().ToString());
            textSpan.gameObject.SetActive(true);
            OnDialogDisplay.Invoke();
        }
    }
}
