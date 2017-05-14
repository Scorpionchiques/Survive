using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

//Contains information about inventory slot
public class Slot
{
    public GameObject slotObject; //Link to in-game slot object
    public GameObject itemObject; //Link to in-game item object
    public bool isEmpty;
    public Item item; //Item that slot contains
    public int count; //Count of contained item
    public Text countText; //Link to text object to display count

    public Slot(GameObject slotObject, GameObject itemObject = null)
    {
        this.slotObject = slotObject;
        //Adding slotController to every slot to control slot interactions
        this.slotObject.AddComponent<InventorySlotConroller>();
        this.slotObject.GetComponent<InventorySlotConroller>().slot = this;
        this.isEmpty = true;
        this.item = null;
        this.countText = null;
        this.count = 0;
    }

    //Returns the amount of items that didn't fit in that slot
    public int setItem(Item itemToSet, GameObject itemObject, int count = 1)
    {
        if (count <= 0) throw new Exception("Invalid count value");
        if (!this.isEmpty) throw new Exception("Trying to set item in filled slot");

        this.isEmpty = false;
        this.item = itemToSet;
        this.itemObject = itemObject;
        this.itemObject.transform.SetParent(this.slotObject.transform, false);
        this.itemObject.GetComponent<Image>().sprite = this.item.sprite;
        this.countText = this.itemObject.transform.GetChild(0).GetComponent<Text>();

        //Evaluating amount of items to return
        this.count = (count > this.item.maxStack) ? this.item.maxStack : count;
        updateCountText();
        return count - this.count;
    }

    //Returns the amount of items that didn't fit in that slot
    public int addToSlot(int count)
    {
        if (!this.item.stackable) throw new Exception("Trying to add to slot with unstackable item");
        if (count <= 0) throw new Exception("Invalid count value");

        if (count + this.count > this.item.maxStack)
        {
            this.count = this.item.maxStack;
            updateCountText();
            return count + this.count - this.item.maxStack;
        }
        else
        {
            this.count = count + this.count;
            updateCountText();
            return 0;
        }
    }

    //Update count text according to current count of contained item
    private void updateCountText()
    {
        if (this.item.stackable)
            this.countText.text = this.count.ToString();
        else
            this.countText.text = "";
    }

    //Remove item from the slot with all related objects and variables
    public void RemoveItem()
    {
        if (this.isEmpty) throw new Exception("Trying to remove item from empty slot");

        this.isEmpty = true;
        this.count = 0;
        this.item = null;
        UnityEngine.Object.Destroy(this.countText);
        UnityEngine.Object.Destroy(this.itemObject);
    }
}
