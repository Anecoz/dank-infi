using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {
	private GameObject player;
	private InventoryItem.Type type;

	void Start () {
		
	}

	void Update () {
		
	}

	public void SetParameters(GameObject play, InventoryItem.Type typ) {
		player = play;
		type = typ;
	}

	void OnMouseDown() {
		Vector2 playerPos = new Vector2 (player.transform.position.x, player.transform.position.y);
		Vector2 pos = new Vector2 (transform.position.x, transform.position.y);
		if (Vector2.Distance(playerPos, pos) <= 1.5f) {
			player.GetComponent<Inventory> ().AddItem (new InventoryItem (type));
			Destroy (this.gameObject);
		}
	}
}
