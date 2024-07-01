using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
        if (Input.GetMouseButtonDown(0) && !isPointerOverUIElement()) // ���콺 ���� ��ư Ŭ�� �Ǵ� ��ġ �Է�, UI ��� ���� ���� ���� ���
        {
            resourceManager.IncreaseWater(touchIncreaseAmount); // ���� �� ����
        }
    }

    private bool isPointerOverUIElement()
    {
        // �����Ͱ� UI ��� ���� �ִ��� Ȯ��
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
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

