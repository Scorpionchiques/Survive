using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backgroundcontroller : MonoBehaviour {

	//List<Vector2> Background_map;
	void OnTriggerEnter2D(Collider2D COLD)
	{	
		
		Vector2 Background_position = new Vector2 (COLD.transform.parent.gameObject.transform.position.x, COLD.transform.parent.gameObject.transform.position.y);
		GameObject inst = null;
					if (COLD.name == "Up") 
					{	
						COLD.gameObject.GetComponent<Collider2D> ().enabled = false; 
						Background_position.y += 20f; 

						inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
						inst.transform.Find ("Down").gameObject.GetComponent<Collider2D> ().enabled = false; 
					}
					if (COLD.name == "Down") 
					{
						COLD.gameObject.GetComponent<Collider2D> ().enabled = false; 
						Background_position.y += -20f;
						inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
						inst.transform.Find ("Up").gameObject.GetComponent<Collider2D> ().enabled = false; 
					}					
					if (COLD.name == "Left") 
					{
						COLD.gameObject.GetComponent<Collider2D> ().enabled = false; 
						Background_position.x += -20f; 
						inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
						inst.transform.Find ("Right").gameObject.GetComponent<Collider2D> ().enabled = false; 
					}			
					if (COLD.name == "Right") 
					{	
						COLD.gameObject.GetComponent<Collider2D>().enabled = false; 
						Background_position.x += 20f; 
						inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
						inst.transform.Find ("Left").gameObject.GetComponent<Collider2D> ().enabled = false; 
					}
                    ChunkGenerator ncg = inst.GetComponent<ChunkGenerator>();
                    ChunkGenerator ocg = transform.GetComponent<ChunkGenerator>();
                    //ncg.setHeightBound(ocg.getHeightBound());
                    //inst.AddComponent<ChunkGenerator>(ncg);

//		if (COLD.name == "Up-Right") 
//		{	
//			COLD.gameObject.GetComponent<Collider2D>().enabled = false; 
//			Background_position.x += 20f; 
//			Background_position.y += 20f;
//			inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
//			inst.transform.Find ("Down-Left").gameObject.GetComponent<Collider2D> ().enabled = false;
//			inst.transform.Find ("Left").gameObject.GetComponent<Collider2D> ().enabled = false; 
//			inst.transform.Find ("Down").gameObject.GetComponent<Collider2D> ().enabled = false; 
//		}	
//		if (COLD.name == "Up-Left") 
//		{	
//			COLD.gameObject.GetComponent<Collider2D>().enabled = false; 
//			Background_position.x += -20f; 
//			Background_position.y += 20f; 
//			inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
//			inst.transform.Find ("Down-Right").gameObject.GetComponent<Collider2D> ().enabled = false;
//			inst.transform.Find ("Down").gameObject.GetComponent<Collider2D> ().enabled = false; 
//			inst.transform.Find ("Right").gameObject.GetComponent<Collider2D> ().enabled = false; 
//		}	
//		if (COLD.name == "Down-Right") 
//		{	
//			COLD.gameObject.GetComponent<Collider2D>().enabled = false; 
//			Background_position.x += 20f; 
//			Background_position.y += -20f; 
//			inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
//			inst.transform.Find ("Up-Left").gameObject.GetComponent<Collider2D> ().enabled = false;
//			inst.transform.Find ("Left").gameObject.GetComponent<Collider2D> ().enabled = false; 
//			inst.transform.Find ("Up").gameObject.GetComponent<Collider2D> ().enabled = false; 
//		}	
//		if (COLD.name == "Down-Left") 
//		{	
//			COLD.gameObject.GetComponent<Collider2D>().enabled = false; 
//			Background_position.x += -20f; 
//			Background_position.y += -20f; 
//			inst = Instantiate (Resources.Load<GameObject> ("Background"), Background_position, Quaternion.identity) as GameObject;
//			inst.transform.Find ("Up-Right").gameObject.GetComponent<Collider2D> ().enabled = false;
//			inst.transform.Find ("Up").gameObject.GetComponent<Collider2D> ().enabled = false; 
//			inst.transform.Find ("Right").gameObject.GetComponent<Collider2D> ().enabled = false; 
//		}	
	}


	// Use this for initialization

	 void Start () {
		//Background_map [x_pos_map, y_pos_map] = 1;
	}
	
	// Update is called once per frame
	void Update () {

		
	}

//	void OnBecameVisible() {
//		Destroy (gameObject);
//		Vector2 Background_position = new Vector2 (transform.position.x, transform.position.y);
//		//GameObject background = Resources.Load("Background", typeof(GameObject));//GameObject.Find ("Background");
//		GameObject inst;
//		switch (gameObject.name) {
//		case "Up":
//			Background_position.y += 20f; 
//			 inst = Instantiate(Resources.Load<GameObject>("Background_up"), Background_position, Quaternion.identity) as GameObject;
//			break;
//
//		case "Down":Background_position.y += -20f; 
//			 inst = Instantiate(Resources.Load<GameObject>("Background_down"), Background_position, Quaternion.identity) as GameObject;
//			break;
//			
//		case "Left":Background_position.x += -20f; 
//			 inst = Instantiate(Resources.Load<GameObject>("Background_left"), Background_position, Quaternion.identity) as GameObject;
//			break;
//			
//		case "Right":Background_position.x += 20f; 
//			 inst = Instantiate(Resources.Load<GameObject>("Background_right"), Background_position, Quaternion.identity) as GameObject;
//			break;
//			
//		}
		//GameObject inst = Instantiate(Resources.Load<GameObject>("Background"), Background_position, Quaternion.identity) as GameObject;
	
	//}	
}
