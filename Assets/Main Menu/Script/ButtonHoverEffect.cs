using System.Collections; // IEnumerator 사용을 위한 네임스페이스
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Image 사용을 위한 네임스페이스

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage; // Image 컴포넌트를 저장
    private CanvasGroup canvasGroup; // CanvasGroup 컴포넌트를 저장
    public float hoverAlpha = 0.5f; // 마우스 오버 시 투명도
    public float fadeDuration = 0.3f; // 페이드 효과의 지속 시간

    private void Start()
    {
        // 오브젝트의 Image 및 CanvasGroup 컴포넌트를 가져오기
        buttonImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (buttonImage == null)
        {
            Debug.LogError("Image component not found on this GameObject.");
            return;
        }

        if (canvasGroup == null)
        {
            // CanvasGroup이 없으면 추가
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // 초기 알파값을 0으로 설정 (완전히 투명하게 시작)
        Color initialColor = buttonImage.color;
        initialColor.a = 0f; // 완전히 투명하게 시작
        buttonImage.color = initialColor;

        // CanvasGroup의 초기 알파값 설정
        canvasGroup.alpha = 0f; // 완전히 투명하게 시작
    }

    // 마우스가 버튼 위에 있을 때
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Entered Button");
        StartCoroutine(FadeTo(hoverAlpha, fadeDuration));
    }

    // 마우스가 버튼에서 나갈 때
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exited Button");
        StartCoroutine(FadeTo(0f, fadeDuration)); // 완전히 투명하게 되돌리기
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha; // 현재 CanvasGroup의 알파값
        float time = 0f;

        // 부드럽게 알파값을 변화시키기
        while (time < duration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            Color newColor = buttonImage.color; // 현재 색상 가져오기
            newColor.a = newAlpha; // 새로운 알파값 설정
            buttonImage.color = newColor; // 색상 변경

            // CanvasGroup의 알파값 변경
            canvasGroup.alpha = newAlpha; // CanvasGroup 알파값 변경

            yield return null; // 다음 프레임까지 대기
        }

        // 최종 알파값 설정
        Color finalColor = buttonImage.color;
        finalColor.a = targetAlpha;
        buttonImage.color = finalColor;

        // CanvasGroup의 최종 알파값 설정
        canvasGroup.alpha = targetAlpha;
    }
}
