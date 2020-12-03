using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
[ CreateAssetMenu(fileName = "ItemList", menuName = "Inventory System/Items/Default" )]

public class DefaultItem : ItemObj
{
    public void Awake()
    {
        type = ItemType.Food;
    }
}
