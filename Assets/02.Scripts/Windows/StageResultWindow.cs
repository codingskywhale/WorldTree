using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageResultWindow : MonoBehaviour
{
    public TextMeshProUGUI stageResultText;
    public Image rewardImage;

    public void SetClear()
    {
        stageResultText.text = "Stage Clear!!";
        // 보상 아이템으로 이미지 변경
    }

    public void SetDefeat()
    {
        stageResultText.text = "Clear Failed...";
        // 쓰러진 캐릭터로 이미지 변경
    }
}
