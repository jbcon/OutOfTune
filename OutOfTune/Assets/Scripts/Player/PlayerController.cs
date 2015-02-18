using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed = 10f;
    public float jumpForce = 10f;
    bool facingRight = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate()
    {
        CharacterMovement();
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
