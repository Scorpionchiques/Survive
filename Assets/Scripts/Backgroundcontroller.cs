﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundcontroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnBecameVisible() {
		Destroy (gameObject);
		Vector2 Background_position = new Vector2 (transform.position.x, transform.position.y);
		//GameObject background = Resources.Load("Background", typeof(GameObject));//GameObject.Find ("Background");
		GameObject inst;
		switch (gameObject.name) {
		case "Up":
			Background_position.y += 10f; 
			 inst = Instantiate(Resources.Load<GameObject>("Background_up"), Background_position, Quaternion.identity) as GameObject;
			break;

		case "Down":Background_position.y += -10f; 
			 inst = Instantiate(Resources.Load<GameObject>("Background_down"), Background_position, Quaternion.identity) as GameObject;
			break;
			
		case "Left":Background_position.x += -10f; 
			 inst = Instantiate(Resources.Load<GameObject>("Background_left"), Background_position, Quaternion.identity) as GameObject;
			break;
			
		case "Right":Background_position.x += 10f; 
			 inst = Instantiate(Resources.Load<GameObject>("Background_right"), Background_position, Quaternion.identity) as GameObject;
			break;
			
		}
		//GameObject inst = Instantiate(Resources.Load<GameObject>("Background"), Background_position, Quaternion.identity) as GameObject;
	
	}	
}
