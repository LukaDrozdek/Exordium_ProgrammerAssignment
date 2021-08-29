using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DynamicInterface : UserInterface
{
    public GameObject inventoryPrefab;
    public int xGridStart;
    public int yGridStart;
    public int xSpaceBetweenItem;
    public int ySpaceBetweenItem;
    public int numberOfColumn;
    public override void CreateSlots()
    {
        slotsOnInterface = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Slots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.BeginDrag, delegate { OnDragStart(obj); });
            AddEvent(obj, EventTriggerType.EndDrag, delegate { OnDragEnd(obj); });
            AddEvent(obj, EventTriggerType.Drag, delegate { OnDrag(obj); });
            inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.Container.Slots[i]);
        }
    }

    private Vector3 GetPosition(int i)
    {
        //print(xGridStart + (xSpaceBetweenItem * (i % numberOfColumn)) + "X");
        //print(yGridStart + (-ySpaceBetweenItem * (i / numberOfColumn)));
        return new Vector3(xGridStart + (xSpaceBetweenItem * (i % numberOfColumn)), yGridStart + (-ySpaceBetweenItem * (i / numberOfColumn)), 0f);
    }
}
