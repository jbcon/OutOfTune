using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed;
    public float jumpForce;
    public bool grounded = false;
    public Transform groundedEnd;

    public int numJumps = 0;
    Transform tf;

	// Use this for initialization
	void Start ()
    {
        tf = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Jumping();
	}

    void FixedUpdate()
    {
        CharacterMovement();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //equip weapon
        if (collision.gameObject.CompareTag("Weapon"))
        {
            collision.transform.SetParent(tf);
            collision.transform.localPosition = Vector3.zero;
        }
        //TODO: make character jump only once (or twice?) until they hit the ground


    }

    void CharacterMovement()
    {
        LeftRightMovement();
    }

    void LeftRightMovement()
    {
        float move = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(move * maxSpeed,
            rigidbody2D.velocity.y);
    }
    void Jumping()
    {
        //check for collision with ground
        grounded = Physics2D.Linecast(tf.position, groundedEnd.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded) numJumps = 0;

        if (Input.GetButtonDown("Jump") && numJumps != 2)
        {
            //this is done so both jumps have same total force
            //there's probably a better way
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            numJumps++;
        }
    }
}
