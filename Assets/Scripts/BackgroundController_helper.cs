using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController_helper : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D COLD)
	{
		if (COLD.name == "Up" || COLD.name == "Down" || COLD.name == "Left" || COLD.name == "Right") {	
			COLD.gameObject.GetComponent<Collider2D> ().enabled = false; 

			gameObject.GetComponent<Collider2D> ().enabled = false; 

		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
