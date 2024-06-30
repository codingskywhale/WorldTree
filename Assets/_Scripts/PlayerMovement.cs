using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targets; // ��ǥ ������
    public float speed = 5f; // �̵� �ӵ�
    public ResourceManager resourceManager; // ResourceManager ����
    public int waterIncreaseAmount = 10; // ������ �������� �� �����ϴ� ���� ��

    private int currentTargetIndex = 0;
    private bool isWaiting = false; // ��� ���� Ȯ��

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
            resourceManager.IncreaseWater(waterIncreaseAmount); // ���� �� ����
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
    public void IncreaseWaterAmountByButton()
    {
        waterIncreaseAmount += 10; // ������ �������� �� �����ϴ� ���� �� 10 ����
    }
}
