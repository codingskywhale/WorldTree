using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerBG : MonoBehaviour
{
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI touchLevelText;
    public TextMeshProUGUI moveSpeedLevelText;
    public TextMeshProUGUI moveSpeedUpgradeCostText;
    public TextMeshProUGUI waterIncreaseLevelText;
    public TextMeshProUGUI waterIncreaseUpgradeCostText;
    public TextMeshProUGUI upgradeWaterCostText;
    public TextMeshProUGUI energyText;
    public TextMeshProUGUI rootLevelText;
    public TextMeshProUGUI rootUpgradeCostText;
    public TextMeshProUGUI spiritLevelText;
    public TextMeshProUGUI spiritUpgradeCostText;
    public Image levelFillImage;
    public Image currentTreeImage;
    public Image upgradedTreeImage;
    public SpriteRenderer outsideTreeSpriteRenderer;
    public TextMeshProUGUI upgradeRequirementText;
    public SpriteRenderer groundSpriteRenderer;
    public Sprite[] treeImages;

    public void UpdateWaterUI(int waterAmount, int waterNeededForCurrentLevel)
    {
        waterText.text = $"�� : {waterAmount}";
        levelFillImage.fillAmount = (float)waterAmount / waterNeededForCurrentLevel;
    }

    public void UpdateEnergyUI(int energyAmount, int maxEnergy)
    {
        energyText.text = $"������ : {energyAmount}/{maxEnergy}";
    }

    public void UpdateLevelUI(int currentLevel)
    {
        levelText.text = $"Level: {currentLevel}";
    }

    public void UpdateUpgradeRequirementUI(int currentLevel, int waterNeededForCurrentLevel)
    {
        if (currentLevel % 5 == 4)
        {
            upgradeRequirementText.text = "����";
        }
        else
        {
            upgradeRequirementText.text = $"�ʿ��� ��ȭ: {waterNeededForCurrentLevel} ��";
        }
    }

    public void UpdateTreeImages(int currentLevel, Sprite[] treeImages)
    {
        int currentIndex = currentLevel / 5;
        int nextIndex = (currentLevel + 1) / 5;
        currentTreeImage.sprite = treeImages[currentIndex];
        upgradedTreeImage.sprite = treeImages[nextIndex];
        outsideTreeSpriteRenderer.sprite = treeImages[currentIndex];
    }

    public void UpdateTouchUI(int touchIncreaseLevel, int touchIncreaseAmount, int upgradeWaterCost)
    {
        touchLevelText.text = $"��ġ ��ȭ ����: {touchIncreaseLevel}";
        upgradeWaterCostText.text = $"��ȭ ���: {upgradeWaterCost} ��";
    }

    public void UpdateMovementUI(int moveSpeedLevel, int moveUpgradeCost)
    {
        moveSpeedLevelText.text = $"���ΰ� ���ǵ� ��ȭ ���� : {moveSpeedLevel}";
        moveSpeedUpgradeCostText.text = $"��ȭ ��� : {moveUpgradeCost}";
    }

    public void UpdateWaterIncreaseUI(int waterIncreaseLevel, int waterIncreaseAmount, int waterIncreaseUpgradeCost)
    {
        waterIncreaseLevelText.text = $"�� ������ ��ȭ ����: {waterIncreaseLevel}";
        waterIncreaseUpgradeCostText.text = $"��ȭ ���: {waterIncreaseUpgradeCost} ��";
    }

    public void UpdateRootLevelUI(int rootLevel, int upgradeCost)
    {
        rootLevelText.text = $"�Ѹ� ����: {rootLevel}";
        rootUpgradeCostText.text = $"��ȭ ���: {upgradeCost} ��";
    }

    public void UpdateSpiritLevelUI(int spiritLevel, int upgradeCost)
    {
        // ������ UI ��� ������Ʈ
        spiritLevelText.text = $"���� ����: {spiritLevel}";
        spiritUpgradeCostText.text = $"��ȭ ���: {upgradeCost} ������";
    }

}
