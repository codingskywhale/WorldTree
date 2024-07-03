using System;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    private static CombatManager _instance;
    public static CombatManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CombatManager").AddComponent<CombatManager>();
            }
            return _instance;
        }
    }

    public CombatStageDataSO stageData;
    public StageResultWindow stageResultWnd;
    public ObjectPool objectPool;
    public Spawner spawner;
    public GoldGainer gainer;
    public PlayerUnitDataSO[] unitDatas;

    public Action OnGameStart;
    int nowStageIdx;
    string nowStageName;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

        gainer = GetComponent<GoldGainer>();
    }

    public void StartGame()
    {
        SetPlayerUnitData();
        SetStage();
        gainer.StartGainGold();
    }

    private void SetPlayerUnitData()
    {
        // 오브젝트 풀에 데이터 추가
        for (int i = 0; i < unitDatas.Length; i++)
        {
            objectPool.AddtoPool(unitDatas[i].unitName, unitDatas[i].unitPrefab, 10);
        }
        // 게임 시작 시 실행되어야 할 내용들
        // 게임 시작 시 골드 설정
        // 골드 생성 시작
        // 스테이지 정보 적용 (적 기지 체력, 스폰 몬스터 수 등)
    }

    private void SetStage()
    {
        // 적을 오브젝트 풀에 미리 추가하기. (적 정보는 스테이지 정보에 있음)
        for (int i = 0; i < stageData.enemyUnitDatas.Length; i++)
        {
            CombatUnitDataSO unitData = stageData.enemyUnitDatas[i];
            objectPool.AddtoPool(unitData.unitName, unitData.unitPrefab, 20);
        }
        spawner.StartSpawnEnemy();
    }
    public void ClearStage()
    {
        Time.timeScale = 0;
        Debug.Log("게임 클리어!!");
        stageResultWnd.gameObject.SetActive(true);
        stageResultWnd.SetClear();
    }

    public void DefeatStage()
    {
        Time.timeScale = 0;
        Debug.Log("게임 패배...");
        stageResultWnd.gameObject.SetActive(true);
        stageResultWnd.SetDefeat();
    }
}
