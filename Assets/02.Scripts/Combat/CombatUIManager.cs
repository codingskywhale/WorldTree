using UnityEngine;

public class CombatUIManager : MonoBehaviour
{
    private static CombatUIManager _instance;
    public static CombatUIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("CombatUIManager").AddComponent<CombatUIManager>();
            }
            return _instance;
        }
    }

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
    }

    public SpawnButton[] spawnButtons;
    public GameObject stageSelectWnd;
    
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

    // 가격이 충분하다면 해당 버튼을 On 시켜준다.
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

    public void StageSelectWndOff()
    {
        stageSelectWnd.SetActive(false);
    }
}
