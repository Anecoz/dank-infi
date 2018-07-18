using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
	public GameObject player;
	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - player.transform.position;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, 0f);
		//float inv = 1.0f / 64.0f;
		//var oldPos = transform.position;
		//transform.position = new Vector3 (quantized (inv, oldPos.x), quantized (inv, oldPos.y), oldPos.z);
	}

	private float quantized(float quant, float inVal) {
		return (int)(inVal/quant)*quant;
	}
}
