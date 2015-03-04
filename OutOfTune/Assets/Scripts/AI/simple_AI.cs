using UnityEngine;
using System.Collections;

public class simple_AI : MonoBehaviour {
	public float speed;
	public GameObject player;
	public int health;
	public float jumping;
	bool jump;
	// Use this for initialization
	void Start () {
		health = 10;
		jump = true;
		player = GameObject.FindGameObjectWithTag ("Player");
		jumping = 200;
		speed = .01f;
		InvokeRepeating ("leap", 0f, 2f);

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

		//transform.Translate(-speed,0,0);
		transform.Translate (Vector3.left * speed);

	}
}
