using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public InventoryObject inventory;

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            inventory.SaveDatabase();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            inventory.LoadDatabase();
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<Item>();
        if(item)
        {
            inventory.AddItem(item.item, 1);
            Destroy(other.gameObject);
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
