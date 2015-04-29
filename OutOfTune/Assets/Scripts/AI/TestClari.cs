using UnityEngine;
using System.Collections;

public class TestClari : MonoBehaviour {
	public GameObject projectile;
	public GameObject play;
	public bool chased;
	Animator animator;
	private Vector3 original_position;
	private float lerpposition;//getting back to original positions
	private float lerptime;	// time it shall take to get back to orig pos
	private Vector3 temp_pos;
	public int duration; // the duration on how long the AI wants to keep patroling
	public int movementspeed; // the speed at which the object is going to move at

	private int counter;		//count the number of frams until flip
	TestClariAI testing = new TestClariAI();
	void Start ()
	{
		play = GameObject.FindGameObjectWithTag ("Player");
		//BrassiTest testing = new BrassiTest();
		testing.SetPos(gameObject.transform.localScale.x);
		testing.CreateBasicStats(gameObject,projectile);
		testing.SetPlayer(play);
		lerpposition = 0f;
		lerptime = 5f;
		duration = 30;
		movementspeed = 5;
		original_position = gameObject.transform.localPosition;
		animator = gameObject.GetComponentInChildren<Animator>();
		if (animator == null)
		{
			Debug.Log ("still not working");
		}
		counter = 0;
		chased = false;
	}
	public void OnTriggerStay2D(Collider2D collider)
	{
		testing.OnTriggerStay2D(collider);
	}
	void Update(){
		//gameObject.transform.Translate(Vector3.left * 10f * Time.deltaTime);
		if (testing.stunned == false && testing.pause == false){
			animator.SetBool("moving", true);
			//animate moving and move go on patrol
			if (counter == 0){
				gameObject.transform.localScale = new Vector2(-testing.pos_scale, testing.self.transform.localScale.y);
			}else if ( counter >= 650){
				counter = 0;
				gameObject.transform.localScale = new Vector2(-testing.pos_scale, testing.self.transform.localScale.y);
			}else if (counter >= 330){
				gameObject.transform.localScale = new Vector2(testing.pos_scale, testing.self.transform.localScale.y);
			}

			gameObject.transform.position = new Vector3(Mathf.PingPong(Time.time *movementspeed,duration) + original_position.x,gameObject.transform.localPosition.y,gameObject.transform.localPosition.z);
			temp_pos = gameObject.transform.localPosition;
		}else{
			//keep it from moving
			gameObject.transform.position = temp_pos;
		}
		counter ++;
	}
	public void pausegame(){
		testing.pausegame();
	}
	public void unpausegame(){
		testing.unpausegame();
	}
	public void FireBullet(){
		testing.FireBullet();
	}
	void OnReceiveDamage(float dmg)
	{        
		if (!testing.stunned)
		{
			testing.currenthealth.health -= dmg;
			if (testing.currenthealth.health > 0)
			{
				StartCoroutine(Stun());
				//Debug.Log("AI Brassi script received damage");
			}
		}
		Debug.Log(testing.currenthealth.health);
	}
	/*
	IEnumerator MoveLeft(){
		while(inmotion = true){
			//right side of the player move right
			gameObject.transform.Translate(Vector3.right * 1 * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}
	IEnumerator MoveRight(){
		while(inmotion = false){
			//right side of the player move right
			gameObject.transform.Translate(Vector3.left * 1 * Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}*/
	IEnumerator Stun()
	{
		
		animator.SetBool("damaged",true);
		//testing.animator.SetTrigger("Stun");
		testing.stunned = true;
		if (!testing.grounded)
		{
			yield return new WaitForEndOfFrame();
		}
		else
		{
			yield return new WaitForSeconds(0.5f);
		}
		testing.stunned = false;
		animator.SetBool("damaged",false);
	}
}
[System.Serializable]
public class TestClariAI : GeneralAI{
	public TestClariAI(){
		range = 20f;
		
	}
}
