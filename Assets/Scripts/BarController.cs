using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BarController {

    private GameObject bar;
    private Image content;

    public BarController(GameObject bar)
    {
        this.bar = bar;
        this.content = bar.transform.GetChild(0).GetComponentInChildren<Image>();
    }
    
    public void handleBar(float value, float maxValue)
    {
        if (value > maxValue) throw new Exception("Invalid stat value: " + bar.name);
        content.fillAmount = value / maxValue;
    }

}
