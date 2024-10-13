using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    public RectTransform background; // �����̴� ���
    public RectTransform fill;       // �����̴� �� (Fill)
    public RectTransform handle;     // �����̴� ��ư (Handle)

    private float minValue = 0f;     // �ּ� ��
    private float maxValue = 1f;     // �ִ� ��
    private float currentValue;       // ���� ��

    void Start()
    {
        // ���� ���� �ʱ�ȭ
        currentValue = minValue;
        UpdateSlider();
    }

    void Update()
    {
        // ���콺 Ŭ������ �����̴� ���� ����
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            UpdateSliderValue(mousePos);
        }
    }

    private void UpdateSliderValue(Vector2 mousePos)
    {
        // ��� ��ġ�� ũ�⸦ ����Ͽ� �����̴� �� ���
        Vector2 backgroundPos = background.anchoredPosition;
        Vector2 backgroundSize = background.sizeDelta;

        // �����̴� ����� ������ �������� ���콺 ��ġ�� ������ ��ȯ
        float percent = (mousePos.x - (backgroundPos.x - backgroundSize.x / 2)) / backgroundSize.x;

        // ������ ���� ������ ��ȯ
        currentValue = Mathf.Clamp(percent * (maxValue - minValue) + minValue, minValue, maxValue);

        UpdateSlider();
    }

    private void UpdateSlider()
    {
        // �����̴� ���� ũ�⸦ ���� ���� �°� ����
        float fillWidth = (currentValue - minValue) / (maxValue - minValue) * background.sizeDelta.x;
        fill.sizeDelta = new Vector2(fillWidth, fill.sizeDelta.y);

        // �����̴� �ڵ��� ��ġ�� ���� ���� �°� ����
        float handleX = (currentValue - minValue) / (maxValue - minValue) * background.sizeDelta.x - (background.sizeDelta.x / 2);
        handle.anchoredPosition = new Vector2(handleX, handle.anchoredPosition.y);
    }
}