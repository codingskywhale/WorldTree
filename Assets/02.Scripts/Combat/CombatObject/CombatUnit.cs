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

    public GameObject nowTarget;

    bool isBase = false;

    // 나를 공격 대상으로 삼은 적의 정보를 저장함.
    // 내가 죽었을 때에 나를 대상으로 하는 유닛들의 공격 대상 정보를 없애 주어야 함.
    public List<CombatUnit> targetingList = new List<CombatUnit>();

    private void Awake()
    {
        SetInitialStat();
    }

    private void Update()
    {
        if (combatUnitData.unitType == UnitType.Player)
        {
            Debug.DrawRay(transform.position, new Vector3(-1, 0, 0), new Color(0, 1, 0));
        }
        else
        {
            Debug.DrawRay(transform.position, new Vector3(1, 0, 0), new Color(0, 1, 0));
        }
    }
    private void SetInitialStat()
    {
        maxHP = combatUnitData.hp;
        nowHP = maxHP;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out CombatUnit enemyUnit))
        {
            // 중복 감지 방지 ( Player가 Player를 감지할 수 있음.)
            if (combatUnitData.unitType != enemyUnit.combatUnitData.unitType && nowTarget == null)
            {
                nowTarget = collision.gameObject;
                isMeetEnemy = true;
                enemyUnit.targetingList.Add(this);
                if(this.gameObject.activeSelf) StartCoroutine(Attack(enemyUnit));
            }
        }
    }

    IEnumerator Attack(CombatUnit enemyUnit)
    {
        if(enemyUnit.TryGetComponent(out CombatBase enemyBase)) isBase = true;

        // 둘 중 하나가 죽을 때 까지
        while (enemyUnit.nowHP > 0 && nowHP > 0)
        {
            ApplyDamage(enemyUnit);

            if (isBase) enemyBase.ApplyBaseHPDecrease(combatUnitData.attackDamage);

            yield return new WaitForSeconds(combatUnitData.attackSpeed);
        }
    }

    public void ApplyDamage(CombatUnit enemyUnit)
    {
        enemyUnit.nowHP -= combatUnitData.attackDamage;
        if (enemyUnit.nowHP <= 0)
        {
            if (!isBase)
            {
                enemyUnit.ResetSetting();
                enemyUnit.gameObject.SetActive(false);
                nowTarget = null;
                ReleaseTargeting(enemyUnit);
                isMeetEnemy = CheckNearEnemy(); 
            }
            else
            {
                if (enemyUnit.combatUnitData.unitType == UnitType.Enemy) CombatManager.Instance.ClearStage();

                else CombatManager.Instance.DefeatStage();
            }
        }
    }

    //적이 죽었을 때, 근처 적을 체크하는 메서드
    public bool CheckNearEnemy()
    {
        RaycastHit2D hit = combatUnitData.unitType == UnitType.Player ? Physics2D.Raycast(this.transform.position, Vector2.left, 1f)
                                                                      : Physics2D.Raycast(this.transform.position, Vector2.right, 1f);

        CombatUnit enemyUnit = hit.collider.gameObject.GetComponent<CombatUnit>();

        if (hit && combatUnitData.unitType != enemyUnit.combatUnitData.unitType)
        {
            nowTarget = hit.collider.gameObject;
            isMeetEnemy = true;
            StartCoroutine(Attack(hit.collider.gameObject.GetComponent<CombatUnit>()));
            enemyUnit.targetingList.Add(this);
        }

        return hit;
    }

    public void ResetSetting()
    {
        isMeetEnemy = false;
        nowTarget = null;
        SetInitialStat();
    }

    // 상대를 처치했을 때 그 적을 대상으로 공격하던 모든 유닛의 타겟팅을 풀어준다.
    public void ReleaseTargeting(CombatUnit enemyUnit)
    {
        foreach (CombatUnit unit in enemyUnit.targetingList)
        {
            unit.nowTarget = null;
        }
    }
}
