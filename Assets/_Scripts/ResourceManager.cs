using TMPro;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public int waterAmount = 0;
    public TextMeshProUGUI WaterText;
    void Start()
    {
        UpdateWaterText(); // �ʱ� �ؽ�Ʈ ������Ʈ
    }

    // ���� ���� ������Ű�� �޼���
    public void IncreaseWater(int amount)
    {
        waterAmount += amount;
        UpdateWaterText();
    }

    // �ؽ�Ʈ ������Ʈ �޼���
    private void UpdateWaterText()
    {
        WaterText.text = "Water: " + waterAmount;
    }
}
