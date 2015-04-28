﻿using UnityEngine;
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
		chased = false;
	}
	void Update(){

		if (testing.stunned == false && testing.pause == false){

			float distance = Vector3.Distance(testing.GetPlayer().transform.position, testing.GetSelf().transform.position);
			if (distance < testing.GetRange() && testing.GetHealth() > 0f){
				chased = true;
				animator.SetBool("moving", true);
				testing.Movement();
				chased = false;
			}//if got out of position must return back to original position before going on patrol
			if (chased == true){
				Debug.Log ("not happening");
				lerpposition += Time.deltaTime/lerptime;
				animator.SetBool("moving", true);
				transform.position= Vector3.Lerp(gameObject.transform.position, original_position, lerpposition);
			}else{
				gameObject.transform.position = new Vector3(Mathf.PingPong(Time.time *movementspeed,duration) + original_position.x,gameObject.transform.position.y,gameObject.transform.position.z);
				animator.SetBool("moving", true);
			}
			temp_pos = gameObject.transform.localPosition;
		}else{
			//keep it from moving
			gameObject.transform.position = temp_pos;
		}
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
