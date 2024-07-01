using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    public ResourceManager resourceManager; // ResourceManager ����
    public int touchIncreaseLevel = 1; // ��ġ ��ȭ ����
    public int touchIncreaseAmount = 10; // Ŭ���� ������ ���� ��
    public int upgradeWaterCost = 20; // ��ȭ�� �ʿ��� ���� ��

    private void Start()
    {
        UpdateUI();
    }
    void Update()
    {
        // ��ġ �Է� ����
        if (Input.GetMouseButtonDown(0)) // ���콺 ���� ��ư Ŭ�� �Ǵ� ��ġ �Է�
        {
            resourceManager.IncreaseWater(touchIncreaseAmount); // ���� �� ����
        }
    }

    // ��ȭ ��ư Ŭ�� �� ȣ��� �޼���
    public void UpgradeIncreaseAmount()
    {
        // ���� ���� ����� ��쿡�� ��ȭ ����
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
            Debug.Log("���� �����Ͽ� ��ȭ�� �� �����ϴ�.");
        }
    }

    // UI ������Ʈ �޼���
    private void UpdateUI()
    {
        // ��ġ ��ȭ ����, ������, ��ȭ�� �ʿ��� ���� ���� UI�� ǥ��
        resourceManager.touchLevelText.text = $"��ġ ��ȭ ����: {touchIncreaseLevel}";
        //resourceManager.touchIncreaseAmountText.text = $"Ŭ���� ������: {touchIncreaseAmount}";
        resourceManager.upgradeWaterCostText.text = $"��ȭ ���: {upgradeWaterCost} ��";
    }
}

