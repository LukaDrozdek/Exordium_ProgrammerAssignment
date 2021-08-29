using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public InventoryObject inventory;
    public InventoryObject equipment;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inventory.SaveDatabase();
            equipment.SaveDatabase();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            inventory.LoadDatabase();
            equipment.LoadDatabase();
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if(inventory.AddItem(_item, 1))
            {
                Destroy(other.gameObject);
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
        equipment.Container.Clear();
    }
}
