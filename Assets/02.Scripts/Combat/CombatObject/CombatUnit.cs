using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    Player,
    Enemy
}
public class CombatUnit : MonoBehaviour
{
    public bool isMeetEnemy = false;
    public CombatUnitDataSO combatUnitData;
    public float nowHP;
    private float maxHP;

    private void Awake()
    {
        SetInitialStat();
    }
    protected virtual void Update()
    {
        Move();
    }

    private void SetInitialStat()
    {
        maxHP = combatUnitData.hp;
        nowHP = maxHP;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        // collision의 대상이 Player인지 Enemy인지 파악해야 한다.
        if (collision.gameObject.TryGetComponent(out CombatUnit combatUnit))
        {
            if(combatUnit.combatUnitData.unitType == UnitType.Enemy)
            {
                isMeetEnemy = true;
                Debug.Log(combatUnit.combatUnitData.unitName);
                StartCoroutine(Attack(combatUnit));
            }
        }
    }

    IEnumerator Attack(CombatUnit unit)
    {
        // 둘 중 하나가 죽을 때 까지
        while (unit.nowHP > 0 && nowHP > 0)
        {
            ApplyDamage(unit);
            Debug.Log(unit.nowHP);
            yield return new WaitForSeconds(combatUnitData.attackSpeed);
        }
    }

    public void Move()
    {
        if (!isMeetEnemy) 
        {
            this.transform.position +=
                (combatUnitData.unitType == UnitType.Player) ? Vector3.left * Time.deltaTime * combatUnitData.moveSpeed: 
                                                               Vector3.right * Time.deltaTime * combatUnitData.moveSpeed;
        }                           
    }

    public void ApplyDamage(CombatUnit unit)
    {
        unit.nowHP -= combatUnitData.attackDamage;
        if(unit.nowHP <= 0)
        {
            Destroy(unit.gameObject);
            isMeetEnemy = false;
        }
    }
}
