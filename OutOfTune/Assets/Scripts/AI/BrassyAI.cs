﻿using UnityEngine;
using System.Collections;

public class BrassyAI : MonoBehaviour {
	//walking forward and shooting at different 
	public GameObject player;
	public GameObject projectile;
	public int speed = 20;
	public Transform player_loc;
	public int health = 10;
	public float jumping = 200f;
	public Health currenthealth;

	//how far until it can't see the player
	public float range = 50.0f;

	bool jump;
	bool faceright;
	float pos_scale;
	// Use this for initialization
	void Start () {
		currenthealth = new Health();
		//Health currenthealth = gameObject.GetComponent<Health();
		pos_scale = transform.localScale.x;
		faceright = true;
		jump = true;
		player = GameObject.FindGameObjectWithTag ("Player");
		player_loc = player.transform;
	}
	public void Defend(int dmg){
		health -= dmg;
		if (health <= 0)
		{
			gameObject.GetComponent<CircleCollider2D>().enabled = false;
			gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
			StartCoroutine("Die");
		}
	}
	public void BrassyFire(){
		GameObject b = GameObject.Instantiate(projectile) as GameObject;
		//assuming the object is facing right 
		//moving it out of collision zone
		b.transform.position = new Vector3(gameObject.transform.position.x - 6,gameObject.transform.position.y ,gameObject.transform.position.z);  

		//Debug.Log(b.transform.position);
		//b.transform.position = GameObject.FindWithTag("Reticle").transform.position;

		//treat it as an angle
		//float spreadModifier = Random.Range(-2, 2);

		//Vector2 direction = new Vector2(2,3);
		//rotates direction by amount of spread
		//Vector3 spreadVector = Quaternion.Euler(0.0f, 0.0f, spreadModifier) * direction;

		b.GetComponent<Rigidbody2D>().AddForce(Vector3.left, ForceMode2D.Impulse);
		if (true)
			b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100));       //put a spin on it so it looks nice
		 
	}
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		
		//move only if not hurt
		if (distance < range && health > 0)
		{
			//animator.SetBool("Walking", true);
			//animator.SetBool("Idle", false);
			Movement();
			//BrassyFire();
		}
		/*else
		{
			Debug.Log("doing nothing");
			//animator.SetBool("Walking", false);
			//animator.SetBool("Idle", true);
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
	private IEnumerator Die()
	{
		//animator.SetTrigger("Hurt");
		yield return new WaitForSeconds(1.0f);
		Destroy(gameObject);
	}
}
