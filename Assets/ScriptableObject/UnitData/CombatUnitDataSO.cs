using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatUnitData", menuName = "new CombatUnitData")]
public class CombatUnitDataSO : ScriptableObject
{
    public string unitName;
    public UnitType unitType;
    public float hp;
    public float attackDamage;
    public float moveSpeed;
    public float attackSpeed;
}
