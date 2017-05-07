using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubBehaviour : MonoBehaviour {

    Vector3 trigger_angle;
	// Use this for initialization
	void Start ()
    {
        trigger_angle = Vector3.zero;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerMovementController character_move = collision.GetComponent<PlayerMovementController>();
            character_move.speedo = 0.05f;                        
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            trigger_angle = new Vector3(0, 0, UnityEngine.Random.Range(-1.5f, 1.5f));
            transform.Rotate(trigger_angle);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            PlayerMovementController character_move = collision.GetComponent<PlayerMovementController>();
            character_move.speedo = 0.1f;
            transform.rotation = Quaternion.identity;
        }
    }

    // Update is called once per frame
    void Update ()
    {

    }
}
