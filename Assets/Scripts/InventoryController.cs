using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

    ItemDatabase database;
    public GameObject slotPanel; // panel that contains all inventory slots
    public GameObject inventorySlot; // slot prefab
    public GameObject inventoryItem; // item prefab
    public GameObject infoPanel; // Panel to display info about an item

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<Slot> slots = new List<Slot>();
    
    void Start()
    {
        //Static info panel for all slots to refer to
        InfoPanel panel = new InfoPanel(infoPanel);
        InventorySlotConroller.infoPanel = panel;

        //Get access to item database
        database = GetComponent<ItemDatabase>();

        //Set up slots
        slotAmount = 30;
        for (int i = 0; i < slotAmount; ++i)
        {
            //Set slot game object to attach to slot instance
            GameObject slotObject = Instantiate(inventorySlot);
            slotObject.transform.SetParent(slotPanel.transform, false);

            slots.Add(new Slot(slotObject));
        }

        gameObject.SetActive(false);
        infoPanel.SetActive(false);

        AddItem(0, 5);
        AddItem(1, 2);

        //Testing AddItem and RemoveItem functions
        /*
        System.Random rand = new System.Random();
        for (int i = 0; i < 21; i++)
        {
            AddItemTest(rand);
        }
        */
    }
    
    public void AddItemTest(System.Random rand)
    {
        int coin = rand.Next(0, 3);
        if (coin == 0) AddItem(0, rand.Next(1, 11));
        else if (coin == 1) AddItem(1, rand.Next(1, 3));
        else if (coin == 2) {
            int slot = rand.Next(0, slotAmount);
            if (!slots[slot].isEmpty)
            {
                slots[slot].RemoveItem();
                Debug.Log("Delete: " + slot);
            }
            
        }
    }
    

    public void AddItem(int id, int count = 1)
    {
        Item itemToAdd = database.getItemByID(id);

        Debug.Log("Item: " + itemToAdd.title + " | Count: " + count);

        //If item is stackable things are complicated
        if (itemToAdd.stackable == true)
        {
            //Look for slots with the same stackable item
            for (int i = 0; i < slotAmount; i++)
            {
                if (!slots[i].isEmpty)
                {
                    if (slots[i].item.id == id && slots[i].count < slots[i].item.maxStack)
                    {
                        count = slots[i].addToSlot(count);
                        if (count == 0) break;
                    }
                }
            }

            //If all slots with the same item are already full and there is still something to add
            if (count > 0)
            {
                int emptySlot = findEmptySlot();
                while (count > 0 && emptySlot != -1)
                {
                    GameObject itemObject = Instantiate(inventoryItem); //Set item game object to attach to slot instance
                    count = slots[emptySlot].setItem(itemToAdd, itemObject, count);
                    emptySlot = findEmptySlot();
                }

                //If there is no more empty slots and count > 0
                if (count > 0)
                {
                    return; //Implement item throwing
                }
            }
        }
        //Is item is unstackable just look for an empty slot
        else
        {
            int emptySlot = findEmptySlot();
            while (count > 0 && emptySlot != -1)
            {
                GameObject itemObject = Instantiate(inventoryItem); //Set item game object to attach to slot instance
                count = slots[emptySlot].setItem(itemToAdd, itemObject, count);
                emptySlot = findEmptySlot();
            }
        }
    }

    int findEmptySlot()
    {
        for (int i = 0; i < slotAmount; i++)
            if (slots[i].isEmpty) return i;
        return -1;
    }
}