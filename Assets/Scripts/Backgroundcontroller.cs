﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundcontroller : MonoBehaviour {

	//List<Vector2> Background_map;
	void OnTriggerEnter2D(Collider2D COLD)
	{	
		
			GameObject inst = null;
					if (COLD.name == "Up") 
		{	Vector2 Background_position = new Vector2 (COLD.transform.parent.gameObject.transform.position.x, COLD.transform.parent.gameObject.transform.position.y);
			
						COLD.gameObject.GetComponent<Collider2D> ().enabled = false; 
						Background_position.y += 20f; 

						inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
						inst.transform.Find ("Down").gameObject.GetComponent<Collider2D> ().enabled = false; 
					}
					if (COLD.name == "Down") 
		{Vector2 Background_position = new Vector2 (COLD.transform.parent.gameObject.transform.position.x, COLD.transform.parent.gameObject.transform.position.y);
			
						COLD.gameObject.GetComponent<Collider2D> ().enabled = false; 
						Background_position.y += -20f;
						inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
						inst.transform.Find ("Up").gameObject.GetComponent<Collider2D> ().enabled = false; 
					}					
					if (COLD.name == "Left") 
		{Vector2 Background_position = new Vector2 (COLD.transform.parent.gameObject.transform.position.x, COLD.transform.parent.gameObject.transform.position.y);
			
						COLD.gameObject.GetComponent<Collider2D> ().enabled = false; 
						Background_position.x += -20f; 
						inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
						inst.transform.Find ("Right").gameObject.GetComponent<Collider2D> ().enabled = false; 
					}			
					if (COLD.name == "Right") 
		{	Vector2 Background_position = new Vector2 (COLD.transform.parent.gameObject.transform.position.x, COLD.transform.parent.gameObject.transform.position.y);
			
						COLD.gameObject.GetComponent<Collider2D>().enabled = false; 
						Background_position.x += 20f; 
						inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
						inst.transform.Find ("Left").gameObject.GetComponent<Collider2D> ().enabled = false; 
					}

	}


	// Use this for initialization

	 void Start () {
		//Background_map [x_pos_map, y_pos_map] = 1;
	}
	
	// Update is called once per frame
	void Update () {

		
	}
}