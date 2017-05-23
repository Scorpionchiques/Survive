using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarController : MonoBehaviour {

    Transform playerPosition;
    Vector3 destination;
    Rigidbody2D kaban_body;
    Animator kaban_animator;
    float kaban_velocity;
    float kaban_acceleration;
    bool chasing;

    float chaseThreshold = 1.5f; // distance within which to start chasing
    float giveUpThreshold = 20f; // distance beyond which AI gives up
    float attackThreshold = 2f; //distance beyond which ai starts to atack
    float atackTime;
    float attackCoulDown = 2.5f;
    // Use this for initialization

    void Start () {
        destination = Vector3.zero;
        playerPosition = null;
        kaban_velocity = 1.5f;
        kaban_acceleration = 0.025f;
        kaban_body = this.GetComponent<Rigidbody2D>();
        kaban_animator = this.GetComponent<Animator>();
        chasing = false;
    }
    bool isChasing()
    {
        if (playerPosition == null)
            return false;
        destination = playerPosition.position;
        Vector3 kaban_position = new Vector3(kaban_body.position.x, kaban_body.position.y, 0);
        float dist = Mathf.Abs((destination - kaban_position).magnitude);
        return dist > chaseThreshold && dist < giveUpThreshold;
    }
    bool isInAtackRange()
    {
        if (playerPosition == null)
            return false;
        destination = playerPosition.position;
        Vector3 kaban_position = new Vector3(kaban_body.position.x, kaban_body.position.y, 0);
        float dist = Mathf.Abs((destination - kaban_position).magnitude);
        return dist < attackThreshold;
    }

	// Update is called once per frame
	void Update () {
        if (chasing == true)
        {
            //kaban_animator.StopPlayback();
            destination = playerPosition.position;
            moveBoar(destination);
            chasing = isChasing();
            
        }
        else
        {
            //kaban_animator.StartPlayback();
            chasing = isChasing();
            kaban_body.velocity = Vector3.zero;
        }

        if (isInAtackRange() && Time.time > atackTime)
        {
            attackTarget();
            kaban_animator.SetInteger("Direction", 4);
            atackTime = Time.time + attackCoulDown;
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Debug.Log("suda podoshol");
            chasing = true;
            playerPosition = collision.transform;
            destination = playerPosition.position;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            chasing = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name == "Player")
        {
            chasing = false;
            Vector3 kaban_position = new Vector3(kaban_body.position.x, kaban_body.position.y, 0);
            //kaban_body.velocity = (destination - kaban_position);
            collision.collider.GetComponent<Player>().changeHP(-5f);
            //Physics2DExtension.AddForce(collision.collider.transform.GetComponent<Rigidbody2D>(),
              // 10*kaban_body.mass*kaban_body.velocity, ForceMode.Impulse);
        }
        if (collision.collider.name == "tree(Clone)")
        {
            collision.collider.GetComponent<TreeBehaviour>().destroy();
        }
    }

    //attack
    private void attackTarget()
    {
        playerPosition.GetComponent<Player>().changeHP(-5f);
    }

    //movement
    private void moveBoar(Vector2 destination)
    {
        moveInDirecrtion(calcDirection(destination));
    }
    private Vector2 calcDirection(Vector2 targetPosition)
    {
        var dir_vec = (targetPosition - kaban_body.position).normalized;
        return MoveDecider.direction(dir_vec.x, dir_vec.y);
    }
    private void moveInDirecrtion(Vector2 direction)
    {
        AnimatorMoveDecider.SetVagabondAnimationDirectionTest(kaban_animator, direction.x, direction.y);
        kaban_body.velocity = kaban_velocity * direction;
    }

}

