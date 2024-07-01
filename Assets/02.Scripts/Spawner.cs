using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    public Transform playerUnitSpawnTr;
    public Transform enemyUnitSpawnTr;

    private void Start()
    {
        StartCoroutine(SpawnEnemyUnit());
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SpawnPlayerUnit();
        }
    }

    private void SpawnPlayerUnit()
    {
        GameObject go = CombatManager.Instance.objectPool.SpawnFromPool("Player");
        go.transform.position = playerUnitSpawnTr.position;
    }

    IEnumerator SpawnEnemyUnit()
    {
        while (true)
        {
            // 적 유닛 생성 로직 (일단 일정 시간마다 생성하는 식으로 할까?)
            GameObject go = CombatManager.Instance.objectPool.SpawnFromPool("Enemy");
            go.transform.position = enemyUnitSpawnTr.position;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
