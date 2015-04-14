using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour {

    public float maxSpeed;
    public float health = 5;
    public float invincibilityTime = 1f;
    public float jumpForce;
    public float aimRange;
    //Player's weapon inventory
    public bool gamepadConnected = false;
    public WeaponManager weaponManager;

    private Animator animator;
    private Transform tf;
    private Vector2 direction;
    private GameObject playerSprite;
    private CircleCollider2D footCollider;
    private BoxCollider2D bodyCollider;
    private GameObject violin;
    private int ignoredPlatformMask;
    private bool invincible = false;
    private int numJumps = 0;
	private Vector3 checkpointpos; //used to do saving the positon of player

    //States
    public bool facingRight = true;
    public bool grounded = false;
    public bool attacking = false;     //melee attacking

    private bool firingAxisInUse = false;

	// Use this for initialization
	void Start ()
    {
        tf = gameObject.transform;
        weaponManager = FindObjectOfType<WeaponManager>();
        violin = GameObject.FindGameObjectWithTag("Violin");
        violin.SetActive(false);

        ignoredPlatformMask = 1 << LayerMask.NameToLayer("Platform");
        playerSprite = transform.Find("Renee").gameObject;
        animator = GetComponentInChildren<Animator>();
        footCollider = GetComponent<CircleCollider2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        Jumping();
        UseWeapon();
        UseMeleeWeapon();
	}

    void FixedUpdate()
    {
        CharacterMovement();
    }
	void OnLevelWasLoaded(int level){
		//when level is loaded save the objects position
		checkpointpos = gameObject.transform.position;
	}
	void loadCheckpoint(){
		gameObject.transform.position = checkpointpos;
	}
	public void newcheckpoint(){

		checkpointpos = gameObject.transform.position;
	}

	public float gethealth(){
		return health;
	}

    //checks if foot collider is on the ground
    void OnCollisionEnter2D(Collision2D collision)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");
        int platLayer = LayerMask.NameToLayer("Platform");
        if (collision.gameObject.layer == groundLayer || collision.gameObject.layer == platLayer)
        {
            //Determine if it was on the body or the feet
            ContactPoint2D[] cps = collision.contacts;
            foreach (ContactPoint2D cp in cps)
            {
                if (cp.otherCollider == footCollider)
                {
                    grounded = true;
                }
            }
        }

        //enemy checking
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        //equip weapon
        if (collision.gameObject.layer == enemyLayer && !invincible)
        {
            health--;
            StartCoroutine(Invincibility());
            if (health <= 0)
            {
				loadCheckpoint();
                //Destroy(gameObject);
                //Application.Quit();
            }
        }

    }

    void OnCollisionExit2D(Collision2D collision)
    {
        int groundLayer = LayerMask.NameToLayer("Ground");
        int platLayer = LayerMask.NameToLayer("Platform");
        if (collision.gameObject.layer == groundLayer || collision.gameObject.layer == platLayer)
        {
            //Determine if it was on the body or the feet
            ContactPoint2D[] cps = collision.contacts;
            foreach (ContactPoint2D cp in cps)
            {
                if (cp.otherCollider == footCollider)
                {
                    grounded = false;
                }
            }
        }

    }

    void UseMeleeWeapon()
    {
        //player does melee attack
        if (Input.GetButtonDown("Fire2"))
        {
            animator.SetTrigger("Attacking");
        }
        violin.SetActive(attacking);
    }

    void UseWeapon()
    {
        Weapon w = weaponManager.currentWeapon;
        /* AIM */

        //if shaking, stabilize before calculations
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        camera.transform.localPosition = new Vector3(0.0f, 0.0f, camera.transform.localPosition.z);

        

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
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 1;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);
            //if the weapon is a flute, do some camera stuff
            if (w is Flute)
            {
                Vector3 mouseRelPos = tf.InverseTransformPoint(mousePos)*aimRange;
                Debug.Log(mouseRelPos);
                camera.transform.localPosition = new Vector3(mouseRelPos.x, mouseRelPos.y, camera.transform.localPosition.z);
            }
            else
            {
                camera.transform.localPosition = new Vector3(0.0f, 0.0f, camera.transform.localPosition.z);
            }
            //localizes mouse position to screen space
           
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
            playerSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            weaponManager.transform.rotation = Quaternion.Euler(0, 0, theta);
            playerSprite.transform.rotation = Quaternion.Euler(0, 180, 0);

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
        
        //transition between idle and walking animations if moving left or right
        //NOT based solely on input
        if (Mathf.Abs(move) > 0 )
        {
            animator.SetBool("Idle", false);
        }
        else
        {
            animator.SetBool("Idle", true);
        }

    }
    void Jumping()
    {
        //check for collision with ground
        //player can double jump
        /*grounded = Physics2D.Linecast(tf.position, groundedEnd.position, (1 << LayerMask.NameToLayer("Ground")) 
            | (1 << LayerMask.NameToLayer("Platform")));
        */

        if (Input.GetButtonDown("Jump") && numJumps < 1)
        {
            //this is done so both jumps have same total force
            //there's probably a better way
			gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(gameObject.GetComponent<Rigidbody2D>().velocity.x, 0);
            
			gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            numJumps++;
        }

        //put afterwards to allow only one jump in midair
        if (grounded) numJumps = 0;

    }

    private IEnumerator Invincibility()
    {
        invincible = true;

        foreach (SpriteRenderer sr in playerSprite.GetComponentsInChildren<SpriteRenderer>())
            sr.material.color = new Vector4(0.5f, 0.0f, 0.0f, 1.0f);
        yield return new WaitForSeconds(invincibilityTime);
        foreach (SpriteRenderer sr in playerSprite.GetComponentsInChildren<SpriteRenderer>())
            sr.material.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
        invincible = false;
    }
}
