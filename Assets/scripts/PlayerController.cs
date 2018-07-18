﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : MonoBehaviour {

	public float _speed = .5f;

	void Awake()
	{
		Vector2 startPos = new Vector2 (Random.Range (500, 10000), Random.Range (500, 10000));
		transform.position = new Vector3 (startPos.x, startPos.y, 0.5f);
	}

	void Start()
	{
		
	}

	void FixedUpdate () {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * _speed;
        var y = Input.GetAxis("Vertical") * Time.deltaTime * _speed;

        RotateTowardsMouse();
        transform.Translate(Vector3.right * x, Space.World);
        transform.Translate(Vector3.up * y, Space.World);
	}

	private void RotateTowardsMouse() {
		Vector3 mousePos = Input.mousePosition;
		mousePos.z = 5.23f; //The distance between the camera and object
		Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objectPos.x;
		mousePos.y = mousePos.y - objectPos.y;
		float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
	}
}
