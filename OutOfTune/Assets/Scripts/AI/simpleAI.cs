using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Health))]
public class simpleAI : MonoBehaviour {

	public float speed = .01f;
	public GameObject player;
	public Transform player_loc;
    public float jumping = 200f;

    //how far until it can't see the player
    public float range = 50.0f;

    Animator animator;
	bool jump;
	bool faceright;
	float pos_scale;
    Health health;
    private bool stunned;
    private bool grounded;      //go to your room young man
	private bool pause;
    // Use this for initialization
	void Start () {
		pos_scale = transform.localScale.x;
		faceright = true;
		jump = true;
        stunned = false;
        grounded = false;
		pause = false;
		player = GameObject.FindGameObjectWithTag ("Player");
        player_loc = player.transform;
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
	}
	public void pausegame(){
		pause = true;
	}
	public void unpausegame(){
		pause = false;
	}

	// Update is called once per frame
	void Update () {
		if ( pause == false && stunned == false){
	        float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
	        
			//move only if not hurt
	        if (distance < range && !animator.GetBool("Die") && health.health > 0)
	        {
	            animator.SetBool("Walking", true);
	            animator.SetBool("Idle", false);
	            Movement();
	        }
	        else
	        {
	            animator.SetBool("Walking", false);
	            animator.SetBool("Idle", true);
	        }

			//destroying the ai when no health
			/*if (health <= 0){
				Destroy(gameObject);
			}*/
		}
	}

    //check if grounding box says it's grounded
    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Ground")
            || collider.gameObject.layer == LayerMask.NameToLayer("Platform"))
        {
            grounded = true;
        }   
    }

    void OnReceiveDamage(float dmg)
    {        
        if (!stunned)
        {
            health.health -= dmg;
            StartCoroutine(Stun());
            if (health.health > 0 && !stunned)
            {
                Debug.Log("AI script received damage");
            }
        }
    }

	void Leap(){
		if (jump == true) {
			gameObject.GetComponent<Rigidbody2D>().AddForce (Vector3.up * jumping);
			//rigidbody2D.AddForce (Vector3.up * jumping);
		}
	}
	void Movement(){
		// using the point to determine if the ai is on the left or right side of the player
        
		Vector3 point = player_loc.InverseTransformPoint (transform.position);
		if (point.x > 0) {
            if (player.GetComponent<PlayerController>().grounded)
                faceright = true;
		} else if (point.x < 0) {
            if(player.GetComponent<PlayerController>().grounded)
                faceright = false;
		}

        if (faceright == true)
        {
            //rotate the sprite to face the correct direction if hes on the left
            transform.localScale = new Vector2(-pos_scale, transform.localScale.y);
            //left side of the player move left
			transform.Translate (Vector3.left * speed * Time.deltaTime);
        }
        else
        {
            transform.localScale = new Vector2(pos_scale, transform.localScale.y);
            //right side of the player move right
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
	}

    IEnumerator Stun()
    {
        animator.SetTrigger("Stun");
        stunned = true;
        while(!grounded)
        {
            yield return new WaitForFixedUpdate();
        }

        yield return new WaitForSeconds(0.5f);
        stunned = false;
    }
    
}
