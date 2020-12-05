using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory System/Inventory")]

public class InventoryObj : ScriptableObject
{
    public List<InventorySlot> Container = new List<InventorySlot>();

    public void AddItem(ItemObj _item, int _amount)
    {
        bool hasItem = false;
        for (int i = 0; i < Container.Count; i++)
        {
            if (Container[i].items == _item)
            {
                Container[i].AddSlot(_amount);
                hasItem = true;
                break;
            }
        }

        if (!hasItem)
        {
            Container.Add(new InventorySlot(_item, _amount));
        }

    }
}

[System.Serializable]
public class InventorySlot
{
    public ItemObj items;
    public int itemAmount;

    public InventorySlot(ItemObj _item, int _amount)
    {
        items = _item;
        itemAmount = _amount;
    }

    public void AddSlot(int value)
    {
        itemAmount += value;
    }
}