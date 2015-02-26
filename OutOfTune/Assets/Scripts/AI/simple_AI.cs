using UnityEngine;
using System.Collections;

public class simple_AI : MonoBehaviour {
	public float speed;
	public GameObject player;
	public int health;
	public float index;
	public float jumping;
	bool jump;
	// Use this for initialization
	void Start () {
		jump = true;
		index = 0;
		player = GameObject.FindGameObjectWithTag ("Player");
		jumping = .01f;
		speed = .01f;

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
	}
	void attack(){
		//getting distance between game object and player 
		float distance = Vector3.Distance (player.transform.position, gameObject.transform.position);
		if (distance < 3) {
			jump = false;
			//play attack animation and stop jumping
		}
	}
	void OnCollisionEnter2D(Collision2D collider){
		rigidbody2D.AddForce(Vector3.up * 100);
	}
	void movement(){
		/*
		 * 
		 * Using sin to jump up and down as the AI moves across the screen
		 * kinda of weird looking just leaving this here as a reminder
		 * how not to realy do it
		 * */
		/*
		index += Time.deltaTime;
		//moving left into the player
		//			rate it goes up		* curve up
		jumping = .009f * Mathf.Sin(.5f * index);
		Debug.Log (jumping);
		attack ();
		if (jump == true) {
			transform.Translate (-speed, jumping, 0);
		} else if (jump == false && gameObject.transform.position.y > 0) {

			transform.Translate (-speed, -1, 0);
		} else {
			transform.Translate (-speed, 0, 0);
		}
		*/
		if (transform.position.y > 2){
			jumping = jumping * -1;
			Debug.Log("hi");
		}
		transform.Translate(-speed,jumping,0);
	}
}
