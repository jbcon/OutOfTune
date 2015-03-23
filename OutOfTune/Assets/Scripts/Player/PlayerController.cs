using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float maxSpeed;
    public float jumpForce;
    public bool grounded = false;
    public bool facingRight = true;
    public Transform groundedEnd;
    //Player's weapon inventory
    public int wepIndex = 0;

    /*private record of inventory to instantiate
     * the weapons in weapons[]*/
    public List<GameObject> inventory;
    public int numJumps = 0;
    private Transform tf;
    private Vector2 direction;
    private bool firingAxisInUse = false;

	// Use this for initialization
	void Start ()
    {
        //retrieve weapons in children, place in list, set active appropriately.
        inventory = new List<GameObject>();
        tf = GetComponent<Transform>();
        Transform[] ts = GetComponentsInChildren<Transform>();
        for (int i = 0; i < ts.Length; i++)
        {
            GameObject tmp = ts[i].gameObject;
            if (tmp.CompareTag("Weapon"))
            {
                inventory.Add(tmp);
                tmp.SetActive(false);
            }
        }
        inventory[0].SetActive(true);
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
            SwapWeapons();
        }

    }

    void SwapWeapons()
    {
        /*inventory[wepIndex].SetActive(false);
        wepIndex++;
        if (wepIndex > inventory.Count-1) wepIndex = 0;
        inventory[wepIndex].SetActive(true);
        */
        SimpleWeapon w = inventory[wepIndex].GetComponent<SimpleWeapon>();
        if (w.weaponType == WeaponType.FullAuto)
            w.weaponType = WeaponType.SemiAuto;
        else
            w.weaponType = WeaponType.FullAuto;
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

        SimpleWeapon w = inventory[wepIndex].GetComponent<SimpleWeapon>();

        /* AIM */

        //if gamepad is connected
        if (Input.GetJoystickNames().Length > 0)
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
            inventory[wepIndex].transform.rotation = Quaternion.Euler(0, 180, 180-theta);
        }
        else
        {
            inventory[wepIndex].transform.rotation = Quaternion.Euler(0, 0, theta);
        }

        float analogFire = Input.GetAxisRaw("AnalogFire");
        

        //full auto weapon, keep firing as long as button held down
        if (w.weaponType == WeaponType.FullAuto)
        {
            if (Input.GetButton("Fire1"))
            {
                w.Fire(direction, true);
            }
            //if the controller input is firing
            else if (analogFire < 0)
            {
                w.Fire(direction, true);
            }
        }
        //semi-auto weapon
        else
        {
            Debug.Log(firingAxisInUse + ", " + analogFire);
            if (Input.GetButtonDown("Fire1"))
            {
                //check if melee or projectile weapon
                //MeleeProperties m = inventory[wepIndex].GetComponent<MeleeProperties>();
                //if it is a projectile weapon
                if (w)
                {
                    w.Fire(direction, true);
                }
                //if it is a melee weapon
                /*else if (m)
                {
                    m.Fire(direction);
                }*/
            }
            else if (analogFire < 0 && !firingAxisInUse)
            {
                Debug.Log("Right");
                w.Fire(direction, true);
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
