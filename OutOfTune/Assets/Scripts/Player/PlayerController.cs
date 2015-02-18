using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed;
    public float jumpForce;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void FixedUpdate()
    {
        CharacterMovement();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO: make character jump only once (or twice?) until they hit the ground
    }

    void CharacterMovement()
    {
        LeftRightMovement();
        Jumping();
    }

    void LeftRightMovement()
    {
        float move = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(move * maxSpeed,
            rigidbody2D.velocity.y);
    }
    void Jumping()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rigidbody2D.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
    }
}
