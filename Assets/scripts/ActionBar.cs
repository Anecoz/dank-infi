using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour {
    public int NumberOfSlots;
    public Sprite SelectedSlot;
    public Sprite NormalSlot;
    public Sprite Wood;

    private Transform[] slotArray;
    private Dictionary<int,InventoryItem.Type> SlotTypeMap;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        slotArray = this.GetComponentsInChildren<Transform>();

        Array.Sort(slotArray, delegate(Transform t1, Transform t2){
            return t1.position.x.CompareTo(t2.position.x);
        });
    }

    public bool SetType(InventoryItem.Type type) {
        bool status = false;
        switch(type) {
            case InventoryItem.Type.Wood:
                status = SetTypeToSlot(Wood, type);
                break;
            default:
                status = false;
                break;
        }

        return status;
    }

    public void ClearSlot(int slot) {
        // TODO: REMOVE INSTANTIATED SPRITE IN SLOT
        SlotTypeMap.Remove(slot);
    }

    public InventoryItem.Type GetType(int slot) {
        InventoryItem.Type type;
        SlotTypeMap.TryGetValue(slot, out type);

        return type;
    }

    public int GetNumberOfSlots() {
        return NumberOfSlots;
    }

    private bool SetTypeToSlot(Sprite sprite, InventoryItem.Type type) {
        int slot = 99;

        for(int i=0;i<SlotTypeMap.Count;i++) {
            if(!SlotTypeMap.ContainsKey(i)) {
                slot = i;
            }
        }
        
        if(slot < slotArray.Length) {
            Instantiate(Wood, slotArray[slot]);
            SlotTypeMap.Add(slot, type);
            return true;
        }

        return false;
    }
}