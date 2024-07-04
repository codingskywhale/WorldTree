using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGameManager : MonoBehaviour
{
    private static TestGameManager _instance;
    public static TestGameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("TestGameManager").AddComponent<TestGameManager>();
            }
            return _instance;
        }
    }

    public CombatStageDataSO stageData;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }
}
