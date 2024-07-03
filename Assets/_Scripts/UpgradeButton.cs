using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public WaterManager waterManager;
    public UIManagerBG uiManager;
    public ResourceManager resourceManager;
    public Root root;
    public Spirit spirit;
    public TouchInputManager touchInputManager;
    public PlayerMovement playerMovement;

    public enum UpgradeType
    {
        Root,
        Spirit,
        Touch,
        Tree,
        MoveSpeed,
        WaterIncrease
    }

    public UpgradeType upgradeType;
    public int upgradeAmount = 1;

    private Button upgradeButton;

    private void Start()
    {
        upgradeButton = GetComponent<Button>();
        upgradeButton.onClick.AddListener(OnUpgradeButtonClicked);

        if (upgradeType == UpgradeType.Root && root != null)
        {
            UpdateUpgradeCostUI(root.rootLevel);
        }
        else if (upgradeType == UpgradeType.Spirit && spirit != null)
        {
            UpdateUpgradeCostUI(spirit.spiritLevel);
        }
        else if (upgradeType == UpgradeType.Touch && touchInputManager != null)
        {
            UpdateUpgradeCostUI(touchInputManager.touchIncreaseLevel);
        }
        else if (upgradeType == UpgradeType.Tree && resourceManager != null)
        {
            UpdateUpgradeCostUI(resourceManager.waterManager.currentLevel);
        }
        else if ((upgradeType == UpgradeType.MoveSpeed || upgradeType == UpgradeType.WaterIncrease) && playerMovement != null)
        {
            UpdateUpgradeCostUI(playerMovement.moveSpeedLevel);
        }
    }

    private void OnUpgradeButtonClicked()
    {
        switch (upgradeType)
        {
            case UpgradeType.Root:
                HandleRootUpgrade();
                break;
            case UpgradeType.Spirit:
                HandleSpiritUpgrade();
                break;
            case UpgradeType.Touch:
                HandleTouchUpgrade();
                break;
            case UpgradeType.Tree:
                HandleGeneralUpgrade();
                break;
            case UpgradeType.MoveSpeed:
                HandleMoveSpeedUpgrade();
                break;
            case UpgradeType.WaterIncrease:
                HandleWaterIncreaseUpgrade();
                break;
        }
    }

    private void HandleRootUpgrade()
    {
        int upgradeCost = root.CalculateUpgradeCost();
        if (waterManager.HasSufficientWater(upgradeCost))
        {
            waterManager.DecreaseWater(upgradeCost);
            root.rootLevel++;
            root.upgradeWaterCost += 20;
            root.UpdateUI();
        }
        else
        {
            Debug.Log("���� �����Ͽ� ��ȭ�� �� �����ϴ�.");
        }
    }

    private void HandleSpiritUpgrade()
    {
        int upgradeCost = spirit.CalculateUpgradeCost();
        if (waterManager.HasSufficientWater(upgradeCost))
        {
            waterManager.DecreaseWater(upgradeCost);
            spirit.spiritLevel++;
            spirit.upgradeEnergyCost += 20;
            spirit.UpdateUI();
        }
        else
        {
            Debug.Log("�������� �����Ͽ� ��ȭ�� �� �����ϴ�.");
        }
    }

    private void HandleTouchUpgrade()
    {
        int upgradeWaterCost = touchInputManager.upgradeWaterCost;
        if (waterManager.HasSufficientWater(upgradeWaterCost))
        {
            waterManager.DecreaseWater(upgradeWaterCost);
            touchInputManager.touchIncreaseLevel++;
            touchInputManager.touchIncreaseAmount += 10;
            touchInputManager.upgradeWaterCost += 20;
            uiManager.UpdateTouchUI(touchInputManager.touchIncreaseLevel, touchInputManager.touchIncreaseAmount, touchInputManager.upgradeWaterCost);
        }
        else
        {
            Debug.Log("���� �����Ͽ� ��ȭ�� �� �����ϴ�.");
        }
    }

    private void HandleGeneralUpgrade()
    {
        int waterNeededForUpgrade = waterManager.CalculateWaterNeededForUpgrade(upgradeAmount);
        if (waterManager.HasSufficientWater(waterNeededForUpgrade))
        {
            waterManager.DecreaseWater(waterNeededForUpgrade);
            waterManager.currentLevel += upgradeAmount;
            resourceManager.UpdateUI();
            resourceManager.UpdateGroundSize();
        }
        else
        {
            Debug.Log("���� �����Ͽ� ��ȭ�� �� �����ϴ�.");
        }
    }

    private void HandleMoveSpeedUpgrade()
    {
        int upgradeCost = playerMovement.moveUpgradeCost;
        if (waterManager.HasSufficientWater(upgradeCost))
        {
            waterManager.DecreaseWater(upgradeCost);
            playerMovement.moveSpeedLevel++;
            playerMovement.speed += 5;
            playerMovement.moveUpgradeCost += 20;
            uiManager.UpdateMovementUI(playerMovement.moveSpeedLevel, playerMovement.moveUpgradeCost);
        }
        else
        {
            Debug.Log("���� �����Ͽ� ��ȭ�� �� �����ϴ�.");
        }
    }

    private void HandleWaterIncreaseUpgrade()
    {
        int upgradeCost = playerMovement.waterIncreaseUpgradeCost;
        if (waterManager.HasSufficientWater(upgradeCost))
        {
            waterManager.DecreaseWater(upgradeCost);
            playerMovement.waterIncreaseLevel++;
            playerMovement.waterIncreaseAmount += 10;
            playerMovement.waterIncreaseUpgradeCost += 20;
            uiManager.UpdateWaterIncreaseUI(playerMovement.waterIncreaseLevel, playerMovement.waterIncreaseAmount, playerMovement.waterIncreaseUpgradeCost);
        }
        else
        {
            Debug.Log("���� �����Ͽ� ��ȭ�� �� �����ϴ�.");
        }
    }

    private void UpdateUpgradeCostUI(int newLevel)
    {
        if (upgradeType == UpgradeType.Root && root != null)
        {
            int upgradeCost = root.upgradeWaterCost;
            uiManager.UpdateRootLevelUI(root.rootLevel, upgradeCost);
        }
        else if (upgradeType == UpgradeType.Spirit && spirit != null)
        {
            int upgradeCost = spirit.upgradeEnergyCost;
            uiManager.UpdateSpiritLevelUI(spirit.spiritLevel, upgradeCost);
        }
        else if (upgradeType == UpgradeType.Touch && touchInputManager != null)
        {
            uiManager.UpdateTouchUI(touchInputManager.touchIncreaseLevel, touchInputManager.touchIncreaseAmount, touchInputManager.upgradeWaterCost);
        }
        else if (upgradeType == UpgradeType.Tree && resourceManager != null)
        {
            int waterNeededForCurrentLevel = resourceManager.waterManager.CalculateWaterNeededForUpgrade(1);
            uiManager.UpdateUpgradeRequirementUI(resourceManager.waterManager.currentLevel, waterNeededForCurrentLevel);
        }
        else if ((upgradeType == UpgradeType.MoveSpeed || upgradeType == UpgradeType.WaterIncrease) && playerMovement != null)
        {
            uiManager.UpdateMovementUI(playerMovement.moveSpeedLevel, playerMovement.moveUpgradeCost);
            uiManager.UpdateWaterIncreaseUI(playerMovement.waterIncreaseLevel, playerMovement.waterIncreaseAmount, playerMovement.waterIncreaseUpgradeCost);
        }
    }
}
