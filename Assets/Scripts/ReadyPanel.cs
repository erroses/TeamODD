using GameJam.Project.UI;

using UnityEngine;

public class ReadyPanel : MonoBehaviour
{
    [SerializeField]
    private CountDown _readyCountDown;

    public void SetReadyCountDown(int readyCountDown)
    {
        _readyCountDown.SetText(readyCountDown.ToString());
    }
}
