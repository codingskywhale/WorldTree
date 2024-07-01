using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private List<Item> items = new List<Item>();

    public void AddItem(Item item)
    {
        items.Add(item);        
    }

    public Item FindItemByName(string itemName)
    {
        return items.Find(item => item.itemName == itemName);
    }

    public List<Item> GetVisibleItems(int startIndex, int count)
    {
        List<Item> visibleItems = new List<Item>();
        for (int i = startIndex; i < startIndex + count && i < items.Count; i++)
        {
            visibleItems.Add(items[i]);
        }
        return visibleItems;
    }

    public int GetItemCount()
    {
        return items.Count;
    }
}

