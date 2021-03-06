﻿using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEditor;
using UnityStandardAssets.CrossPlatformInput;

public class Backgroundcontroller : MonoBehaviour {

	//List<Vector2> Background_map;
	void OnTriggerEnter2D(Collider2D COLD)
	{	
		
        if (COLD.name == "Up" || COLD.name == "Down" || COLD.name == "Left" || COLD.name == "Right")
        {
            GameObject instBG = null;
            Vector2 Background_position = new Vector2(COLD.transform.parent.gameObject.transform.position.x, COLD.transform.parent.gameObject.transform.position.y);
            COLD.gameObject.GetComponent<Collider2D>().enabled = false;
            instantiateBackground(COLD.name, Background_position, instBG, COLD.transform.parent.GetComponent<ChunkGenerator>().HeightBound);
        } 
	}

    void instantiateBackground(string COLDname, Vector2 Background_position, GameObject instBG, float[] hb)
    {
        string direction = null;
        float[] newhb = new float[4];
        switch (COLDname)
        {
            case "Up":
                direction = "Down";
                Background_position.y += 20f;
                setNewHeight(hb, newhb, 3, 2, 0, 1);  
                break;

            case "Left":
                direction = "Right";
                Background_position.x += -20f;
                setNewHeight(hb, newhb, 0, 3, 1, 2);
                break;

            case "Down":
                direction = "Up";
                Background_position.y += -20f;
                setNewHeight(hb, newhb, 0, 1, 3, 2);
                break;

            case "Right":
                direction = "Left";
                Background_position.x += 20f;
                setNewHeight(hb, newhb, 1, 2, 0, 3);
                break;
        }
        UnityEngine.Object prefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Background.prefab", typeof(GameObject));
        instBG = (GameObject)Instantiate(prefab, Background_position, Quaternion.identity);
        instBG.transform.SetParent(this.transform.parent.transform.parent);
        instBG.transform.Find(direction).gameObject.GetComponent<Collider2D>().enabled = false;
        instBG.GetComponent<ChunkGenerator>().HeightBound = newhb;
    }

    void setNewHeight(float[] hb, float[] newhb, int o1, int o2, int n1, int n2)
    {
        newhb[o1] = hb[n1];
        newhb[o2] = hb[n2];
        newhb[3-o1] = UnityEngine.Random.value;
        newhb[3-o2] = UnityEngine.Random.value;
    }

	// Use this for initialization

	 void Start () {
		//Background_map [x_pos_map, y_pos_map] = 1;
	}

    public float speedo;
    Rigidbody2D rb;

    void FixedUpdate()
    {
        Vector2 input = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical")).normalized;
        Vector3 movement = MoveDecider.direction(input.x, input.y);
        transform.position += movement * speedo;        
    }
    // Update is called once per frame
    void Update () {

		
	}
}