using System.Collections; // IEnumerator ����� ���� ���ӽ����̽�
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Image ����� ���� ���ӽ����̽�

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Image buttonImage; // Image ������Ʈ�� ����
    private CanvasGroup canvasGroup; // CanvasGroup ������Ʈ�� ����
    public float hoverAlpha = 0.5f; // ���콺 ���� �� ����
    public float fadeDuration = 0.3f; // ���̵� ȿ���� ���� �ð�

    private void Start()
    {
        // ������Ʈ�� Image �� CanvasGroup ������Ʈ�� ��������
        buttonImage = GetComponent<Image>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (buttonImage == null)
        {
            Debug.LogError("Image component not found on this GameObject.");
            return;
        }

        if (canvasGroup == null)
        {
            // CanvasGroup�� ������ �߰�
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }

        // �ʱ� ���İ��� 0���� ���� (������ �����ϰ� ����)
        Color initialColor = buttonImage.color;
        initialColor.a = 0f; // ������ �����ϰ� ����
        buttonImage.color = initialColor;

        // CanvasGroup�� �ʱ� ���İ� ����
        canvasGroup.alpha = 0f; // ������ �����ϰ� ����
    }

    // ���콺�� ��ư ���� ���� ��
    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse Entered Button");
        StartCoroutine(FadeTo(hoverAlpha, fadeDuration));
    }

    // ���콺�� ��ư���� ���� ��
    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse Exited Button");
        StartCoroutine(FadeTo(0f, fadeDuration)); // ������ �����ϰ� �ǵ�����
    }

    private IEnumerator FadeTo(float targetAlpha, float duration)
    {
        float startAlpha = canvasGroup.alpha; // ���� CanvasGroup�� ���İ�
        float time = 0f;

        // �ε巴�� ���İ��� ��ȭ��Ű��
        while (time < duration)
        {
            time += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            Color newColor = buttonImage.color; // ���� ���� ��������
            newColor.a = newAlpha; // ���ο� ���İ� ����
            buttonImage.color = newColor; // ���� ����

            // CanvasGroup�� ���İ� ����
            canvasGroup.alpha = newAlpha; // CanvasGroup ���İ� ����

            yield return null; // ���� �����ӱ��� ���
        }

        // ���� ���İ� ����
        Color finalColor = buttonImage.color;
        finalColor.a = targetAlpha;
        buttonImage.color = finalColor;

        // CanvasGroup�� ���� ���İ� ����
        canvasGroup.alpha = targetAlpha;
    }
}
