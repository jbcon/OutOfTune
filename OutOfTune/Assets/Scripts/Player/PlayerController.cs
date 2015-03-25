﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float maxSpeed;
    public float jumpForce;
    public bool grounded = false;
    public bool facingRight = true;
    public Transform groundedEnd;
    //Player's weapon inventory
    public bool gamepadConnected = false;
    public WeaponManager weaponManager;
    private int numJumps = 0;
    private Transform tf;
    private Vector2 direction;
    private bool firingAxisInUse = false;

	// Use this for initialization
	void Start ()
    {
        tf = gameObject.transform;
        weaponManager = FindObjectOfType<WeaponManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        Jumping();
        processInput();
        UseWeapon();

	}

    void FixedUpdate()
    {
        CharacterMovement();
    }

    void processInput()
    {
        //change, aim weapon
        if (Input.GetButtonDown("Swap"))
        {
            weaponManager.SwapWeapons();
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        //equip weapon
        if (collision.gameObject.CompareTag("Weapon"))
        {
            /*if (!weapons.Contains(collision.gameObject))
            {
                weapons.Add(collision.gameObject);
            }*/
        }
    }

    void UseWeapon()
    {
        Weapon w = weaponManager.currentWeapon;

        /* AIM */

        //if gamepad is connected
        if (gamepadConnected)
        {
            Vector2 newDirection = new Vector2(Input.GetAxis("AimX"), Input.GetAxis("AimY"));
            if (newDirection.magnitude > 0)
            {
                direction = newDirection;
            }
        }
        else
        {
            //localizes mouse position to screen space
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            Debug.DrawLine(tf.position, mousePos);
            direction = new Vector2(mousePos.x - tf.position.x, mousePos.y - tf.position.y);
        }

        direction.Normalize();
        //orient the weapon to mouse
        //theta is in degrees
        float theta = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        if (theta > 90 || theta < -90) facingRight = false;
        else facingRight = true;

        //change direction of weapon
        if (!facingRight)
        {
            weaponManager.transform.rotation = Quaternion.Euler(0, 180, 180-theta);
        }
        else
        {
            weaponManager.transform.rotation = Quaternion.Euler(0, 0, theta);
        }

        float analogFire = Input.GetAxisRaw("AnalogFire");
        

        //full auto weapon, keep firing as long as button held down
        if (w.weaponType == WeaponType.FullAuto)
        {
            if (Input.GetButton("Fire1"))
            {
                weaponManager.FireCurrentWeapon(direction, weaponManager.transform);
            }
            //if the controller input is firing
            else if (analogFire < 0)
            {
                weaponManager.FireCurrentWeapon(direction, weaponManager.transform);
            }
        }
        //semi-auto weapon
        else
        {
            if (Input.GetButtonDown("Fire1"))
            {
                //check if melee or projectile weapon
                //MeleeProperties m = inventory[wepIndex].GetComponent<MeleeProperties>();
                //if it is a projectile weapon
                weaponManager.FireCurrentWeapon(direction, weaponManager.transform);
                //if it is a melee weapon
                /*else if (m)
                {
                    m.Fire(direction);
                }*/
            }
            else if (analogFire < 0 && !firingAxisInUse)
            {
                Debug.Log("Right");
                weaponManager.FireCurrentWeapon(direction, weaponManager.transform);
                firingAxisInUse = true;
            }
            
            else if (analogFire == 0 && firingAxisInUse)
            {
                firingAxisInUse = false;
            }
        }
        
    }

    void CharacterMovement()
    {
        float move = Input.GetAxis("Movement");

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed * Time.deltaTime, gameObject.GetComponent<Rigidbody2D>().velocity.y);

    }
    void Jumping()
    {
        //check for collision with ground
        //player can double jump
        grounded = Physics2D.Linecast(tf.position, groundedEnd.position, 1 << LayerMask.NameToLayer("Ground"));
        if (grounded) numJumps = 0;

        if (Input.GetButtonDown("Jump") && numJumps < 1)
        {
            //this is done so both jumps have same total force
            //there's probably a better way
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            
			gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            numJumps++;
        }
    }
}
