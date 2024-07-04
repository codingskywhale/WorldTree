using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CombatUnit : Unit
{
    public CombatUnitDataSO combatUnitData;

    public GameObject nowTarget;

    // 근처에 있는 적들의 정보를 저장함. 
    public List<CombatUnit> nearEnemyList = new List<CombatUnit>();

    // 나를 공격 대상으로 삼은 적의 정보를 저장함.
    // 내가 죽었을 때에 나를 대상으로 하는 유닛들의 공격 대상 정보를 없애 주어야 함.
    public List<CombatUnit> targetingList = new List<CombatUnit>();
    public bool isAttackBase;

    protected override void SetInitialStat()
    {
        maxHP = combatUnitData.hp;
        nowHP = maxHP;
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CombatUnit enemyUnit))
        {
            // 중복 감지 방지 ( Player가 Player를 감지할 수 있음.)
            if (combatUnitData.unitType != enemyUnit.combatUnitData.unitType)
            {
                // 리스트에 넣은 뒤 한 마리씩 처리하기 위함.
                nearEnemyList.Add(enemyUnit);

                // 상대가 나를 공격하는 대상으로 지정
                enemyUnit.targetingList.Add(this);

                if (nowTarget == null && this.gameObject.activeInHierarchy) StartCoroutine(Attack());
            }
        }
        else if (collision.gameObject.TryGetComponent(out BaseUnit baseUnit))
        {
            if (combatUnitData.unitType != baseUnit.unitData.unitType)
            {
                isAttackBase = true;
                StartCoroutine(Attack(baseUnit));
            }
        }
    }

    IEnumerator Attack(BaseUnit baseUnit = null)
    {
        WaitForSeconds wait = new WaitForSeconds(combatUnitData.attackSpeed);

        if (nearEnemyList.Count > 0)
        {
            CombatUnit enemyUnit = nearEnemyList[0];
            nowTarget = enemyUnit.gameObject;

            // 둘 중 하나가 죽을 때 까지
            while (enemyUnit.nowHP > 0 && nowHP > 0)
            {
                ApplyDamage(enemyUnit);

                yield return wait;
            }
        }
        else if (isAttackBase)
        {
            while (isAttackBase && baseUnit.nowHP >= 0)
            {
                baseUnit.ApplyDamage(combatUnitData.attackDamage);

                yield return wait;
            }
        }
    }

    public void ApplyDamage(CombatUnit enemyUnit)
    {
        enemyUnit.nowHP -= combatUnitData.attackDamage;

        // 적이 죽었을 경우
        if (enemyUnit.nowHP <= 0)
        {
            enemyUnit.ResetSetting();
            enemyUnit.gameObject.SetActive(false);
            nowTarget = null;

            // 타겟팅 해제
            ReleaseTargeting(enemyUnit);

            if (nearEnemyList.Count > 0)
            {
                StartCoroutine(Attack());
            }
        }
    }

    //적이 죽었을 때, 근처 적을 체크하는 메서드
    public bool CheckNearEnemy()
    {
        RaycastHit2D hit = combatUnitData.unitType == UnitType.Player ? Physics2D.Raycast(this.transform.position, Vector2.left, 1f, 1 << LayerMask.NameToLayer("Enemy"))
                                                                  : Physics2D.Raycast(this.transform.position, Vector2.right, 1f, 1 << LayerMask.NameToLayer("Player"));

        if (hit)
        {
            CombatUnit enemyUnit = hit.collider.gameObject.GetComponent<CombatUnit>();

            if (combatUnitData.unitType != enemyUnit.combatUnitData.unitType)
            {
                nowTarget = hit.collider.gameObject;
                StartCoroutine(Attack());
                enemyUnit.targetingList.Add(this);

                return true;
            }
        }

        return false;
    }

    public void ResetSetting()
    {
        nearEnemyList.Clear();
        nowTarget = null;
        isAttackBase = false;
        SetInitialStat();
    }

    // 상대를 처치했을 때 그 적을 공격하던 모든 유닛의 타겟팅을 풀어준다.
    public void ReleaseTargeting(CombatUnit enemyUnit)
    {
        // 상대 유닛이 공격하던 적의 리스트를 삭제한다. (enemyUnit이 죽음)
        foreach (CombatUnit myUnit in enemyUnit.targetingList)
        {
            myUnit.nowTarget = null;
            myUnit.targetingList.Remove(enemyUnit);
        }
        // 자기를 공격하던 적 리스트 없애기.
        enemyUnit.targetingList.Clear();

        // nearEnemyList에 죽은 적의 정보를 가지고 있다면 모두 제거해야 한다. (내 근처의 적)
        foreach (CombatUnit myUnit in this.combatUnitData.unitType == UnitType.Player ? CombatManager.Instance.spawner.playerCombatUnits
                                                                                   : CombatManager.Instance.spawner.enemyCombatUnits)
        {
            if (myUnit.nearEnemyList.Contains(enemyUnit))
            {
                myUnit.nearEnemyList.Remove(enemyUnit);
            }
        }
    }
}
