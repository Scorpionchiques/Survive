using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float stamina;
    [SerializeField]
    private float maxStamina;

    public void changeHP(float suka)
    {
        health += suka;
    }
    public float speedo;
    public GameObject healthBar;
    public GameObject staminaBar;

    Rigidbody2D rigidBody_player;
    private Animator animator_player;

    PlayerMovementController movement;
    AnimatorController player_animation;
    public GameObject inventoryPanel;
    InventoryController inventory;

    BarController healthBarControl;
    BarController staminaBarControl; 

    Item itemInHands;

    public GameObject canvas;
    public Messenger messenger;

    public delegate void ResponseFromObject(Item i, Player p);

    public event ResponseFromObject ClickOn;

    void Start()
    {
        rigidBody_player = GetComponent<Rigidbody2D>();
        animator_player = GetComponent<Animator>();
        movement = new PlayerMovementController();
        player_animation = new AnimatorController();
        inventory = inventoryPanel.GetComponent<InventoryController>();
        itemInHands = null;
        healthBarControl = new BarController(healthBar);
        staminaBarControl = new BarController(staminaBar);

        messenger = new Messenger(canvas);
    }
    
    void FixedUpdate()
    {
        movement.move(rigidBody_player, animator_player, speedo);
        player_animation.move(animator_player);
        healthBarControl.handleBar(health, maxHealth);
        staminaBarControl.handleBar(stamina, maxStamina);
        stamina -= 0.005f;
    }

    public void ClickedObject()
    {
        ClickOn(itemInHands, this);
    }

    public void give(int id, int count)
    {
        inventory.AddItem(id, count);
    }

}