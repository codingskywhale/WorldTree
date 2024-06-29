using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Friendly : CombatUnit
{
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("적 발견");
            isMeetEnemy = true;
            StartCoroutine(Attack(UnitType.Enemy));
        }
    }
}
