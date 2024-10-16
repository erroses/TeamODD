using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameJam.Project.UI
{
    [RequireComponent(typeof(Button))]
    public class SettingsButtonComponent
        : MonoBehaviour,
          IPointerEnterHandler,
          IPointerExitHandler,
          IPointerDownHandler,
          IPointerUpHandler
    {
        [SerializeField] private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            button.OnPointerEnter(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            button.OnPointerExit(eventData);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            button.OnPointerDown(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            button.OnPointerUp(eventData);
        }
    }
}
