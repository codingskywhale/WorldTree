using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CombatUnitData", menuName = "UnitData/CombatUnit", order = 0)]
public class CombatUnitDataSO : UnitDataSO
{
    public float attackDamage;
    public float moveSpeed;
    public float attackSpeed;
    public Sprite unitIcon;
    public GameObject unitPrefab;
}
