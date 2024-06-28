using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targets; // ��ǥ ������
    public float speed = 5f; // �̵� �ӵ�

    private int currentTargetIndex = 0;

    void Update()
    {
        if (targets.Length == 0)
            return;

        // ���� ��ǥ �������� �̵�
        Transform target = targets[currentTargetIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // ��ǥ ������ �����ߴ��� Ȯ��
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // ���� ��ǥ �������� ����
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        }
    }
}
