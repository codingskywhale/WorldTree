using System.Collections;
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

    public GameObject nowTarget;

    private void Awake()
    {
        SetInitialStat();
    }

    private void SetInitialStat()
    {
        maxHP = combatUnitData.hp;
        nowHP = maxHP;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        // collision의 대상이 Player인지 Enemy인지 파악해야 한다.
        if (collision.gameObject.TryGetComponent(out CombatUnit enemyUnit))
        {
            if (combatUnitData.unitType != enemyUnit.combatUnitData.unitType)
            {
                nowTarget = collision.gameObject;
                isMeetEnemy = true;
                Debug.Log(enemyUnit.combatUnitData.unitName);
                StartCoroutine(Attack(enemyUnit));
            }
        }
    }

    IEnumerator Attack(CombatUnit enemyUnit)
    {
        // 둘 중 하나가 죽을 때 까지
        while (enemyUnit.nowHP > 0 && nowHP > 0)
        {
            ApplyDamage(enemyUnit);
            yield return new WaitForSeconds(combatUnitData.attackSpeed);
        }
    }

    public void ApplyDamage(CombatUnit unit)
    {
        unit.nowHP -= combatUnitData.attackDamage;
        if (unit.nowHP <= 0)
        {
            Destroy(unit.gameObject);
            isMeetEnemy = false;
            CheckNearEnemy();
        }
    }

    //적이 죽었을 때, 근처 적을 체크하는 메서드
    public void CheckNearEnemy()
    {
        Ray ray = new Ray();
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector2.left, 0.1f);
        if(hit)
        {
            nowTarget = hit.collider.gameObject;
            Debug.Log(hit.collider.gameObject.name + "히트가 진짜였다.");
            isMeetEnemy = true;
        }
    }
}
