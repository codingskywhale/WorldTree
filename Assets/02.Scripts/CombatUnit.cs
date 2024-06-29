using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Player,
    Enemy
}
public abstract class CombatUnit : MonoBehaviour
{
    public bool isMeetEnemy = false;
    public float attackSpeed;

    protected virtual void Update()
    {
        Move();
    }

    protected abstract void OnTriggerEnter2D(Collider2D collision);

    public IEnumerator Attack(UnitType type)
    {
        Debug.Log(type.ToString());
        // 공격 처리
        yield return new WaitForSeconds(attackSpeed);
    }

    public void Move()
    {
        if (!isMeetEnemy) this.transform.position += Vector3.left * Time.deltaTime;
    }
}
