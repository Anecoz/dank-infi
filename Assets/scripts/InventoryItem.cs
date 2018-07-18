using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem {
	public enum Type {
		Wood
	};

	private Type type;

	public InventoryItem(Type typ) {
		type = typ;
	}

	public Type GetType() {
		return type;
	}
}
