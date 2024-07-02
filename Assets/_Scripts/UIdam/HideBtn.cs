using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HideBtn : MonoBehaviour, IPointerClickHandler
{
    public Canvas uiCanvas;
    public Button hideShowButton;

    private bool isUIVisible = true;

    private void Start()
    {
        hideShowButton.onClick.AddListener(HideBtnOn);
    }

    public void HideBtnOn()
    {
        uiCanvas.enabled = false;
        isUIVisible = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isUIVisible)
        {
            uiCanvas.enabled = true;
            isUIVisible = true;
        }
    }
}
