using UnityEngine;
using System.Collections;

public class ClariAi : MonoBehaviour {
	//Koopa behavior AI 
	//patroling a platform
	public float offsetorigin;// the offset in the x direction to the platform
	public int duration; // the duration on how long the AI wants to keep patroling
	public int movementspeed; // the speed at which the object is going to move at
	public GameObject player;
	public float speed;
	private float range; //distance before stop patroling
	private Vector3 original_position;
	private float lerpposition;//getting back to original positions
	private float lerptime;	// time it shall take to get back to orig pos
	float pos_scale;
	bool faceright;
	bool chased;
	// Use this for initialization
	void Start () {
		pos_scale = transform.localScale.x;
		offsetorigin = 40;
		duration = 30;
		speed = 5;
		movementspeed = 5;
		range = 50f;
		lerpposition = 0f;
		lerptime = 5f;
		faceright = true;
		chased = false;
		player = GameObject.FindGameObjectWithTag ("Player");
		original_position = gameObject.transform.position;
	}
	void Movement(){
		// using the point to determine if the ai is on the left or right side of the player
		Vector3 point = player.transform.InverseTransformPoint (transform.position);
		if (point.x > 0) {
			if (player.GetComponent<PlayerController>().grounded)
				faceright = true;
		} else if (point.x < 0) {
			if(player.GetComponent<PlayerController>().grounded)
				faceright = false;
		}
		
		if (faceright == true)
		{
			Debug.Log ("faceright");
			//rotate the sprite to face the correct direction if hes on the left
			transform.localScale = new Vector2(-pos_scale, transform.localScale.y);
			//left side of the player move left
			transform.Translate (Vector3.left * speed * Time.deltaTime);
			Debug.Log(transform.position);
		}
		else
		{
			transform.localScale = new Vector2(pos_scale, transform.localScale.y);
			//right side of the player move right
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
	}
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if (distance < range){
			//chased has begun and the player has made the AI get out of position
			chased = true;
			Movement();
			chased = false;
		}else{
			//if got out of position must return back to original position before going on patrol
			if (chased == true){
				Debug.Log ("not happening");
				lerpposition += Time.deltaTime/lerptime;
				transform.position= Vector3.Lerp(gameObject.transform.position, original_position, lerpposition);
			}else{
				gameObject.transform.position = new Vector3(Mathf.PingPong(Time.time *movementspeed,duration) + offsetorigin,gameObject.transform.position.y,gameObject.transform.position.z);
		
			}
		}
		/*//move only if not hurt
		if (distance < range && !animator.GetBool("Hurt") && health > 0)
		{
			animator.SetBool("Walking", true);
			animator.SetBool("Idle", false);
			Movement();
		}
		else
		{
			animator.SetBool("Walking", false);
			animator.SetBool("Idle", true);
		}*/
	}
}
