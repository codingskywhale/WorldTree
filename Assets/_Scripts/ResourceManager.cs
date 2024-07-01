using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [Header("Resource Fields")]
    // 필드 정의
    public int waterAmount = 0;
    public int currentLevel = 1;
    public int waterPerLevel = 10; // 레벨 당 필요한 물의 양
    [Header("UI Elements")]
    // UI 요소 정의
    public TextMeshProUGUI waterText; // 물의 재화량을 표시할 텍스트
    public TextMeshProUGUI levelText; // 현재 레벨을 표시할 텍스트
    public TextMeshProUGUI touchLevelText; // 터치 강화 레벨을 표시할 UI 텍스트
    //public Text touchIncreaseAmountText; // 터치 증가량을 표시할 UI 텍스트
    public TextMeshProUGUI waterIncreaseLevelText;
    public TextMeshProUGUI waterIncreaseUpgradeCostText;
    public TextMeshProUGUI upgradeWaterCostText; // 강화 비용을 표시할 UI 텍스트
    public Image levelFillImage; // 레벨 바 이미지
    public Image currentTreeImage; // 현재 나무 이미지
    public Image upgradedTreeImage; // 강화된 나무 이미지
    public SpriteRenderer outsideTreeSpriteRenderer; // 밖에 있는 나무 이미지
    public TextMeshProUGUI upgradeRequirementText; // 강화에 필요한 재화를 표시할 텍스트
    public SpriteRenderer groundSpriteRenderer; // 지면의 SpriteRenderer 컴포넌트
    public Sprite[] treeImages; // 나무 이미지를 담을 배열

    // 초기 설정
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

    // 물의 양을 감소시키는 메서드
    public void DecreaseWater(int amount)
    {
        waterAmount -= amount;
        UpdateUI();
    }

    // 레벨을 업그레이드하는 메서드
    public void UpgradeLevel(int amount)
    {
        int waterNeededForUpgrade = (currentLevel + amount) * waterPerLevel;

        if (waterAmount >= waterNeededForUpgrade)
        {
            DecreaseWater(waterNeededForUpgrade);
            currentLevel += amount;
            UpdateUI();
            UpdateGroundSize();
        }
        else
        {
            Debug.Log("물이 부족하여 강화할 수 없습니다.");
        }
    }

    // 지면 크기를 업데이트하는 메서드
    private void UpdateGroundSize()
    {
        float groundScale = 8f + (currentLevel / 10f); // 레벨에 따른 지면 크기 계산
        groundSpriteRenderer.transform.localScale = new Vector3(groundScale, groundScale, groundScale);
    }

    // UI를 업데이트하는 메서드
    public void UpdateUI()
    {
        // 현재 레벨에 필요한 물의 양 계산
        int waterNeededForCurrentLevel = (currentLevel + 1) * waterPerLevel;
        waterText.text = $"Water: {waterAmount}";
        levelText.text = $"Level: {currentLevel}";
        levelFillImage.fillAmount = (float)waterAmount / waterNeededForCurrentLevel;

        // 나무 이미지 업데이트
        UpdateTreeImages();

        // 업그레이드에 필요한 재화 텍스트 업데이트
        upgradeRequirementText.text = $"필요한 재화: {waterNeededForCurrentLevel} 물 ";
    }

    // 나무 이미지를 업데이트하는 메서드
    private void UpdateTreeImages()
    {
        int currentIndex = currentLevel / 5;
        int nextIndex = (currentLevel + 1) / 5;

        currentTreeImage.sprite = treeImages[currentIndex];
        upgradedTreeImage.sprite = treeImages[nextIndex];
        outsideTreeSpriteRenderer.sprite = treeImages[currentIndex];
    }
}
