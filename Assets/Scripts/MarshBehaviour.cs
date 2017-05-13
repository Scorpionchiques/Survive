using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarshBehaviour : MonoBehaviour {


   // Use this for initialization
    void Start()
    {
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerMovementController character_move = collision.GetComponent<PlayerMovementController>();

            character_move.speedo = 0.05f;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerMovementController character_move = collision.GetComponent<PlayerMovementController>();
            character_move.speedo = 0.1f;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
