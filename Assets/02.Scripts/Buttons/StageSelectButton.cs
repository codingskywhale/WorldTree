using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectButton : MonoBehaviour
{
    // 해당 스테이지 데이터를 가지고 있도록
    // 버튼을 누르면 해당 스테이지 데이터를 CombatManager에 세팅해 줄 수 있도록
    // 씬이 별도로 존재하므로 넘겨줄 수 있는 로직또한 필요함.
    public CombatStageDataSO selectStageData;

    public void SetStageData()
    {
        CombatManager.Instance.stageData = selectStageData;
        CombatManager.Instance.StartGame();
        CombatUIManager.Instance.StageSelectWndOff();
    }
}
