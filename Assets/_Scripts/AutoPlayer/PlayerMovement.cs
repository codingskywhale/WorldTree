using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targets; // ��ǥ ������
    public float speed = 5f; // �̵� �ӵ�
    public ResourceManager resourceManager; // ResourceManager ����
    public WaterManager waterManager; // waterManager ����
    public UIManagerBG uiManager;
    public int waterIncreaseAmount = 10; // ������ �������� �� �����ϴ� ���� ��
    public int waterIncreaseLevel = 1; // �� ������ ��ȭ ����
    public int waterIncreaseUpgradeCost = 20; // �� ������ ��ȭ�� �ʿ��� ���� ��
    public int moveSpeedLevel = 1;
    public int moveUpgradeCost = 20;


    private int currentTargetIndex = 0;
    private bool isWaiting = false; // ��� ���� Ȯ��

    private void Start()
    {
        UpdateUI();
    }
    void Update()
    {
        if (targets.Length == 0 || isWaiting)
            return;

        // ���� ��ǥ �������� �̵�
        Transform target = targets[currentTargetIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // ��ǥ ������ �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(HandleArrival(target));
        }
    }

    IEnumerator HandleArrival(Transform target)
    {
        // ���� �±׸� ���� ������Ʈ�� �����ߴ��� Ȯ��
        if (target.CompareTag("Tree"))
        {
            waterManager.IncreaseWater(waterIncreaseAmount); // ���� �� ����
        }

        // �� �±׸� ���� ������Ʈ�� �����ߴ��� Ȯ��
        if (target.CompareTag("Water"))
        {
            isWaiting = true; // ��� ���·� ��ȯ
            yield return new WaitForSeconds(1f); // 1�� ���
            isWaiting = false; // ��� ���� ����
        }

        // ���� ��ǥ �������� ����
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
    }

    // ��ư Ŭ�� �� ȣ��� �޼���
    public void UpgradeWaterIncreaseAmount()
    {
        // ���� ���� ����� ��쿡�� ��ȭ ����
        if (waterManager.waterAmount >= waterIncreaseUpgradeCost)
        {
            waterManager.DecreaseWater(waterIncreaseUpgradeCost);
            waterIncreaseLevel++;
            waterIncreaseAmount += 10;
            waterIncreaseUpgradeCost += 20;
            UpdateUI();
        }
        else
        {
            Debug.Log("���� �����Ͽ� ��ȭ�� �� �����ϴ�.");
        }
    }
    public void MoveSpeedUpgrade()
    {
        // ���� ���� ����� ��쿡�� ��ȭ ����
        if (waterManager.waterAmount >= moveUpgradeCost)
        {
            waterManager.DecreaseWater(moveUpgradeCost);
            moveSpeedLevel++;
            speed += 5;
            moveUpgradeCost += 20;
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
        // �� ������ ��ȭ ����, ������, ��ȭ�� �ʿ��� ���� ���� UI�� ǥ��
        uiManager.waterIncreaseLevelText.text = $"�� ������ ��ȭ ����: {waterIncreaseLevel}";
        //resourceManager.waterIncreaseAmountText.text = $"���� ���� �� ������: {waterIncreaseAmount}";
        uiManager.waterIncreaseUpgradeCostText.text = $"��ȭ ���: {waterIncreaseUpgradeCost} ��";
        uiManager.moveSpeedLevelText.text = $"���ΰ� ���ǵ� ��ȭ ���� : {moveSpeedLevel}";
        uiManager.moveSpeedUpgradeCostText.text = $"��ȭ ��� : {moveUpgradeCost}";
    }
}
