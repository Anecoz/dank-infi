using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	private List<InventoryItem> items = new List<InventoryItem>();

	public ActionBar actionbar;

	void Start () {
		actionbar.Initialize();
	}

	void Update() {
		
	}

	public void AddItem(InventoryItem item) {
		items.Add (item);
		if(actionbar.SetType(item.GetType())) {
			Debug.Log ("Added item to inventory, type: " + item.GetType ());
		}
		else {
			Debug.Log ("Inventory full!");
		}
	}

	public bool HasType(InventoryItem.Type type) {
		foreach (InventoryItem item in items) {
			if (item.GetType() == type) {
				return true;
			}
		}
		return false;
	}

	public void TakeItem(InventoryItem.Type type) {
		int idx = -1;
		for (int i = 0; i < items.Count; i++) {
			if (items[i].GetType() == type) {
				idx = i;
				break;
			}
		}
		if (idx == -1)
			return;
		items.RemoveAt (idx);
	}
}
