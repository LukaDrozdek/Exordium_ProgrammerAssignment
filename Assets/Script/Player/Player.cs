using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{

    public InventoryObject inventory;
    public InventoryObject equipment;

    public Attribute[] attribute;
    public TextMeshProUGUI agility;
    public TextMeshProUGUI intellect;
    public TextMeshProUGUI stamina;
    public TextMeshProUGUI strenght;
    public List<GameObject> setGameObjectInPool = new List<GameObject>();
    public TextMeshProUGUI UiInventory;

    private void Start()
    {
        
        for (int i = 0; i < attribute.Length; i++)
        {
            attribute[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SaveInventory();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadInventory();
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            CreateItemFromPool();
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            Item _item = new Item(item.item);
            if (inventory.AddItem(_item, 1))
            {
                other.gameObject.SetActive(false);
                setGameObjectInPool.Add(other.gameObject);
            }
            else
            {
                Debug.Log("Inventori id full");
                UiInventory.text = "Inventori is full";
                Invoke("UiInventoryOff", 1);

            }
        }
    }

    public void SaveInventory()
    {
        inventory.SaveDatabase();
        equipment.SaveDatabase();
    }

    public void LoadInventory()
    {
        inventory.LoadDatabase();
        equipment.LoadDatabase();
    }

    public void UiInventoryOff()
    {
        UiInventory.text = "";
    }

    public void CreateItemFromPool()
    {
        int random = Random.Range(0, setGameObjectInPool.Count);
        if (setGameObjectInPool.Count > 0)
        {
            setGameObjectInPool[random].SetActive(true);
        }
        if(setGameObjectInPool.Count > 0)
        {
            setGameObjectInPool.RemoveAt(random);
        }
    }

    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
        {
            return;
        }

        switch (_slot.parent.inventory.type)
        {
            case InterFaceType.Inventory:
                break;
            case InterFaceType.Equipment:
               // print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attribute.Length; j++)
                    {
                        if (attribute[j].type == _slot.item.buffs[i].attributes)
                        {
                            attribute[j].value.RemoveModifier(_slot.item.buffs[i]);
                            
                        }
                    }
                }
                break;
            case InterFaceType.Chest:
                break;
            default:
                break;
        }

    }

    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterFaceType.Inventory:
                break;
            case InterFaceType.Equipment:
               // print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));
                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attribute.Length; j++)
                    {
                        if (attribute[j].type == _slot.item.buffs[i].attributes)
                        {
                            attribute[j].value.AddModifier(_slot.item.buffs[i]);
                        }
                    }
                }
                break;
            case InterFaceType.Chest:
                break;
            default:
                break;
        }
    }

    public void AttributeModifiedDisplay(Attribute attribute)
    {
        if (attribute.type.ToString() == "Agility")
        {
            agility.text = string.Concat("Agility: " ,attribute.value.ModifiedValue);
        }
        if (attribute.type.ToString() == "Intellect")
        {
            intellect.text = string.Concat("Intellect: ", attribute.value.ModifiedValue);
        }
        if (attribute.type.ToString() == "Stamina")
        {
            stamina.text = string.Concat("Stamina: ", attribute.value.ModifiedValue);
        }
        if (attribute.type.ToString() == "Strenght")
        {
            strenght.text = string.Concat("Strenght: ", attribute.value.ModifiedValue);
        }

    }


    private void OnApplicationQuit()
    {
        inventory.Clear();
        equipment.Clear();
    }
}
[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public Player parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(Player _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified()
    {
        parent.AttributeModifiedDisplay(this);
    }
}
