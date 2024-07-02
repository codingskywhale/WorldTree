using UnityEngine;

public class EnergyManager : MonoBehaviour
{
    public int energyAmount = 0;
    public int maxEnergy = 200;
    public float energyRechargeRate = 5f;
    private float energyRechargeTimer = 0f;

    public delegate void EnergyChanged(int newAmount);
    public event EnergyChanged OnEnergyChanged;

    private void Update()
    {
        RechargeEnergyOverTime();
    }

    private void RechargeEnergyOverTime()
    {
        energyRechargeTimer += Time.deltaTime;
        if (energyRechargeTimer >= 1f)
        {
            energyRechargeTimer = 0f;
            IncreaseEnergy((int)energyRechargeRate);
        }
    }

    public void IncreaseEnergy(int amount)
    {
        energyAmount = Mathf.Min(energyAmount + amount, maxEnergy);
        OnEnergyChanged?.Invoke(energyAmount);
    }

    public void DecreaseEnergy(int amount)
    {
        energyAmount = Mathf.Max(energyAmount - amount, 0);
        OnEnergyChanged?.Invoke(energyAmount);
    }
}
