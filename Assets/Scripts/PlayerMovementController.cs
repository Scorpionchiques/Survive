using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementController : MonoBehaviour {
    
	public float speedo;

	void FixedUpdate () {
		Vector3 input = new Vector3 (CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"), 0).normalized;
        Vector3 movement;
        
        if (Math.Abs(input.x) >= 0.382683 && Math.Abs(input.x) < 0.923879) // 22,5 - 67,5 degrees
        {
            movement = new Vector3(Math.Sign(input.x) * (float)0.7, Math.Sign(input.y) * (float)0.7, 0); //North-East, South-East, South-West, North-West         
        } else if (Math.Abs(input.x) >= 0.923879) // 0 - 22,5 degrees
        {
            movement = new Vector3(Math.Sign(input.x), 0, 0); //East, West
        } else // 67,5 - 90 degrees
        {
            movement = new Vector3(0, Math.Sign(input.y), 0); //North, South
        }
        transform.position += movement * speedo; 
	}
}
