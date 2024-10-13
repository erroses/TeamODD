using System;
using System.Collections;
using UnityEngine;

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

    private void Awake()
    {
        foreach (TextSpan textSpan in _textSpans)
        {
            textSpan.gameObject.SetActive(false);
        }
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
            TextSpan textSpan = _textSpans[i];
            textSpan.SetValue(DiagnosticValueGenerator[i]().ToString());
            textSpan.gameObject.SetActive(true);
            yield return new WaitForSeconds(delay);
        }
    }
}
