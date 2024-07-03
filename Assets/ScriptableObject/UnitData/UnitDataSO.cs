using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 기지 등 공격 능력이 없는 유닛
[CreateAssetMenu(fileName = "UnitData", menuName = "UnitData/DefaultUnit", order = 1)]
public class UnitDataSO : ScriptableObject
{
    public string unitName;
    public UnitType unitType;
    public float hp;
}
