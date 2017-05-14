using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MoveDecider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static Vector3 direction(float input_x, float input_y)
    {
        int signx = Math.Sign(input_x), signy = Math.Sign(input_y);

        Vector3 movement = Vector3.zero;
        if (Math.Abs(input_x) >= 0.382683 && Math.Abs(input_x) < 0.923879) // 22,5 - 67,5 degrees
        {
            movement = new Vector3(signx * (float)0.7, signy * (float)0.7, 0); //North-East, South-East, South-West, North-West         
        }
        else if (Math.Abs(input_x) >= 0.923879) // 0 - 22,5 degrees
        {
            movement = new Vector3(signx, 0, 0); //East, West
        }
        else // 67,5 - 90 degrees
        {
            movement = new Vector3(0, signy, 0); //North, South
        }
        return movement;
    }

}
