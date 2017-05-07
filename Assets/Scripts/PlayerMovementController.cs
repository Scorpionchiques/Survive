using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerMovementController : MonoBehaviour {
    
	public float speedo;
    Rigidbody2D rigidBody_player;
    private Animator animator_player;
    void Start()
    {
        rigidBody_player = GetComponent<Rigidbody2D>();
        animator_player = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        Vector2 input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")).normalized;

        Vector3 movement = MoveDecider.direction(input.x, input.y);
        MoveDecider.SetAnimationDirection(animator_player, input.x, input.y);

        rigidBody_player.velocity = speedo * (movement.normalized);
        rigidBody_player.MovePosition(rigidBody_player.position + rigidBody_player.velocity);

    }
}
