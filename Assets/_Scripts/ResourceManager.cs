using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int waterAmount = 0;

    // ���� ���� ������Ű�� �޼���
    public void IncreaseWater(int amount)
    {
        waterAmount += amount;
        Debug.Log("Water increased. Current amount: " + waterAmount);
    }
}
