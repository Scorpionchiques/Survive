using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotConroller: MonoBehaviour {

    public static InfoPanel infoPanel;
    public Slot slot;

    private void Start()
    {
        Debug.Log("Slot controller start");
    }

    private void OnMouseDown()
    {
        //Check if slot is not empty
        if (slot.item != null)
        {
            infoPanel.panel.SetActive(true); //Show info panel
            infoPanel.setValues(slot.item); //Set info panel to represent selected item
        }
    }

}