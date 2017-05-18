using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class Messenger {

    private GameObject canvas;

    public Messenger(GameObject canvas)
    {
        this.canvas = canvas;
    }

    public void itemGainMessage(Item item, int count)
    {
        createMessage("+" + count.ToString() + " " + item.title);
    }

    public void createMessage(string msg)
    {
        //Create message object and place it on canvas
        string path = "Assets/Prefabs/Message.prefab";
        UnityEngine.Object messagePrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));
        GameObject messageObj = (GameObject)GameObject.Instantiate(messagePrefab);
        messageObj.transform.SetParent(this.canvas.transform);

        //Place message in the middle of canvas and adjust scales
        messageObj.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        messageObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0.5f, 0.5f);
        
        //Show text and animation
        Text message = messageObj.GetComponent<Text>();
        message.text = msg;
        animateMessage(messageObj);
        Debug.Log("Message: " + msg);
    }

    private void animateMessage(GameObject messageObj)
    {
        float time = 1.0f;
        float alphaVal = 0.0f;
        LeanTween.moveLocalY(messageObj, 70.0f, time).destroyOnComplete = true;
        LeanTween.alphaText(messageObj.GetComponent<RectTransform>(), alphaVal, time);
    }


}
