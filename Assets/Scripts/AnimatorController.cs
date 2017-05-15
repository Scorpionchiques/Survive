using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

using System;

public class AnimatorController {

    public void move(Animator animatorPlayer)
    {
        Vector2 input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")).normalized;
        Vector3 movement = MoveDecider.direction(input.x, input.y);

        AnimatorMoveDecider.SetAnimationDirection(animatorPlayer, input.x, input.y);
        if (movement.Equals(Vector3.zero))
        {
            animatorPlayer.StartPlayback();
        }
        else
        {
            animatorPlayer.StopPlayback();
        }
    }
}
