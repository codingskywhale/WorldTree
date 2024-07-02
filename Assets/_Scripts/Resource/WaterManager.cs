using UnityEngine;

public class WaterManager : MonoBehaviour
{
    public int waterAmount = 0;
    public int currentLevel = 1;
    public int waterPerLevel = 10;

    public delegate void WaterChanged(int newAmount);
    public event WaterChanged OnWaterChanged;

    public void IncreaseWater(int amount)
    {
        waterAmount += amount;
        OnWaterChanged?.Invoke(waterAmount);
    }

    public void DecreaseWater(int amount)
    {
        waterAmount -= amount;
        OnWaterChanged?.Invoke(waterAmount);
    }

    public bool HasSufficientWater(int requiredAmount)
    {
        return waterAmount >= requiredAmount;
    }

    public int CalculateWaterNeededForUpgrade(int amount)
    {
        return (currentLevel + amount) * waterPerLevel;
    }
}
