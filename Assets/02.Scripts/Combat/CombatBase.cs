using TMPro;
using UnityEngine;

public class CombatBase : MonoBehaviour
{
    public float baseNowHP;
    private float baseMaxHP;

    [SerializeField] TextMeshPro baseHPText;

    private void Start()
    {
        InitSet();
    }

    private void InitSet()
    {
        baseMaxHP = this.CompareTag("Player") ? CombatManager.Instance.stageData.friendlyBaseHP : CombatManager.Instance.stageData.enemyBaseHP;
        baseNowHP = baseMaxHP;
        UpdateHPText();
    }
    public void ApplyBaseHPDecrease(float value)
    {
        baseNowHP -= Mathf.Min(baseNowHP + value, baseMaxHP);
        UpdateHPText();
    }

    private void UpdateHPText()
    {
        baseHPText.text = $"{baseNowHP}/{baseMaxHP}";
    }
}
