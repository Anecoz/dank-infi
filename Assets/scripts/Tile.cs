using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	protected GameObject player;
	protected float maxDistance;
	void Start () {
		
	}

	void Update () {
		if (Vector3.Distance(transform.position, player.transform.position) > maxDistance) {
			Destroy (this.gameObject);
		}
	}

	public void SetParameters(GameObject play, float maxDist) {
		player = play;
		maxDistance = maxDist;
	}
}
