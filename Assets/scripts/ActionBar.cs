using System;
using System.Collections.Generic;
using UnityEngine;

public class ActionBar : MonoBehaviour {
    public int NumberOfSlots;
    public Sprite SelectedSlotSprite;
    public Sprite NormalSlotSprite;
    public GameObject Wood;

    private int selectedSlot;
    private List<Transform> slotList = new List<Transform>();
    private Dictionary<int,InventoryItem.Type> slotTypeMap = new Dictionary<int, InventoryItem.Type>();
    private Dictionary<int,GameObject> slotObjectMap = new Dictionary<int, GameObject>();

    public void Initialize() {
        var slotArray = this.GetComponentsInChildren<Transform>();
        for(int i=1;i<slotArray.Length;i++) {
            slotList.Add(slotArray[i]);
        }

        selectedSlot = 0;
        SelectSlot(selectedSlot);
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

        if (slot >= 0 && slot < NumberOfSlots) {
            GameObject spriteObj = slotList[slot].gameObject;

            if(spriteObj != null) {
                spriteObj.GetComponent<SpriteRenderer>().sprite = sprite;
            }
        }
    }

    private bool SetTypeToSlot(GameObject obj, InventoryItem.Type type) {
        int slot = 0;

        for(int i=0;i<slotTypeMap.Count;i++) {
            if(slotTypeMap.ContainsKey(i)) {
                slot++;
            }
        }
        
        if(slot < slotList.Count) {
            AddNewObject(obj, slot, type);
            return true;
        }

        return false;
    }

    private void AddNewObject(GameObject obj, int slot, InventoryItem.Type type) {
        GameObject newObject = Instantiate(obj, slotList[slot].transform);
        newObject.GetComponent<SpriteRenderer>().sortingOrder = 4;
        slotObjectMap.Add(slot, newObject);
        slotTypeMap.Add(slot, type);
    }
}