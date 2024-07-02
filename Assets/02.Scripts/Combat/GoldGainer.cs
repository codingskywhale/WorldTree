using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldGainer : MonoBehaviour
{
    public float nowGold { get; set; }
    private float goldIncreaseAmount = 5f;
    private float goldIncreaseDelay = 0.1f;

    public TextMeshProUGUI goldText;
    private void Start()
    {
        StartCoroutine(IncreaseGold());
    }

    IEnumerator IncreaseGold()
    {
        WaitForSeconds waitTime = new WaitForSeconds(goldIncreaseDelay);
        while (true)
        {
            nowGold += goldIncreaseAmount;

            goldText.text = $"Gold : {nowGold}";
            yield return waitTime;
        }
        // 골드가 오르면 UI에도 적용이 되어야 한다.
        // 버튼 UI 및 상단 보유 골드량도 늘어나야 함.
        // 구매 가능한 경우 버튼을 상호작용 가능하도록 변경해주자.
    }
}
