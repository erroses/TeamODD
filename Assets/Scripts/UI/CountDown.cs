using TMPro;
using UnityEngine;

namespace GameJam.Project.UI
{
    public class CountDown : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text _text;

        public void SetText(string text)
        {
            _text.text = text;
        }
    }
}
