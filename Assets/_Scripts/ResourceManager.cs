using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int waterAmount = 0;
    public TextMeshProUGUI WaterText;
    void Start()
    {
        UpdateWaterText(); // 초기 텍스트 업데이트
    }

    // 물의 양을 증가시키는 메서드
    public void IncreaseWater(int amount)
    {
        waterAmount += amount;
        UpdateWaterText();
    }

    // 텍스트 업데이트 메서드
    private void UpdateWaterText()
    {
        WaterText.text = "Water: " + waterAmount;
    }
}
