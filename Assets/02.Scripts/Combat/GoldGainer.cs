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
    public void StartGainGold()
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
    }
}
