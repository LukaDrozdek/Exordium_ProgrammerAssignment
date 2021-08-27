using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;
    public InventoryObject inventory;
    public int xGridStart;
    public int yGridStart;
    public int xSpaceBetweenItem;
    public int ySpaceBetweenItem;
    public int numberOfColumn;
    Dictionary<InventorySlot, GameObject> itemsDisplayed = new Dictionary<InventorySlot, GameObject>();

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
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];
            if (itemsDisplayed.ContainsKey(slot))
            {
                itemsDisplayed[slot].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");

            }
            else
            {
                var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
                obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDysplay;
                obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
                obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
                itemsDisplayed.Add(slot, obj);
            }

        }
    }

    public void CreateDisplay()
    {
        for (int i = 0; i < inventory.Container.Items.Count; i++)
        {
            InventorySlot slot = inventory.Container.Items[i];
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDysplay;
            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
            itemsDisplayed.Add(slot, obj);
        }
    }

    public Vector3 GetPosition(int i)
    {
        //print(xGridStart + (xSpaceBetweenItem * (i % numberOfColumn)) + "X");
        //print(yGridStart + (-ySpaceBetweenItem * (i / numberOfColumn)));
        return new Vector3(xGridStart + (xSpaceBetweenItem * (i % numberOfColumn)), yGridStart + (-ySpaceBetweenItem * (i / numberOfColumn)), 0f);
    }
}
