using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementController
{

    //public float speedo;
    //Rigidbody2D rigidBody_player;
    //private Animator animator_player;

    //public PlayerMovementController(float sp)
    //{
    //    speedo = sp;
    //}

    public void move(Rigidbody2D rigidBody_player, Animator animator_player, float speedo)
    {
        Vector2 input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")).normalized;

        Vector3 movement = MoveDecider.direction(input.x, input.y);
        MoveDecider.SetAnimationDirection(animator_player, input.x, input.y);
        if (movement.Equals(Vector3.zero))
        {
            animator_player.StartPlayback();
        }
        else
        {
            animator_player.StopPlayback();
        }

        rigidBody_player.velocity = speedo * (movement.normalized);
        rigidBody_player.MovePosition(rigidBody_player.position + rigidBody_player.velocity);
    }
}
