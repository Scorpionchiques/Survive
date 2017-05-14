using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour {

    ItemDatabase database;
    public GameObject slotPanel;
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount;
    public List<Item> items = new List<Item>();
    public List<Slot> slots = new List<Slot>();
    
    void Start()
    {
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

        AddItem(0, 5);

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

        Debug.Log("Item: " + itemToAdd.Title + " | Count: " + count);

        //If item is stackable things are complicated
        if (itemToAdd.Stackable == true)
        {
            //Look for slots with the same stackable item
            for (int i = 0; i < slotAmount; i++)
            {
                if (!slots[i].isEmpty)
                {
                    if (slots[i].Item.ID == id && slots[i].Count < slots[i].Item.MaxStack)
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

//Contains information about inventory slot
public class Slot
{
    public GameObject SlotObject { get; set; }
    public GameObject ItemObject { get; set; }
    public bool isEmpty { get; set; }
    public Item Item { get; set; }
    public int Count { get; set; }
    public Text CountText { get; set; }

    public Slot(GameObject slotObject, GameObject itemObject = null)
    {
        this.SlotObject = slotObject;
        this.isEmpty = true;
        this.Item = null;
        this.CountText = null;
        this.Count = 0;
    }

    //Returns the amount of items that didn't fit in that slot
    public int setItem(Item itemToSet, GameObject itemObject, int count = 1)
    {
        if (count <= 0) throw new Exception("Invalid count value");
        if (!this.isEmpty) throw new Exception("Trying to set item in filled slot");
        
        this.isEmpty = false;
        this.Item = itemToSet;
        this.ItemObject = itemObject;
        this.ItemObject.transform.SetParent(this.SlotObject.transform, false);
        this.ItemObject.GetComponent<Image>().sprite = this.Item.Sprite;
        this.CountText = this.ItemObject.transform.GetChild(0).GetComponent<Text>();

        //Evaluating amount of items to return
        this.Count = (count > this.Item.MaxStack) ? this.Item.MaxStack : count;
        updateCountText();
        return count - this.Count;
    }

    //Returns the amount of items that didn't fit in that slot
    public int addToSlot(int count)
    {
        if (!this.Item.Stackable) throw new Exception("Trying to add to slot with unstackable item");
        if (count <= 0) throw new Exception("Invalid count value");
        
        if (count + this.Count > this.Item.MaxStack)
        {
            this.Count = this.Item.MaxStack;
            updateCountText();
            return count + this.Count - this.Item.MaxStack;
        }
        else
        {
            this.Count = count + this.Count;
            updateCountText();
            return 0;
        }
    }

    private void updateCountText()
    {
        if (this.Item.Stackable)
            this.CountText.text = this.Count.ToString();
        else
            this.CountText.text = "";
    }

    public void RemoveItem()
    {
        if (this.isEmpty) throw new Exception("Trying to remove item from empty slot");

        this.isEmpty = true;
        this.Count = 0;
        this.Item = null;
        UnityEngine.Object.Destroy(this.CountText);
        UnityEngine.Object.Destroy(this.ItemObject);
    }
  
}
