using UnityEngine;
using System.Collections;

public class simpleAI : MonoBehaviour {

	public float speed = .01f;
	public GameObject player;
	public Transform player_loc;
    public int health = 10;
    public float jumping = 200;
    Animator animator;
	bool jump;
	bool faceright;
	float pos_scale;
	// Use this for initialization
	void Start () {
		pos_scale = transform.localScale.x;
		faceright = true;
		jump = true;
		player = GameObject.FindGameObjectWithTag ("Player");
        animator = GetComponentInChildren<Animator>();
	}
	public void Defend(int dmg){
		health -= dmg;
        animator.SetTrigger("Hurt");
	}

	// Update is called once per frame
	void Update () {

		//move left
		Movement ();
		Attack ();
		//destroying the ai when no health
		/*if (health <= 0){
			Destroy(gameObject);
		}*/
	}
	void Attack(){
		//getting distance between game object and player 
		float distance = Vector3.Distance (player.transform.position, gameObject.transform.position);
		//play attack animation and stop jumping
        animator.SetBool("Walking", true);
        animator.SetBool("Idle", false);
        if (distance < 3)
        {
			jump = false;
		}
	}

	void Leap(){
		if (jump == true) {
			rigidbody2D.AddForce (Vector3.up * jumping);
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
