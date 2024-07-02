using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnButton : MonoBehaviour
{
    public Image bgImage;
    public TextMeshProUGUI characterNameText;
    public Image characterIcon;
    public TextMeshProUGUI requireGoldText;

    private Button spawnButton;

    public bool isInDealyTime = false;

    private void Awake()
    {
        spawnButton = GetComponent<Button>();
    }

    public void SetSpawnButtonInfo(PlayerUnitDataSO unitData)
    {
        characterNameText.text = unitData.unitName;
        requireGoldText.text = unitData.cost.ToString();
        characterIcon.sprite = unitData.unitIcon;
    }

    public void SetButtonInteractable(bool on)
    {
        spawnButton.interactable = on;
    }

    public void ClickSpawnPlayerUnitButton(int buttonIdx)
    {
        CombatManager.Instance.gainer.nowGold -= CombatManager.Instance.unitDatas[buttonIdx].cost;
        CombatManager.Instance.spawner.SpawnPlayerUnit(CombatManager.Instance.unitDatas[buttonIdx].unitName);
        StartCoroutine(DelayUnitGenerateTime());
    }

    private IEnumerator DelayUnitGenerateTime()
    {
        isInDealyTime = true;
        SetButtonInteractable(false);

        Color color = bgImage.color;
        color.a = 0;

        while (color.a < 1)
        {
            // TODO : 스폰 딜레이 타임 적용
            color.a += 0.1f;

            bgImage.color = color;
            yield return new WaitForSeconds(0.5f);
        }

        isInDealyTime = false;
    }
}
