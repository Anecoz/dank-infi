using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour {
    public int NumberOfSlots;
    public Sprite SelectedSlotSprite;
    public Sprite NormalSlotSprite;
    public GameObject Wood;

    private int selectedSlot;
    private Transform[] slotArray;
    private Dictionary<int,InventoryItem.Type> slotTypeMap;
    private Dictionary<int,GameObject> slotObjectMap;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start() {
        selectedSlot = 1;
        slotArray = this.GetComponentsInChildren<Transform>();
        Array.Sort(slotArray, delegate(Transform t1, Transform t2){
            return t1.position.x.CompareTo(t2.position.x);
        });

        // Scale the sprite to fit the slots
        Wood.GetComponent<Transform>().localScale = new Vector3(0.5f, 0.5f, 0.5f);
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

    public InventoryItem.Type SelectSlot(int slot) {
        ChangeSlotSprite(selectedSlot, NormalSlotSprite);
        ChangeSlotSprite(slot, SelectedSlotSprite);
        return GetType(slot);
    }

    public void ClearSlot(int slot) {
        slotTypeMap.Remove(slot);
        slotObjectMap.Remove(slot);

        GameObject spriteObj = GetSpriteObj(slot);
        Destroy(spriteObj);
    }

    public int GetNumberOfSlots() {
        return NumberOfSlots;
    }

    private InventoryItem.Type GetType(int slot) {
        InventoryItem.Type type;
        slotTypeMap.TryGetValue(slot, out type);

        return type;
    }

    private GameObject GetSpriteObj(int slot) {
        GameObject spriteObj;
        slotObjectMap.TryGetValue(slot, out spriteObj);

        return spriteObj;
    }

    private void ChangeSlotSprite(int slot, Sprite sprite) {
        GameObject spriteObj = GetSpriteObj(slot);
        spriteObj.GetComponent<SpriteRenderer>().sprite = sprite;
    }

    private bool SetTypeToSlot(GameObject obj, InventoryItem.Type type) {
        int slot = NumberOfSlots + 1;

        for(int i=0;i<slotTypeMap.Count;i++) {
            if(!slotTypeMap.ContainsKey(i)) {
                slot = i;
            }
        }
        
        if(slot < slotArray.Length) {
            GameObject newObject = Instantiate(obj, slotArray[slot]);
            slotObjectMap.Add(slot, newObject);
            slotTypeMap.Add(slot, type);
            return true;
        }

        return false;
    }
}