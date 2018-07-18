using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

	private List<InventoryItem> items = new List<InventoryItem>();
	void Start () {
		
	}

	void Update() {
		
	}

	public void AddItem(InventoryItem item) {
		items.Add (item);
		Debug.Log ("Added item to inventory, type: " + item.GetType ());
	}
}
