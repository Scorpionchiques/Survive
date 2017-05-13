using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour {


    // Use this for initialization
    void Start()
    {
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Player character_move = collision.GetComponent<Player>();
            character_move.speedo = 0.025f;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Player character_move = collision.GetComponent<Player>();
            character_move.speedo = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
