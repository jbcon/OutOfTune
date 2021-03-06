﻿using UnityEngine;
using System.Collections;

public class TestTriplets : MonoBehaviour {
	public GameObject player;
	public GameObject projectile;
	private bool moving;
	private float anglebetween;
	Animator animator;
	TestTripletsAI testing = new TestTripletsAI();
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		testing.SetPos(gameObject.transform.localScale.x);
		testing.CreateBasicStats(gameObject,projectile);
		testing.SetPlayer(player);
		moving= false;
		animator = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if (testing.stunned == false && testing.pause == false && (testing.currenthealth.health > 0)){
			//different movement set compared to other ai getes its own
			//testing.Movement();
			float distance = Vector3.Distance(player.transform.position, testing.self.transform.position);
			//Debug.Log(distance+"sdf"+testing.range);
			//moving left off the platform basically
			/*
			//determinging if the object got flipped
			Vector3 tempangle = player.transform.position - gameObject.transform.position;
			anglebetween = Vector3.Angle(gameObject.transform.forward, tempangle);
			//Debug.Log(anglebetween);
			if(anglebetween >= 80){
				moving = false;
				//Debug.Log(anglebetween);
			}*/
			if (distance < testing.range || moving == true) {
				animator.SetBool("Sliding",true);
				/*if (player.GetComponent<PlayerController>().grounded ){*/
					testing.faceright = true;
					gameObject.transform.localScale = new Vector2(testing.pos_scale, testing.self.transform.localScale.y);
					//right side of the player move right
					gameObject.transform.Translate(Vector3.left * testing.speed * Time.deltaTime);
					//continues to move even after player gets out of ranger
					moving = true;
				/*}*/

			}
		}
	}
	public void FireBullet(){
		testing.FireBullet();
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
				Debug.Log("AI Brassi script received damage");
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
public class TestTripletsAI : GeneralAI{
	public TestTripletsAI(){
		range = 200f;
		speed = 80f;
	}
}