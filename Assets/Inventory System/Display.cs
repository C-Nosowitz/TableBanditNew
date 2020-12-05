using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Display : MonoBehaviour
{
    public GameObject itemObj;
    public InventoryObj inventory;
    public int xStart;
    public int yStart;
    public int Xspace; //repsresents space between items
    public int numColumn;
    public int Yspace;
    Dictionary<InventorySlot, GameObject> itemDisplay = new Dictionary<InventorySlot, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        CreateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDisplay();
    }

    public void UpdateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {
            if (itemDisplay.ContainsKey(inventory.Container[i]))
            {
                itemDisplay[inventory.Container[i]].GetComponent<RectTransform>().localPosition = GetPosition(i);
            }
            else
            {
                var obj = Instantiate(itemObj, Vector3.zero, Quaternion.identity, transform);

                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

                itemDisplay.Add(inventory.Container[i], obj);
            }
        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Count; i++)
        {

            var obj = Instantiate(itemObj, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            obj.transform.GetChild(0);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(xStart + (Xspace * (i % numColumn)), yStart + (-Yspace * (i / numColumn)), 0f);

    }
}
