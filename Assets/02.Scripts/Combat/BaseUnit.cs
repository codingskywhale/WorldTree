using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseUnit : Unit
{
    public UnitDataSO unitData;
    [SerializeField] TextMeshPro baseHPText;

    private void Awake()
    {
        InitSet();
    }

    protected override void SetInitialStat()
    {
        maxHP = unitData.hp;
        nowHP = maxHP;
    }

    private void InitSet()
    {
        SetInitialStat();
        UpdateHPText();
    }

    public void ApplyDamage(float damage)
    {
        if (nowHP > 0)
        {
            nowHP -= damage;
            UpdateHPText();
        }

        else
        {
            if(unitData.unitType == UnitType.Enemy) 
                CombatManager.Instance.ClearStage();
            else
                CombatManager.Instance.DefeatStage();
        }
    }

    private void UpdateHPText()
    {
        baseHPText.text = $"{nowHP}/{maxHP}";
    }
}
