using System;
using UnityEngine;
using UnityEngine.UIElements;

[Serializable]
public class InventoryItem
{
    public ItemData itemData;
    public int stackSize;

    public InventoryItem(ItemData item)
    {
        itemData = item;
        AddToInvStack();
    }

    public void AddToInvStack()
    {
        stackSize++;
    }

    public void RemoveFromStack()
    {
        stackSize--;
    }
}
