using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targets; // 목표 지점들
    public float speed = 5f; // 이동 속도
    public ResourceManager resourceManager; // ResourceManager 참조
    public int waterIncreaseAmount = 10; // 나무에 도착했을 때 증가하는 물의 양

    private int currentTargetIndex = 0;
    private bool isWaiting = false; // 대기 상태 확인

    void Update()
    {
        if (targets.Length == 0 || isWaiting)
            return;

        // 현재 목표 지점으로 이동
        Transform target = targets[currentTargetIndex];
        Vector3 direction = (target.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // 목표 지점에 도착했는지 확인
        if (Vector3.Distance(transform.position, target.position) < 0.1f)
        {
            StartCoroutine(HandleArrival(target));
        }
    }

    IEnumerator HandleArrival(Transform target)
    {
        // 나무 태그를 가진 오브젝트에 도착했는지 확인
        if (target.CompareTag("Tree"))
        {
            resourceManager.IncreaseWater(waterIncreaseAmount); // 물의 양 증가
        }

        // 물 태그를 가진 오브젝트에 도착했는지 확인
        if (target.CompareTag("Water"))
        {
            isWaiting = true; // 대기 상태로 전환
            yield return new WaitForSeconds(1f); // 1초 대기
            isWaiting = false; // 대기 상태 해제
        }

        // 다음 목표 지점으로 변경
        currentTargetIndex = (currentTargetIndex + 1) % targets.Length;
    }

    // 버튼 클릭 시 호출될 메서드
    public void IncreaseWaterAmountByButton()
    {
        waterIncreaseAmount += 10; // 나무에 도착했을 때 증가하는 물의 양 10 증가
    }
}
