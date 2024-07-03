using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Player,
    Enemy
}

public abstract class Unit : MonoBehaviour
{
    public float nowHP;
    public float maxHP;

    private void Awake()
    {
        SetInitialStat();
    }

    protected abstract void SetInitialStat();
}
