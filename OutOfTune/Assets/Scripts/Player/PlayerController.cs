using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour {

    [System.Serializable]
    public class GroundComponents
    {
        /* this class contains three empty GameObjects, where
         * they are positioned left, center and right of the player.
         * Linecasts are used to determine if it is grounded
         */
        public GameObject groundLeft;
        public GameObject groundCenter;
        public GameObject groundRight;
    }

    public WeaponManager weaponManager;
    public GroundComponents groundComponents;
    public float maxSpeed;
    public float health = 5;
    public float invincibilityTime = 1f;
    public float jumpForce;
    public float aimRange;
    //Player's weapon inventory
    public bool gamepadConnected = false;
    
    private Animator animator;
    private Transform tf;
    private Vector2 direction;
    private GameObject playerSprite;
    //private CircleCollider2D footCollider;
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

    //max angle of aim rotation
    public float rotationRange = 80f;

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
        //footCollider = GetComponent<CircleCollider2D>();
        bodyCollider = GetComponent<BoxCollider2D>();

	}
	
	// Update is called once per frame
	void Update ()
    {
        CheckGround();
        Jumping();
        if (!attacking)
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
		//moving player to save checkpoint location
		gameObject.transform.position = checkpointpos;
	}
	public void newcheckpoint(){
		//upon reaching a new checkpoint save it
		checkpointpos = gameObject.transform.position;
	}

	public float gethealth(){
		return health;
	}

    void CheckGround()
    {
        int groundLayer = 1 << LayerMask.NameToLayer("Ground") | 1 << LayerMask.NameToLayer("Platform");
        
        RaycastHit2D right = Physics2D.Linecast(transform.position, groundComponents.groundRight.transform.position, groundLayer);
        RaycastHit2D left = Physics2D.Linecast(transform.position, groundComponents.groundLeft.transform.position, groundLayer);
        RaycastHit2D center = Physics2D.Linecast(transform.position, groundComponents.groundCenter.transform.position, groundLayer);

        Debug.DrawLine(transform.position, groundComponents.groundRight.transform.position);
        Debug.DrawLine(transform.position, groundComponents.groundLeft.transform.position);
        Debug.DrawLine(transform.position, groundComponents.groundCenter.transform.position);

        if (right || left || center)
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
    }

    //checks if foot collider is on the ground
    void OnCollisionEnter2D(Collision2D collision)
    {

        //enemy checking
        int enemyLayer = LayerMask.NameToLayer("Enemy");
        //equip weapon
        if (collision.gameObject.layer == enemyLayer && !invincible)
        {
            health--;
            StartCoroutine(Invincibility());
            if (health <= 0)
            {
				//load last save checkpoint
				loadCheckpoint();
                //Destroy(gameObject);
                //Application.Quit();
            }
        }

    }
       
    void UseMeleeWeapon()
    {
        //player does melee attack
        if (Input.GetButtonDown("Fire2"))
        {
            animator.Play("Renee_Upper_Attack_Melee_Start");
        }
        violin.SetActive(attacking);
    }

    void UseWeapon()
    {
        Weapon w = weaponManager.currentWeapon;
        /* AIM */

        //if shaking, stabilize before calculations
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        //camera.transform.localPosition = new Vector3(0.0f, 0.0f, camera.transform.localPosition.z);

        

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
            Vector2 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 100f));
            //if the weapon is a flute, do some camera stuff
            //TODO: improve


            /*if (w is Flute)
            {
                Vector2 mouseRelPos = tf.InverseTransformPoint(mousePos)*aimRange;
                Debug.Log(mouseRelPos);
                camera.transform.localPosition = new Vector3(mouseRelPos.x, mouseRelPos.y, camera.transform.localPosition.z);
            }
            else
            {
                camera.transform.localPosition = new Vector3(0.0f, 0.0f, camera.transform.localPosition.z);
            }
             */
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
        if (!attacking)
        {
            weaponManager.gameObject.SetActive(true);
            //flip to face mouse cursor
            if (!facingRight)
            {
                //weaponManager.transform.rotation = Quaternion.Euler(0, 180, 180 - theta);
                playerSprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                //weaponManager.transform.rotation = Quaternion.Euler(0, 0, theta);
                playerSprite.transform.rotation = Quaternion.Euler(0, 180, 0);

            }

            float clampedAngle;
            //aim the weapon by scrubbing the aim animation
            //maps from -80 to 80?
            if (theta <= 90 && theta >= -90)
            {
                clampedAngle = Mathf.Clamp(theta, -rotationRange, rotationRange);

            }
            else if (theta >= -180 && theta < 0)
            {
                clampedAngle = -90+(-theta - 90);
                clampedAngle = Mathf.Clamp(clampedAngle, -rotationRange, rotationRange);
            }
            else
            {
                clampedAngle = 90- (theta - 90);
                clampedAngle = Mathf.Clamp(clampedAngle, -rotationRange, rotationRange);
            }

            clampedAngle = (clampedAngle + rotationRange) / (2.0f * rotationRange);
            Debug.Log(clampedAngle);

            animator.Play("Renee_Aim_Trombone", animator.GetLayerIndex("Upper Layer"), clampedAngle);

        }
        else
        {
            weaponManager.gameObject.SetActive(false);
        }

        float analogFire = Input.GetAxisRaw("AnalogFire");
        

        //full auto weapon, keep firing as long as button held down
        if (w.weaponType == WeaponType.FullAuto)
        {
            if (Input.GetButton("Fire1"))
            {
                weaponManager.FireCurrentWeapon(weaponManager.transform);
            }
            //if the controller input is firing
            else if (analogFire < 0)
            {
                weaponManager.FireCurrentWeapon(weaponManager.transform);
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
                weaponManager.FireCurrentWeapon(weaponManager.transform);
                //if it is a melee weapon
                /*else if (m)
                {
                    m.Fire(direction);
                }*/
            }
            else if (analogFire < 0 && !firingAxisInUse)
            {
                weaponManager.FireCurrentWeapon(weaponManager.transform);
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
            animator.SetTrigger("Jump");
        }

        //put afterwards to allow only one jump in midair
        if (grounded) numJumps = 0;
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("Yvel", gameObject.GetComponent<Rigidbody2D>().velocity.y);
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
