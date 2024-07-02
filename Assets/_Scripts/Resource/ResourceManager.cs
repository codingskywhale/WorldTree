using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public WaterManager waterManager;
    public EnergyManager energyManager;
    public UIManagerBG uiManager;

    private void Start()
    {
        waterManager.OnWaterChanged += UpdateWaterUI;
        energyManager.OnEnergyChanged += UpdateEnergyUI;
        UpdateUI();
    }

    public void UpgradeLevel(int amount)
    {
        int waterNeededForUpgrade = waterManager.CalculateWaterNeededForUpgrade(amount);
        if (waterManager.HasSufficientWater(waterNeededForUpgrade))
        {
            waterManager.DecreaseWater(waterNeededForUpgrade);
            waterManager.currentLevel += amount;
            UpdateUI();
            UpdateGroundSize();
        }
        else
        {
            Debug.Log("물이 부족하여 강화할 수 없습니다.");
        }
    }

    private void UpdateGroundSize()
    {
        float groundScale = 8f + (waterManager.currentLevel / 10f);
        uiManager.groundSpriteRenderer.transform.localScale = new Vector3(groundScale, groundScale, groundScale);
    }

    private void UpdateUI()
    {
        int waterNeededForCurrentLevel = waterManager.CalculateWaterNeededForUpgrade(1);
        uiManager.UpdateWaterUI(waterManager.waterAmount, waterNeededForCurrentLevel);
        uiManager.UpdateLevelUI(waterManager.currentLevel);
        uiManager.UpdateUpgradeRequirementUI(waterNeededForCurrentLevel);
        uiManager.UpdateTreeImages(waterManager.currentLevel, uiManager.treeImages);
    }

    private void UpdateWaterUI(int newWaterAmount)
    {
        int waterNeededForCurrentLevel = waterManager.CalculateWaterNeededForUpgrade(1);
        uiManager.UpdateWaterUI(newWaterAmount, waterNeededForCurrentLevel);
    }

    private void UpdateEnergyUI(int newEnergyAmount)
    {
        uiManager.UpdateEnergyUI(newEnergyAmount, energyManager.maxEnergy);
    }
}
