using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInputManager : MonoBehaviour
{
    public WaterManager waterManager;
    public UIManagerBG uIManagerBG;
    public int touchIncreaseLevel = 1;
    public int touchIncreaseAmount = 10;
    public int upgradeWaterCost = 20;

    private void Start()
    {
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isPointerOverUIElement())
        {
            waterManager.IncreaseWater(touchIncreaseAmount);
        }
    }

    private bool isPointerOverUIElement()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void UpdateUI()
    {
        uIManagerBG.UpdateTouchUI(touchIncreaseLevel, touchIncreaseAmount, upgradeWaterCost);
    }
}
