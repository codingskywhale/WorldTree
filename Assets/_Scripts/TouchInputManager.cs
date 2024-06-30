using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    public ResourceManager resourceManager; // ResourceManager 참조
    public int IncreaseAmount = 10; // 클릭당 증가할 물의 양 초기값

    void Update()
    {
        // 터치 입력 감지
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 또는 터치 입력
        {
            resourceManager.IncreaseWater(IncreaseAmount); // 물의 양 증가
        }
    }

    // 강화 버튼 클릭 시 호출될 메서드
    public void UpgradeIncreaseAmount()
    {
        IncreaseAmount += 10; // 증가량을 10씩 증가시킴
    }
}
