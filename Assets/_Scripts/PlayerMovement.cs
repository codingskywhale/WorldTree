using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targets; // 목표 지점들
    public float speed = 5f; // 이동 속도

    private int currentTargetIndex = 0;

    void Update()
    {
        if (targets.Length == 0)
            return;

        // 현재 목표 지점으로 이동
        Transform target = targets[currentTargetIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // 목표 지점에 도착했는지 확인
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            // 다음 목표 지점으로 변경
            currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
        }
    }
}
