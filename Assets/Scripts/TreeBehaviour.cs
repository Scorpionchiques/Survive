using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeBehaviour : MapObject
{
    Vector3 trigger_angle;
    // Use this for initialization
    void Start()
    {
        o_sprite = GetComponent<SpriteRenderer>();
        trigerred = false;
        o_type = MapObjects.tree;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            var color = o_sprite.color;
            color.a = 0.5f;
            o_sprite.color = color;
            trigerred = true;            
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            trigger_angle = new Vector3(0, 0, UnityEngine.Random.Range(-0.5f, 0.5f));
            transform.Rotate(trigger_angle);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            var color = o_sprite.color;
            color.a = 1f;
            o_sprite.color = color;
            trigerred = false;
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
        Debug.Log("response from tree");
        if (trigerred == true)
        {
            p.messenger.createMessage("+1 wood");
            p.give(2,1);

            var sprite = GetComponent<SpriteRenderer>();
            var colliders = GetComponents<Collider2D>();
            p.ClickOn -= Response;
            foreach (var component in colliders)
            {                
                Destroy(component);
            }
            Destroy(sprite);
            Destroy(this);
        }
    }
}
