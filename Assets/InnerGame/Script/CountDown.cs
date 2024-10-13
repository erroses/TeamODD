using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    public void SetText(string text)
    {
        _text.text = text;
    }
}
