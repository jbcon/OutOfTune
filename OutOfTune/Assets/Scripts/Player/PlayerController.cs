using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float maxSpeed;
    public float jumpForce;
    public bool grounded = false;
    public bool facingRight = true;
    public Transform groundedEnd;

    uint numJumps = 0;

    //Player's weapon inventory
    HashSet<GameObject> weapons;
    GameObject currentWeapon;
    Transform tf;

	// Use this for initialization
	void Start ()
    {
        tf = GetComponent<Transform>();
        weapons = new HashSet<GameObject>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        Jumping();

        if (currentWeapon) UseWeapon();

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
            weapons.Add(collision.gameObject);
            currentWeapon = collision.gameObject;
            currentWeapon.transform.SetParent(tf);
            currentWeapon.transform.localPosition = Vector3.zero;
        }
    }

    void UseWeapon()
    {
        //localizes mouse position to screen space
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 1;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        Debug.DrawLine(tf.position, mousePos);
        Vector2 direction = new Vector2(mousePos.x-tf.position.x, mousePos.y - tf.position.y);
        direction.Normalize();

        //orient the weapon to mouse
        //theta is in degrees
        float theta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log("Theta: " + theta + ", mousePos: " + mousePos);
        if (theta > 90 || theta < -90) facingRight = false;
        else facingRight = true;

        //change direction of weapon
        if (!facingRight)
        {
            currentWeapon.transform.rotation = Quaternion.Euler(0, 180, 180-theta);
        }
        else
        {
            currentWeapon.transform.rotation = Quaternion.Euler(0, 0, theta);
        }


        if (Input.GetButtonDown("Fire1"))
            currentWeapon.GetComponent<WeaponProperties>().Fire(direction);
    }

    void CharacterMovement()
    {
        float move = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(move * maxSpeed, rigidbody2D.velocity.y);
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
