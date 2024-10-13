using TMPro;
using UnityEngine;

public class TextSpan : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _key;

    [SerializeField]
    private TMP_Text _value;

    public void SetKey(string key)
    {
        _key.text = key;
    }

    public void SetValue(string value)
    {
        _value.text = value;
    }
}
