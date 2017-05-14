using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrubBehaviour : MapObject
{

    Vector3 trigger_angle;
    // Use this for initialization
    void Start ()
    {
        o_sprite = GetComponent<SpriteRenderer>();
        trigger_angle = Vector3.zero;
        trigerred = false;
        o_type = MapObjects.shrub;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            var color = o_sprite.color;
            color.a = 0.5f;
            trigerred = true;
            o_sprite.color = color;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Player character_move = collision.GetComponent<Player>();
            trigger_angle = new Vector3(0, 0, UnityEngine.Random.Range(-1.5f, 1.5f));
            character_move.speedo = 0.05f;
            transform.Rotate(trigger_angle);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            var color = o_sprite.color;
            color.a = 1f;
            o_sprite.color = color;
            trigerred = false;
            Player character_move = collision.GetComponent<Player>();
            character_move.speedo = 0.1f;
            transform.rotation = Quaternion.identity;
        }
    }

    private void OnMouseDown()
    {
        if (trigerred == true)
        {
            var p = transform.parent.parent.GetComponentInChildren<Player>();
            p.ClickOn += Response;
            p.ClickedObject();
        }
    }

    private void Response(Item clickedItem, Player p)
    {
        p.ClickOn -= Response;
        Debug.Log("response from shrub");
    }
    // Update is called once per frame
    void Update ()
    {

    }
}
