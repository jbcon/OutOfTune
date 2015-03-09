using UnityEngine;
using System.Collections;

public class simple_AI : MonoBehaviour {
	public float speed = .01f;
	public GameObject player;
	public Transform player_loc;
    public int health = 10;
    public float jumping = 200;
	bool jump;
	bool faceright;
	float pos_scale;
	// Use this for initialization
	void Start () {
		pos_scale = transform.localScale.x;
		faceright = true;
		jump = true;
		player = GameObject.FindGameObjectWithTag ("Player");
		//InvokeRepeating ("leap", 0f, 2f);

	}
	public void defend(int dmg){
		health -= dmg;
	}
	public void update_x(float new_x){
		speed = new_x;
	}
	public void update_y(float new_y){
		jumping = new_y;
	}

	// Update is called once per frame
	void Update () {

		//move left
		movement ();
		attack ();
		//destroying the ai when no health
		if (health <= 0){
			Destroy(gameObject);
		}
	}
	void attack(){
		//getting distance between game object and player 
		float distance = Vector3.Distance (player.transform.position, gameObject.transform.position);
		//play attack animation and stop jumping
		if (distance < 3) {
			jump = false;
		}
	}
	// bounce/ jump every time collide with something
	/*void OnCollisionEnter2D(Collision2D collider){
		if (jump == true) {
			rigidbody2D.AddForce (Vector3.up * jumping);
		}
	}*/
	void leap(){
		if (jump == true) {
			rigidbody2D.AddForce (Vector3.up * jumping);
		}
	}
	void movement(){
		if (faceright == true) {
			//rotate the sprite to face the correct direction if hes on the left
			transform.localScale = new Vector2 (-pos_scale, transform.localScale.y);
			faceright = false;
		} 
		// using the point to determine if the ai is on the left or right side of the player
		Vector3 point = player_loc.InverseTransformPoint (transform.position);
		if (point.x > 0) {
			//left side of the player move left
			transform.Translate (Vector3.left * speed * Time.deltaTime);
		} else if (point.x < 0) {
			//right side of the player move right
			transform.Translate (Vector3.right * speed * Time.deltaTime);
		}
	}

}
