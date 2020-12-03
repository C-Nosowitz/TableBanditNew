using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Food ", menuName = "Inventory System/Items/Food")]


public class FoodObj : ItemObj 
{
    public int addScore;

    public void Awake()
    {
        type = ItemType.Food;
    }
}
