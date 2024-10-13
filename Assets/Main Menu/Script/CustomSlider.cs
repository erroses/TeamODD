using UnityEngine;
using UnityEngine.UI;

public class CustomSlider : MonoBehaviour
{
    public RectTransform background; // 슬라이더 배경
    public RectTransform fill;       // 슬라이더 바 (Fill)
    public RectTransform handle;     // 슬라이더 버튼 (Handle)

    private float minValue = 0f;     // 최소 값
    private float maxValue = 1f;     // 최대 값
    private float currentValue;       // 현재 값

    void Start()
    {
        // 현재 값을 초기화
        currentValue = minValue;
        UpdateSlider();
    }

    void Update()
    {
        // 마우스 클릭으로 슬라이더 값을 변경
        if (Input.GetMouseButton(0))
        {
            Vector2 mousePos = Input.mousePosition;
            UpdateSliderValue(mousePos);
        }
    }

    private void UpdateSliderValue(Vector2 mousePos)
    {
        // 배경 위치와 크기를 사용하여 슬라이더 값 계산
        Vector2 backgroundPos = background.anchoredPosition;
        Vector2 backgroundSize = background.sizeDelta;

        // 슬라이더 배경의 왼쪽을 기준으로 마우스 위치를 비율로 변환
        float percent = (mousePos.x - (backgroundPos.x - backgroundSize.x / 2)) / backgroundSize.x;

        // 비율을 현재 값으로 변환
        currentValue = Mathf.Clamp(percent * (maxValue - minValue) + minValue, minValue, maxValue);

        UpdateSlider();
    }

    private void UpdateSlider()
    {
        // 슬라이더 바의 크기를 현재 값에 맞게 조정
        float fillWidth = (currentValue - minValue) / (maxValue - minValue) * background.sizeDelta.x;
        fill.sizeDelta = new Vector2(fillWidth, fill.sizeDelta.y);

        // 슬라이더 핸들의 위치를 현재 값에 맞게 조정
        float handleX = (currentValue - minValue) / (maxValue - minValue) * background.sizeDelta.x - (background.sizeDelta.x / 2);
        handle.anchoredPosition = new Vector2(handleX, handle.anchoredPosition.y);
    }
}