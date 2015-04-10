using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Health))]
public class GeneralAI {
	public GameObject projectile;
	public GameObject player;
	public GameObject self;
	public float range;
	public Health currenthealth;
	public bool faceright;
	public float pos_scale;
	public float speed;

	public GeneralAI(){
		range = 20f;
		speed = 20f;
	}
	public void SetPos(float posx){
		pos_scale = posx;
	}
	public float GetRange(){
		return range;
	}
	public void CreateBasicStats(GameObject obj,GameObject proj){
		currenthealth = obj.GetComponent<Health>();
		self = obj;
		projectile = proj;	
	}
	public float GetHealth(){
		return currenthealth.health;
	}
	public GameObject GetSelf(){
		return self;
	}
	public void SetPlayer(GameObject play){
		player = play;
	}
	public GameObject GetPlayer(){
		//Debug.Log("jacky" + player.transform.position);
		return player;
	}
	public virtual void Movement(){
		// using the point to determine if the ai is on the left or right side of the player
		Vector3 point = player.gameObject.transform.InverseTransformPoint (self.transform.position);
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
			self.transform.localScale = new Vector2(-pos_scale, self.transform.localScale.y);
			//left side of the player move left
			self.transform.Translate (Vector3.left * speed * Time.deltaTime);
		}
		else
		{
			self.transform.localScale = new Vector2(pos_scale, self.transform.localScale.y);
			//right side of the player move right
			self.transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
	}
	public virtual void FireBullet(){
		GameObject b = GameObject.Instantiate(projectile) as GameObject;
		//assuming the object is facing right 
		//moving it out of collision zone

		Vector3 point = player.gameObject.transform.InverseTransformPoint (self.transform.position);
		if (point.x > 0){
			b.transform.position = new Vector3(self.transform.position.x - 6,self.transform.position.y ,self.transform.position.z);  
			b.GetComponent<Rigidbody2D>().AddForce(Vector3.left * 10, ForceMode2D.Impulse);
		}else if (point.x < 0){
			b.transform.position = new Vector3(self.transform.position.x + 6,self.transform.position.y ,self.transform.position.z);  
			b.GetComponent<Rigidbody2D>().AddForce(Vector3.right * 10, ForceMode2D.Impulse);
		}

		if (true)
			b.GetComponent<Rigidbody2D>().AddTorque(Random.Range(-100, 100)); 
	}
}
