using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
//		gameObject.transform.Find ("Up").gameObject.GetComponent<Collider2D> ().enabled = false; 
//		gameObject.transform.Find ("Down").gameObject.GetComponent<Collider2D> ().enabled = false; 
//		gameObject.transform.Find ("Right").gameObject.GetComponent<Collider2D> ().enabled = false; 
//		gameObject.transform.Find ("Left").gameObject.GetComponent<Collider2D> ().enabled = false; 
        var x = Input.GetAxis("Horizontal");
		var y = Input.GetAxis("Vertical");
//		if (x > 0) {
//			gameObject.transform.Find ("Right").gameObject.GetComponent<Collider2D> ().enabled = true;
//		} else if (x<0){
//			gameObject.transform.Find ("Left").gameObject.GetComponent<Collider2D> ().enabled = true;	
//		}
//		if (y > 0) {
//			gameObject.transform.Find ("Up").gameObject.GetComponent<Collider2D> ().enabled = true;
//		} else if (y<0){
//			gameObject.transform.Find ("Down").gameObject.GetComponent<Collider2D> ().enabled = true;	
//		}
        transform.position = new Vector3(transform.position.x + x*0.1f, transform.position.y + y*0.1f, 0);
    }
}
