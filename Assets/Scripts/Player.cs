using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speedo;
    Rigidbody2D rigidBody_player;
    private Animator animator_player;
    PlayerMovementController movement;
    InventoryController inventory;

    //public Event UseItem(Item);

    Item itemInHands;

    void Start()
    {
        rigidBody_player = GetComponent<Rigidbody2D>();
        animator_player = GetComponent<Animator>();
        movement = new PlayerMovementController();
        inventory = new InventoryController();
    }

    void FixedUpdate()
    {
        movement.move(rigidBody_player, animator_player, speedo);
    }
}
