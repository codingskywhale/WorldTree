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
        placementButton.gameObject.SetActive(false); 
        placementInventoryPanel.SetActive(true);    
        placementActionPanel.SetActive(false);      
    }

    private void HideItemSlots()
    {
        placementButton.gameObject.SetActive(true); 
        placementInventoryPanel.SetActive(false);   
    }

    private void ShowPlacementActions()
    {
        placementButton.gameObject.SetActive(false); 
        placementActionPanel.SetActive(true);        
    }

    private void HidePlacementActions()
    {
        placementActionPanel.SetActive(false);
        UpdateCurrentItemText("");
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
                UpdateButton(itemButtons[i], visibleItems[i], i);
            }
            else
            {
                ClearButton(itemButtons[i]);
            }
        }
        UpdateArrowButtons();
    }

    private void UpdateButton(Button button, Item item, int index)
    {
        button.gameObject.SetActive(true);
        Image itemIcon = button.transform.Find("ItemIcon").GetComponent<Image>();

        if (item.icon != null)
        {
            itemIcon.sprite = item.icon;
            itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 1f);
        }        

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => SelectItem(item));
    }

    private void ClearButton(Button button)
    {
        button.gameObject.SetActive(true);
        Image itemIcon = button.transform.Find("ItemIcon").GetComponent<Image>();
        itemIcon.sprite = null;
        itemIcon.color = new Color(itemIcon.color.r, itemIcon.color.g, itemIcon.color.b, 0f);
        button.onClick.RemoveAllListeners();
    }

    private void SelectItem(Item item)
    {
        placementManager.SelectItem(item);
        HideItemSlots();
        ShowPlacementActions();
        UpdateCurrentItemText(placementManager.SelectedItem.itemName); 
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
