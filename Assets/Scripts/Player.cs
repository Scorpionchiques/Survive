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

    public float speedo;
    public GameObject healthBar;
    public GameObject staminaBar;

    Rigidbody2D rigidBodyPlayer;
    private Animator animatorPlayer;

    PlayerMovementController movement;
    AnimatorController playerAnimator;
    InventoryController inventory;

    BarController healthBarControl;
    BarController staminaBarControl; 

    Item itemInHands;

    public delegate void ResponseFromObject(Item i, Player p);

    public event ResponseFromObject ClickOn;

    void Start()
    {
        rigidBodyPlayer = GetComponent<Rigidbody2D>();
        animatorPlayer = GetComponent<Animator>();
        movement = new PlayerMovementController();
        playerAnimator = new AnimatorController();
        inventory = new InventoryController();
        itemInHands = null;
        healthBarControl = new BarController(healthBar);
        staminaBarControl = new BarController(staminaBar);
    }
    
    void FixedUpdate()
    {
        movement.move(rigidBodyPlayer, animatorPlayer, speedo);
        playerAnimator.move(animatorPlayer);
        healthBarControl.handleBar(health, maxHealth);
        staminaBarControl.handleBar(stamina, maxStamina);
        stamina -= 0.005f;
    }

    public void ClickedObject()
    {
        ClickOn(itemInHands, this);
    }
}