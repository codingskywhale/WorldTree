using TMPro;
using UnityEngine;

public class CombatBase : MonoBehaviour
{
    public float baseNowHP;
    private float baseMaxHP;

    [SerializeField] TextMeshPro baseHPText;
    BaseUnit unit;

    private void Awake()
    {
        unit = GetComponent<BaseUnit>();
    }
    private void Start()
    {
        InitSet();
    }

    private void InitSet()
    {
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
