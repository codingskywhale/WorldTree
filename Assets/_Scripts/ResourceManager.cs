using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    public int waterAmount = 0;
    public int currentLevel = 1;
    public int waterPerLevel = 10; // ���� �� �ʿ��� ���� ��
    private const int maxWaterAmount = 200; // ���� �ִ� ��
    public TextMeshProUGUI waterText; // ���� ��ȭ���� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI levelText; // ���� ������ ǥ���� �ؽ�Ʈ
    public Image levelFillImage; // ���� �� �̹���

    void Start()
    {
        UpdateUI(); // �ʱ� UI ������Ʈ
    }

    // ���� ���� ������Ű�� �޼���
    public void IncreaseWater(int amount)
    {
        waterAmount += amount;        
        UpdateUI();
    }

    

    // UI�� ������Ʈ�ϴ� �޼���
    private void UpdateUI()
    {
        waterText.text = "Water: " + waterAmount;
        levelText.text = "Level: " + currentLevel;
        levelFillImage.fillAmount = (float)waterAmount / maxWaterAmount;
    }
}
