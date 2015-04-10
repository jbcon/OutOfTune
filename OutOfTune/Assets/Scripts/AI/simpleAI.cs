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
	// Use this for initialization
	void Start () {
		pos_scale = transform.localScale.x;
		faceright = true;
		jump = true;
		player = GameObject.FindGameObjectWithTag ("Player");
        player_loc = player.transform;
        animator = GetComponentInChildren<Animator>();
        health = GetComponent<Health>();
	}
	

	// Update is called once per frame
	void Update () {
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

    
}
