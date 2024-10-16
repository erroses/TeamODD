using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameJam.Project.UI
{
    [RequireComponent(typeof(Button))]
    public class MainMenuButtonComponent
        : MonoBehaviour,
          IPointerEnterHandler,
          IPointerExitHandler,
          IPointerDownHandler,
          IPointerUpHandler
    {
        [SerializeField] private GameObject defaultText;
        [SerializeField] private GameObject focusBackground;
        [SerializeField] private GameObject focusText;
        [SerializeField] private GameObject clickBackground;
        [SerializeField] private GameObject clickText;
        [SerializeField] private Button button;

        private void Awake()
        {
            DisplayDefaultText();
            button = GetComponent<Button>();
            button.interactable = false;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            DisplayFocusText();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            DisplayDefaultText();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            DisplayClickText();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            button.onClick.Invoke();
            if (eventData.hovered.Contains(gameObject))
            {
                DisplayFocusText();
            }
            else
            {
                DisplayDefaultText();
            }
        }

        private void DisplayDefaultText()
        {
            defaultText.SetActive(true);
            focusBackground.SetActive(false);
            focusText.SetActive(false);
            clickBackground.SetActive(false);
            clickText.SetActive(false);
        }

        private void DisplayFocusText()
        {
            defaultText.SetActive(false);
            focusBackground.SetActive(true);
            focusText.SetActive(true);
            clickBackground.SetActive(false);
            clickText.SetActive(false);
        }

        private void DisplayClickText()
        {
            defaultText.SetActive(false);
            focusBackground.SetActive(false);
            focusText.SetActive(false);
            clickBackground.SetActive(true);
            clickText.SetActive(true);
        }
    }
}
