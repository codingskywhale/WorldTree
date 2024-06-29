using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CombatUnit
{
    protected override void Update()
    {
        if (!isMeetEnemy) this.transform.position += Vector3.right * Time.deltaTime;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어 발견");
            isMeetEnemy = true;
            StartCoroutine(Attack(UnitType.Player));
        }
    }
}
