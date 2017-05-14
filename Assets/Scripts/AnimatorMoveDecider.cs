using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AnimatorMoveDecider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void SetAnimationDirection(Animator animator, float input_x, float input_y)
    {
        int signx = Math.Sign(input_x), signy = Math.Sign(input_y);
        string par_name_direction = "Direction";

        if (Math.Abs(input_x) >= 0.382683 && Math.Abs(input_x) < 0.923879)
        {
            if (signx > 0 && signy > 0)
            {
                animator.SetInteger(par_name_direction, 2);// 5 North-West 
            }
            if (signx > 0 && signy < 0)
            {
                animator.SetInteger(par_name_direction, 0);//7 South-West
            }
            if (signx < 0 && signy > 0)
            {
                animator.SetInteger(par_name_direction, 2);//4 North-East
            }
            if (signx < 0 && signy < 0)
            {
                animator.SetInteger(par_name_direction, 0);//6 South-East 
            }
        }

        else if (Math.Abs(input_x) >= 0.923879) // 0 - 22,5 degrees
        {
            switch (signx)
            {
                case -1:
                    animator.SetInteger(par_name_direction, 3);
                    break;
                case 1:
                    animator.SetInteger(par_name_direction, 1);
                    break;
            }
        }
        else // 67,5 - 90 degrees
        {
            switch (signy)
            {
                case -1:
                    animator.SetInteger(par_name_direction, 0);
                    break;
                case 1:
                    animator.SetInteger(par_name_direction, 2);
                    break;
            }
        }
    }
}
