using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InventoryLoader : MonoBehaviour
{
    public Inventory inventory;
    public ItemData[] itemDataArray;    

    void Start()
    {        
        LoadItemsFromScriptableObjects();        
    }

    void LoadItemsFromScriptableObjects()
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
        Debug.Log("Items successfully loaded and added from ScriptableObjects.");
    }

    [System.Serializable]
    private class ItemArray
    {
        public Item[] items;
    }
}
