using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
    [SerializeField] private Button backButton;
    [SerializeField] private GameObject errorPopup;
    [SerializeField] private TMP_Text currentItemText;

    private int currentStartIndex = 0;

    private void Start()
    {
        placementButton.onClick.AddListener(ShowItemSlots);
        leftArrowButton.onClick.AddListener(ShowPreviousItems);
        rightArrowButton.onClick.AddListener(ShowNextItems);
        installButton.onClick.AddListener(AttemptInstallObject);
        cancelButton.onClick.AddListener(CancelPlacement);
        backButton.onClick.AddListener(HideItemSlots);
        UpdateItemButtons();
        HideItemSlots();
        HidePlacementActions();
    }

    private void ShowItemSlots()
    {
        placementButton.gameObject.SetActive(false); // Placement 버튼 비활성화
        placementInventoryPanel.SetActive(true);    // 아이템 슬롯 패널 활성화
        placementActionPanel.SetActive(false);      // 설치 및 취소 패널 비활성화
    }

    private void HideItemSlots()
    {
        placementButton.gameObject.SetActive(true); // Placement 버튼 활성화
        placementInventoryPanel.SetActive(false);   // 아이템 슬롯 패널 비활성화
    }

    private void ShowPlacementActions()
    {
        placementButton.gameObject.SetActive(false); // Placement 버튼 비활성화
        placementActionPanel.SetActive(true);        // 설치 및 취소 패널 활성화
    }

    private void HidePlacementActions()
    {
        placementActionPanel.SetActive(false);       // 설치 및 취소 패널 비활성화        
    }

    private void ShowPreviousItems()
    {
        if (currentStartIndex > 0)
        {
            currentStartIndex -= 4;
            UpdateItemButtons();
        }
    }

    private void ShowNextItems()
    {
        if (currentStartIndex + 4 < inventory.GetItemCount())
        {
            currentStartIndex += 4;
            UpdateItemButtons();
        }
    }

    private void UpdateItemButtons()
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

    private void SelectItem(Item item)
    {
        placementManager.SelectItem(item);
        HideItemSlots();
        ShowPlacementActions();
        UpdateCurrentItemText(item.itemName);
    }

    private void UpdateArrowButtons()
    {
        leftArrowButton.interactable = currentStartIndex > 0;
        rightArrowButton.interactable = currentStartIndex + 4 < inventory.GetItemCount();
    }

    private void AttemptInstallObject()
    {
        bool success = placementManager.InstallObject();
        if (success)
        {
            UpdateItemButtons();
            HidePlacementActions();
            ShowItemSlots(); 
        }
        else
        {
            ShowErrorPopup("You cannot install it here!!");
        }
    }

    private void CancelPlacement()
    {
        placementManager.CancelPlacement();
        HidePlacementActions();
        ShowItemSlots();
        UpdateCurrentItemText("");
    }

    public void ShowErrorPopup(string message)
    {
        if (errorPopup != null)
        {
            errorPopup.SetActive(true);
            errorPopup.GetComponentInChildren<TMP_Text>().text = message;
            StartCoroutine(HideErrorPopup());
        }
    }
        
    private IEnumerator HideErrorPopup()
    {
        yield return new WaitForSeconds(1f); 
        errorPopup.SetActive(false);
    }

    private void UpdateCurrentItemText(string itemName)
    {
        if (currentItemText != null)
        {
            currentItemText.text = itemName;
        }
    }
}
