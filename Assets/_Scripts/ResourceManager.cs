using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int waterAmount = 0;

    // 물의 양을 증가시키는 메서드
    public void IncreaseWater(int amount)
    {
        waterAmount += amount;
        Debug.Log("Water increased. Current amount: " + waterAmount);
    }
}
