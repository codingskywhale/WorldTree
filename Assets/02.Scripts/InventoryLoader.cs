using System.Collections.Generic;
using UnityEngine;

public class InventoryLoader : MonoBehaviour
{
    public Inventory inventory;
    public ItemData[] itemDataArray;    

    private void Start()
    {        
        LoadItemsFromScriptableObjects();        
    }

    private void LoadItemsFromScriptableObjects()
    {
        ItemData[] itemDataArray = Resources.LoadAll<ItemData>("ScriptableObjects/Items");
        foreach (ItemData itemData in itemDataArray)
        {
            Item item = new Item
            {
                itemName = itemData.itemName,
                prefab = itemData.prefab,
                icon = itemData.icon
            };
            inventory.AddItem(item);
        }        
    }

    [System.Serializable]
    private class ItemArray
    {
        public Item[] items;
    }
}
