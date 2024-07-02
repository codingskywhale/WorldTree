using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    public Transform playerUnitSpawnTr;
    public Transform enemyUnitSpawnTr;

    public List<CombatUnit> playerCombatUnits;
    public List<CombatUnit> enemyCombatUnits; 

    public void SpawnPlayerUnit(string tag)
    {
        GameObject go = CombatManager.Instance.objectPool.SpawnFromPool(tag);
        go.transform.position = playerUnitSpawnTr.position;
        playerCombatUnits.Add(go.GetComponent<CombatUnit>());
    }

    public void StartSpawnEnemy()
    {
        StartCoroutine (SpawnEnemyUnit());
    }

    IEnumerator SpawnEnemyUnit()
    {
        while (true)
        {
            // 적 유닛 생성 로직 (일단 일정 시간마다 생성하는 식으로 할까?)
            GameObject go = CombatManager.Instance.objectPool.SpawnFromPool("red");
            go.transform.position = enemyUnitSpawnTr.position;
            enemyCombatUnits.Add(go.GetComponent<CombatUnit>());

            yield return new WaitForSeconds(0.5f);
        }
    }
}
