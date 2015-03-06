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
        if (Input.GetKeyDown("tab"))
        {
            SwapWeapons();
        }

    }

    void SwapWeapons()
    {
        inventory[wepIndex].SetActive(false);
        wepIndex++;
        if (wepIndex > inventory.Count-1) wepIndex = 0;
        inventory[wepIndex].SetActive(true);

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


        if (Input.GetButtonDown("Fire1"))
            inventory[wepIndex].GetComponent<WeaponProperties>().Fire(direction);
    }

    void CharacterMovement()
    {
        float move = Input.GetAxis("Horizontal");
        rigidbody2D.velocity = new Vector2(move * maxSpeed * Time.deltaTime, rigidbody2D.velocity.y);
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
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0);
            
            rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            numJumps++;
        }
    }
}
