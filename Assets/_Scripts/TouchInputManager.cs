using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    public ResourceManager resourceManager; // ResourceManager 참조
    public int touchIncreaseLevel = 1; // 터치 강화 레벨
    public int touchIncreaseAmount = 10; // 클릭당 증가할 물의 양
    public int upgradeWaterCost = 20; // 강화에 필요한 물의 양

    private void Start()
    {
        UpdateUI();
    }
    void Update()
    {
        // 터치 입력 감지
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 또는 터치 입력
        {
            resourceManager.IncreaseWater(touchIncreaseAmount); // 물의 양 증가
        }
    }

    // 강화 버튼 클릭 시 호출될 메서드
    public void UpgradeIncreaseAmount()
    {
        // 물의 양이 충분한 경우에만 강화 진행
        if (resourceManager.waterAmount >= upgradeWaterCost)
        {
            resourceManager.DecreaseWater(upgradeWaterCost);
            touchIncreaseLevel++;
            touchIncreaseAmount += 10;
            upgradeWaterCost += 20;
            UpdateUI();
        }
        else
        {
            Debug.Log("물이 부족하여 강화할 수 없습니다.");
        }
    }

    // UI 업데이트 메서드
    private void UpdateUI()
    {
        // 터치 강화 레벨, 증가량, 강화에 필요한 물의 양을 UI에 표시
        resourceManager.touchLevelText.text = $"터치 강화 레벨: {touchIncreaseLevel}";
        //resourceManager.touchIncreaseAmountText.text = $"클릭당 증가량: {touchIncreaseAmount}";
        resourceManager.upgradeWaterCostText.text = $"강화 비용: {upgradeWaterCost} 물";
    }
}

