using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Food
}
public abstract class ItemObj : ScriptableObject
{
    public GameObject gameObject;
    public ItemType type;
    [TextArea(50,50)]
    public string description;
}
