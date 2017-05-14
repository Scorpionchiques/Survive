using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

using System;

public class AnimatorController {

    public void move(Animator animator_player)
    {
        Vector2 input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")).normalized;
        Vector3 movement = MoveDecider.direction(input.x, input.y);

        AnimatorMoveDecider.SetAnimationDirection(animator_player, input.x, input.y);
        if (movement.Equals(Vector3.zero))
        {
            animator_player.StartPlayback();
        }
        else
        {
            animator_player.StopPlayback();
        }
    }
}
