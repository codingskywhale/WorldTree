using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatUnitData", menuName = "CombatUnitData/Default", order = 0)]
public class CombatUnitDataSO : ScriptableObject
{
    public string unitName;
    public UnitType unitType;
    public float hp;
    public float attackDamage;
    public float moveSpeed;
    public float attackSpeed;
    public Sprite unitIcon;
    public GameObject unitPrefab;
}
