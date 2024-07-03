using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targets; // ��ǥ ������
    public float speed = 5f; // �̵� �ӵ�
    public WaterManager waterManager; // waterManager ����
    public UIManagerBG uiManager; // UIManagerBG ����
    public int waterIncreaseAmount = 10; // ������ �������� �� �����ϴ� ���� ��
    public int waterIncreaseLevel = 1; // �� ������ ��ȭ ����
    public int waterIncreaseUpgradeCost = 20; // �� ������ ��ȭ�� �ʿ��� ���� ��
    public int moveSpeedLevel = 1; // �̵� �ӵ� ��ȭ ����
    public int moveUpgradeCost = 20; // �̵� �ӵ� ��ȭ ���

    private int currentTargetIndex = 0;
    private bool isWaiting = false; // ��� ���� Ȯ��

    private void Start()
    {
        UpdateUI();
    }

    private void Update()
    {
        if (targets.Length == 0 || isWaiting)
            return;

        MoveToTarget();
    }

    private void MoveToTarget()
    {
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

    private IEnumerator HandleArrival(Transform target)
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

    // UI ������Ʈ �޼���
    private void UpdateUI()
    {
        // UIManagerBG�� ���� UI ������Ʈ
        uiManager.UpdateMovementUI(moveSpeedLevel, moveUpgradeCost);
        uiManager.UpdateWaterIncreaseUI(waterIncreaseLevel, waterIncreaseAmount, waterIncreaseUpgradeCost);
    }
}
