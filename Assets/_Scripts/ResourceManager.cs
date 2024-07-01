using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [Header("Resource Fields")]
    // �ʵ� ����
    public int waterAmount = 0;
    public int currentLevel = 1;
    public int waterPerLevel = 10; // ���� �� �ʿ��� ���� ��
    [Header("UI Elements")]
    // UI ��� ����
    public TextMeshProUGUI waterText; // ���� ��ȭ���� ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI levelText; // ���� ������ ǥ���� �ؽ�Ʈ
    public TextMeshProUGUI touchLevelText; // ��ġ ��ȭ ������ ǥ���� UI �ؽ�Ʈ
    //public Text touchIncreaseAmountText; // ��ġ �������� ǥ���� UI �ؽ�Ʈ
    public TextMeshProUGUI waterIncreaseLevelText;
    public TextMeshProUGUI waterIncreaseUpgradeCostText;
    public TextMeshProUGUI upgradeWaterCostText; // ��ȭ ����� ǥ���� UI �ؽ�Ʈ
    public Image levelFillImage; // ���� �� �̹���
    public Image currentTreeImage; // ���� ���� �̹���
    public Image upgradedTreeImage; // ��ȭ�� ���� �̹���
    public SpriteRenderer outsideTreeSpriteRenderer; // �ۿ� �ִ� ���� �̹���
    public TextMeshProUGUI upgradeRequirementText; // ��ȭ�� �ʿ��� ��ȭ�� ǥ���� �ؽ�Ʈ
    public SpriteRenderer groundSpriteRenderer; // ������ SpriteRenderer ������Ʈ
    public Sprite[] treeImages; // ���� �̹����� ���� �迭

    // �ʱ� ����
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

    // ���� ���� ���ҽ�Ű�� �޼���
    public void DecreaseWater(int amount)
    {
        waterAmount -= amount;
        UpdateUI();
    }

    // ������ ���׷��̵��ϴ� �޼���
    public void UpgradeLevel(int amount)
    {
        int waterNeededForUpgrade = (currentLevel + amount) * waterPerLevel;

        if (waterAmount >= waterNeededForUpgrade)
        {
            DecreaseWater(waterNeededForUpgrade);
            currentLevel += amount;
            UpdateUI();
            UpdateGroundSize();
        }
        else
        {
            Debug.Log("���� �����Ͽ� ��ȭ�� �� �����ϴ�.");
        }
    }

    // ���� ũ�⸦ ������Ʈ�ϴ� �޼���
    private void UpdateGroundSize()
    {
        float groundScale = 8f + (currentLevel / 10f); // ������ ���� ���� ũ�� ���
        groundSpriteRenderer.transform.localScale = new Vector3(groundScale, groundScale, groundScale);
    }

    // UI�� ������Ʈ�ϴ� �޼���
    public void UpdateUI()
    {
        // ���� ������ �ʿ��� ���� �� ���
        int waterNeededForCurrentLevel = (currentLevel + 1) * waterPerLevel;
        waterText.text = $"Water: {waterAmount}";
        levelText.text = $"Level: {currentLevel}";
        levelFillImage.fillAmount = (float)waterAmount / waterNeededForCurrentLevel;

        // ���� �̹��� ������Ʈ
        UpdateTreeImages();

        // ���׷��̵忡 �ʿ��� ��ȭ �ؽ�Ʈ ������Ʈ
        upgradeRequirementText.text = $"�ʿ��� ��ȭ: {waterNeededForCurrentLevel} �� ";
    }

    // ���� �̹����� ������Ʈ�ϴ� �޼���
    private void UpdateTreeImages()
    {
        int currentIndex = currentLevel / 5;
        int nextIndex = (currentLevel + 1) / 5;

        currentTreeImage.sprite = treeImages[currentIndex];
        upgradedTreeImage.sprite = treeImages[nextIndex];
        outsideTreeSpriteRenderer.sprite = treeImages[currentIndex];
    }
}
