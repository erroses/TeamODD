using GameJam.Project.UI;

using UnityEngine;

public class GameSystemTimeoutCounter : MonoBehaviour
{
    [SerializeField] private CountDown _timeoutCountDown;

    public void SetReadyCountDown(int readyCountDown)
    {
        _timeoutCountDown.SetText(readyCountDown.ToString());
    }

    public void SetCountdown(string countdown)
    {
        _timeoutCountDown.SetText(countdown);
    }
}
