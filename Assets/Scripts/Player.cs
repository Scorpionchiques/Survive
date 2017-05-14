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
    AnimatorController player_animation;
    InventoryController inventory;

    Item itemInHands;

    public delegate void ResponseFromObject(Item i, Player p);

    public event ResponseFromObject ClickOn;

    void Start()
    {
        rigidBody_player = GetComponent<Rigidbody2D>();
        animator_player = GetComponent<Animator>();
        movement = new PlayerMovementController();
        player_animation = new AnimatorController();
        inventory = new InventoryController();
        itemInHands = null;
    }
    
    void FixedUpdate()
    {
        movement.move(rigidBody_player, animator_player, speedo);
        player_animation.move(animator_player);
    }
   
    private void OnMouseDown()
    {
        ClickOn(itemInHands, this);
    }
    public void ClickedObject()
    {
        ClickOn(itemInHands, this);
    }
}