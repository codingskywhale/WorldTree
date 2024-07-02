using UnityEngine;

public class CombatUIManager : MonoBehaviour
{
    public SpawnButton[] spawnButtons;
    GoldGainer goldGainer;

    private void Start()
    {
        SetInitialData();
        goldGainer = CombatManager.Instance.gainer;
    }

    private void Update()
    {
        CheckEnoughGold();
    }

    public void SetInitialData()
    {
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            spawnButtons[i].SetSpawnButtonInfo(CombatManager.Instance.unitDatas[i]);
        }
    }

    private void CheckEnoughGold()
    {
        for (int i = 0; i < spawnButtons.Length; i++)
        {
            if (goldGainer.nowGold > CombatManager.Instance.unitDatas[i].cost && !spawnButtons[i].isInDealyTime)
            {
                spawnButtons[i].SetButtonInteractable(true);
            }
            else
            {
                spawnButtons[i].SetButtonInteractable(false);
            }
        }
    }
}
