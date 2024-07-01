using UnityEngine;

[CreateAssetMenu(fileName = "StageData", menuName = "New StageData")]
public class CombatStageDataSO : ScriptableObject
{
    public int stageIdx;
    public string stageName;
    public float friendlyBaseHP;
    public float enemyBaseHP;
}
