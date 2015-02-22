using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float maxSpeed;
    public float jumpForce;
    public bool grounded = false;
    public Transform groundedEnd;

    uint numJumps = 0;

    GameObject weapon;
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

        if (weapon && Input.GetButtonDown("Fire1")) UseWeapon();
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
            weapon = collision.gameObject;
            weapon.transform.SetParent(tf);
            weapon.transform.localPosition = Vector3.zero;
        }
    }

    void UseWeapon()
    {
        //localizes mouse position to screen space
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Debug.DrawLine(tf.position, mousePos);
        Vector2 direction = new Vector2(mousePos.x-tf.position.x, mousePos.y - tf.position.y);
        direction.Normalize();
        weapon.GetComponent<WeaponProperties>().Fire(direction);
    }

    void CharacterMovement()
    {
        float move = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(move * maxSpeed,
            rigidbody2D.velocity.y);
    }
    void Jumping()
    {
        //check for collision with ground
        //player can double jump
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
