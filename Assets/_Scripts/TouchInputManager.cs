using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    public ResourceManager resourceManager; // ResourceManager 참조

    void Update()
    {
        // 터치 입력 감지
        if (Input.GetMouseButtonDown(0)) // 마우스 왼쪽 버튼 클릭 또는 터치 입력
        {
            resourceManager.IncreaseWater(10); // 물의 양 증가
        }
    }
}

