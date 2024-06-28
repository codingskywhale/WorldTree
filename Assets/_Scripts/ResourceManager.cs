using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public int waterAmount = 0;
    public int currentLevel = 1;
    public int waterPerLevel = 10; // 레벨 당 필요한 물의 양
    private const int maxWaterAmount = 200; // 물의 최대 양
    public TextMeshProUGUI waterText; // 물의 재화량을 표시할 텍스트
    public TextMeshProUGUI levelText; // 현재 레벨을 표시할 텍스트
    public Image levelFillImage; // 레벨 바 이미지

    void Start()
    {
        UpdateUI(); // 초기 UI 업데이트
    }

    // 물의 양을 증가시키는 메서드
    public void IncreaseWater(int amount)
    {
        waterAmount += amount;        
        UpdateUI();
    }

    

    // UI를 업데이트하는 메서드
    private void UpdateUI()
    {
        waterText.text = "Water: " + waterAmount;
        levelText.text = "Level: " + currentLevel;
        levelFillImage.fillAmount = (float)waterAmount / maxWaterAmount;
    }
}
