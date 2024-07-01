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
    public Action GoldGetEvent;

    // 전투 관련 로직들을 담는 매니저 (전투 시작 / 종료)
    // 맵 내 오브젝트 들의 관리를 담당하자.
    // 스포너는 별도로?

    public int nowStageIdx;
    public string nowStageName;
    private float nowGold;

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

        SetStage();
        GoldGetEvent += IncreaseGold;
    }

    public void IncreaseGold()
    {
        nowGold += 5;
        // 골드가 오르면 UI에도 적용이 되어야 한다.
    }

    public void StartGame()
    {
        // 게임 시작 시 실행되어야 할 내용들
        // 게임 시작 시 골드 설정
        // 골드 생성 시작
        // 스테이지 정보 적용 (적 기지 체력, 스폰 몬스터 수 등)
    }

    private void SetStage()
    {
        nowStageIdx = stageData.stageIdx;
        nowStageName = stageData.stageName;
    }
    public void ClearStage()
    {
        Time.timeScale = 0;
        Debug.Log("게임 클리어!!");
        stageResultWnd.gameObject.SetActive(true);
        stageResultWnd.SetClear();
        // 게임 클리어 시 실행될 내용들
        // 게임 클리어 글씨 출력
        // 보상 획득 창 출력
        // 창 내에 메인 씬으로 돌아가는 버튼 만들기.
    }

    public void DefeatStage()
    {
        Time.timeScale = 0;
        Debug.Log("게임 패배...");
        stageResultWnd.gameObject.SetActive(true);
        stageResultWnd.SetDefeat();
        // 게임 패배 시 실행될 내용들
        // 패배 관련 글씨 출력
        // 메뉴로 돌아가기
    }
}
