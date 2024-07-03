using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public Transform[] targets; // 목표 지점들
    public float speed = 5f; // 이동 속도
    public WaterManager waterManager; // waterManager 참조
    public UIManagerBG uiManager; // UIManagerBG 참조
    public int waterIncreaseAmount = 10; // 나무에 도착했을 때 증가하는 물의 양
    public int waterIncreaseLevel = 1; // 물 증가량 강화 레벨
    public int waterIncreaseUpgradeCost = 20; // 물 증가량 강화에 필요한 물의 양
    public int moveSpeedLevel = 1; // 이동 속도 강화 레벨
    public int moveUpgradeCost = 20; // 이동 속도 강화 비용

    private int currentTargetIndex = 0;
    private bool isWaiting = false; // 대기 상태 확인

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

    private IEnumerator HandleArrival(Transform target)
    {
        // 나무 태그를 가진 오브젝트에 도착했는지 확인
        if (target.CompareTag("Tree"))
        {
            waterManager.IncreaseWater(waterIncreaseAmount); // 물의 양 증가
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

    // UI 업데이트 메서드
    private void UpdateUI()
    {
        // UIManagerBG를 통해 UI 업데이트
        uiManager.UpdateMovementUI(moveSpeedLevel, moveUpgradeCost);
        uiManager.UpdateWaterIncreaseUI(waterIncreaseLevel, waterIncreaseAmount, waterIncreaseUpgradeCost);
    }
}
