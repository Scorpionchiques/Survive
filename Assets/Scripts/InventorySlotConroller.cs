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
        infoPanel.panel.SetActive(true);
        infoPanel.setValues(slot.item);
    }

}