using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public string[] characterNames;

    public Sprite[] sprites;

    [TextArea(3, 10)]
    public string[] lines;

}
