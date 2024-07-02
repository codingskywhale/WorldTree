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
        //levelFillImage.fillAmount = (float)waterAmount / waterNeededForCurrentLevel;
    }

    public void UpdateEnergyUI(int energyAmount, int maxEnergy)
    {
        energyText.text = $"������ : {energyAmount}/{maxEnergy}";
    }

    public void UpdateLevelUI(int currentLevel)
    {
        levelText.text = $"Level: {currentLevel}";
        UpdateLevelFillImage(currentLevel);
    }
    private void UpdateLevelFillImage(int currentLevel)
    {
        int levelInGroup = currentLevel % 5;

        // 4����, 9����, 14����, 19���� ����� fillAmount�� 1�� ����
        if (levelInGroup == 4)
        {
            levelFillImage.fillAmount = 1f;
        }
        else
        {
            levelFillImage.fillAmount = (float)levelInGroup / 4f;
        }
    }

    public void UpdateUpgradeRequirementUI(int currentLevel, int waterNeededForCurrentLevel)
    {     
        if(currentLevel % 5 == 4)
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
}
