using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIPlacementManager : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private PlacementManager placementManager;
    [SerializeField] private Button placementButton;
    [SerializeField] private Button installButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private Button leftArrowButton;
    [SerializeField] private Button rightArrowButton;
    [SerializeField] private Button[] itemButtons;
    [SerializeField] private GameObject placementInventoryPanel;
    [SerializeField] private GameObject placementActionPanel;

    private int currentStartIndex = 0;

    void Start()
    {
        placementButton.onClick.AddListener(ShowItemSlots);
        leftArrowButton.onClick.AddListener(ShowPreviousItems);
        rightArrowButton.onClick.AddListener(ShowNextItems);
        installButton.onClick.AddListener(InstallObject);
        cancelButton.onClick.AddListener(CancelPlacement);
        UpdateItemButtons();
        HideItemSlots();
        HidePlacementActions();
    }

    void ShowItemSlots()
    {
        placementButton.gameObject.SetActive(false); // Placement 버튼 비활성화
        placementInventoryPanel.SetActive(true);    // 아이템 슬롯 패널 활성화
        placementActionPanel.SetActive(false);      // 설치 및 취소 패널 비활성화
    }

    void HideItemSlots()
    {
        placementInventoryPanel.SetActive(false);   // 아이템 슬롯 패널 비활성화
    }

    void ShowPlacementActions()
    {
        placementActionPanel.SetActive(true);       // 설치 및 취소 패널 활성화
    }

    void HidePlacementActions()
    {
        placementActionPanel.SetActive(false);      // 설치 및 취소 패널 비활성화
    }

    void ShowPreviousItems()
    {
        if (currentStartIndex > 0)
        {
            currentStartIndex -= 4;
            UpdateItemButtons();
        }
    }

    void ShowNextItems()
    {
        if (currentStartIndex + 4 < inventory.GetItemCount())
        {
            currentStartIndex += 4;
            UpdateItemButtons();
        }
    }

    void UpdateItemButtons()
    {
        List<Item> visibleItems = inventory.GetVisibleItems(currentStartIndex, 4);
        for (int i = 0; i < itemButtons.Length; i++)
        {
            if (i < visibleItems.Count)
            {
                itemButtons[i].gameObject.SetActive(true);
                Image itemIcon = itemButtons[i].transform.Find("ItemIcon").GetComponent<Image>();

                if (visibleItems[i].icon != null)
                {
                    itemIcon.sprite = visibleItems[i].icon;
                    itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 1f); // 아이템 아이콘 보이기
                }
                else
                {
                    Debug.LogError("Icon not found for item: " + visibleItems[i].itemName);
                }

                int index = i; // 로컬 변수로 캡처
                itemButtons[i].onClick.RemoveAllListeners();
                itemButtons[i].onClick.AddListener(() => SelectItem(visibleItems[index]));
            }
            else
            {
                itemButtons[i].gameObject.SetActive(true); // 빈 슬롯도 활성화
                Image itemIcon = itemButtons[i].transform.Find("ItemIcon").GetComponent<Image>();
                itemIcon.sprite = null;
                itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 0f); // 아이템 아이콘 숨기기
                itemButtons[i].onClick.RemoveAllListeners();
            }
        }
        UpdateArrowButtons();
    }

    void SelectItem(Item item)
    {
        placementManager.SelectItem(item);
        HideItemSlots();
        ShowPlacementActions();
    }

    void UpdateArrowButtons()
    {
        leftArrowButton.interactable = currentStartIndex > 0;
        rightArrowButton.interactable = currentStartIndex + 4 < inventory.GetItemCount();
    }

    void InstallObject()
    {
        placementManager.InstallObject();
        placementButton.gameObject.SetActive(true); // Placement 버튼 다시 활성화
        HidePlacementActions();
    }

    void CancelPlacement()
    {
        placementManager.CancelPlacement();
        placementButton.gameObject.SetActive(true); // Placement 버튼 다시 활성화
        ShowItemSlots();                             // 아이템 슬롯 패널 다시 표시
    }
}
