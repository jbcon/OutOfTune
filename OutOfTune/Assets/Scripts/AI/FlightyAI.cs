using UnityEngine;
using System.Collections;

public class FlightyAI : MonoBehaviour {
	public GameObject player;
	public GameObject projectile;
	private float movementspeed;
	private float duration;
	private Vector3 originalposition;
	private Vector3 temppos;
	private float lowerbound;
	private float upperbound;
	private bool chased;
	FlighyFightAI testing = new FlighyFightAI();
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		testing.SetPos(gameObject.transform.localScale.x);
		testing.CreateBasicStats(gameObject,projectile);
		testing.SetPlayer(player);
		movementspeed = 5;
		duration = 30;
		originalposition = gameObject.transform.localPosition;
		chased = false;
	}

	void CalculateBound(float pos){
		//make the object go at a slant to player at certain position
		lowerbound = pos - 2;
		upperbound = pos + 2;
	}
	void ReturnPos(){
		//moving left or right of original position
		if (testing.GetSelf().transform.position.x > originalposition.x) {
			//rotate the sprite to face the correct direction if hes on the left
			gameObject.transform.localScale = new Vector2(-testing.pos_scale, testing.GetSelf().transform.localScale.y);
			//left side of the player move left
			gameObject.transform.Translate (Vector3.left * testing.speed * Time.deltaTime);
		} else {
			gameObject.transform.localScale = new Vector2(testing.pos_scale, testing.GetSelf().transform.localScale.y);
			//right side of the player move right
			gameObject.transform.Translate(Vector3.right * testing.speed * Time.deltaTime);
		}
	}
	// Update is called once per frame
	void Update () {
		if (testing.stunned == false && testing.pause == false && testing.currenthealth.health > 0){
			float distance = Vector3.Distance(testing.GetPlayer().transform.position, testing.GetSelf().transform.position);
			if(distance < testing.GetRange()){
				chased = true;
				//player is within the chasing distance
				testing.Movement();
				CalculateBound(testing.GetPlayer().transform.position.x);
				if( testing.GetSelf().transform.position.x > lowerbound &&  testing.GetSelf().transform.position.x < upperbound){
					//attack player
					gameObject.transform.Translate (Vector3.down * testing.speed * Time.deltaTime);
				}
			}else{
				if (chased == true){
					//return to original location
					ReturnPos();
					CalculateBound(gameObject.transform.position.x);
					if( testing.GetSelf().transform.position.x > lowerbound &&  testing.GetSelf().transform.position.x < upperbound){
						//attack player
						gameObject.transform.Translate (Vector3.up * testing.speed * Time.deltaTime);
					}
					//make sure return original height then go back on patrol
					CalculateBound(gameObject.transform.position.y);
					if( testing.GetSelf().transform.position.y > lowerbound &&  testing.GetSelf().transform.position.y < upperbound){
						chased = false;
					}
		
				}else{
					gameObject.transform.position = new Vector3(Mathf.PingPong(Time.time *movementspeed,duration) + originalposition.x,gameObject.transform.localPosition.y,gameObject.transform.localPosition.z);
				}
			}
			//saving current position for pausing and stun
			temppos = gameObject.transform.localPosition;
		}else{
			//keep it from moving
			gameObject.transform.position = temppos;
		}
	}
	public void pausegame(){
		testing.pausegame();
	}
	public void unpausegame(){
		testing.unpausegame();
	}
	void OnReceiveDamage(float dmg)
	{        
		if (!testing.stunned)
		{
			testing.currenthealth.health -= dmg;
			if (testing.currenthealth.health > 0)
			{
				StartCoroutine(Stun());
			}
		}
	}
	
	IEnumerator Stun()
	{
		
		//testing.animator.SetTrigger("Stun");
		testing.stunned = true;
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            sr.material.color = new Vector4(0.5f, 0.0f, 0.0f, 1.0f);
		if (!testing.grounded)
		{
			yield return new WaitForEndOfFrame();
		}
		else
		{
			yield return new WaitForSeconds(0.5f);
		}
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
            sr.material.color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
		testing.stunned = false;
	}
}
[System.Serializable]
public class FlighyFightAI : GeneralAI{
	public FlighyFightAI(){
		range = 100f;
		speed = 10f;
	}
}