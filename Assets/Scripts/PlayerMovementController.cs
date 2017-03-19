using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementController : MonoBehaviour {

	//Rigidbody2D playerBody;
	public float speedo;

	void Start () {
		//playerBody = this.GetComponent<Rigidbody2D>();
	}
	
	void FixedUpdate () {
		Vector3 movement = new Vector3 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0);
		transform.position += movement * speedo;
         
	}
}
