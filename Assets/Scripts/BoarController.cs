using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarController : MonoBehaviour {

    Transform playerPosition;
    Vector3 destination;
    Rigidbody2D kaban_body;
    float kaban_velocity;
    float kaban_acceleration;
    bool run;
    private CustomSecondTrigger pdtf;
    // Use this for initialization
    void Start () {
        destination = Vector3.zero;
        playerPosition = null;
        kaban_velocity = 0.05f;
        kaban_acceleration = 0.025f;
        kaban_body = this.GetComponent<Rigidbody2D>();
        run = false;
        pdtf = this.gameObject.AddComponent<CustomSecondTrigger>();
        pdtf.Sendee = gameObject;
    }
    bool check_dist()
    {
        if (playerPosition == null)
            return false;
        destination = playerPosition.position;
        Vector3 kaban_position = new Vector3(kaban_body.position.x, kaban_body.position.y, 0);
        float dist = Mathf.Abs((destination - kaban_position).magnitude);
        return dist > 1.5f && dist < 10f;
    }
	// Update is called once per frame
	void Update () {
        run = check_dist();
        if (run == true)
        {
            destination = playerPosition.position;
            //kaban_velocity += kaban_acceleration;
            Vector3 kaban_position = new Vector3(kaban_body.position.x, kaban_body.position.y, 0);
            kaban_body.velocity = kaban_velocity * (destination - kaban_position).normalized;
            kaban_body.MovePosition(kaban_body.position + kaban_body.velocity);            
        }
        else
        {
            kaban_body.velocity = Vector3.zero;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Debug.Log("suda podoshol");
            run = true;
            kaban_velocity = 0.05f;
            //kaban_acceleration = 0.1f;
            playerPosition = collision.transform;
            destination = playerPosition.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            run = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            run = false;
            Vector3 kaban_position = new Vector3(kaban_body.position.x, kaban_body.position.y, 0);
            kaban_body.velocity = (destination - kaban_position);
            collision.collider.GetComponent<Player>().changeHP(-5f);
            //Physics2DExtension.AddForce(collision.collider.transform.GetComponent<Rigidbody2D>(),
              // 10*kaban_body.mass*kaban_body.velocity, ForceMode.Impulse);
        }
        if (collision.collider.name == "tree(Clone)")
        {
            collision.collider.GetComponent<TreeBehaviour>().destroy();
        }
    }
}

public class CustomSecondTrigger : MonoBehaviour
{
    private GameObject sendee;
    public GameObject Sendee
    {
        get { return sendee; }
        set { sendee = value; }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        //can send message to whomever or run our code here
        collider.GetComponent<Player>().changeHP(-5f);
        Debug.Log("pizda tebe");
    }
}