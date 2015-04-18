using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TestBrassi : MonoBehaviour {
	public GameObject projectile;
	public GameObject play;
	public BrassiTest testing = new BrassiTest();
	//ai shoot bullet on arbituary beat

	void Start ()
	{
		play = GameObject.FindGameObjectWithTag ("Player");
		//BrassiTest testing = new BrassiTest();
		testing.SetPos(gameObject.transform.localScale.x);
		testing.CreateBasicStats(gameObject,projectile);
		testing.SetPlayer(play);

	}
	void Update(){
		/*
		float distance = Vector3.Distance(testing.GetPlayer().transform.position, testing.GetSelf().transform.position);
		if (distance < testing.GetRange() && testing.GetHealth() > 0f){
			testing.Movement();
		}*/
		//movement animation for the characters
		float distance = Vector3.Distance(play.transform.position, testing.self.transform.position);
		
		//move only if not hurt
		if (distance < testing.range && !testing.animator.GetBool("Die") && testing.currenthealth.health > 0)
		{
			//animator.SetBool("Walking", true);
			//animator.SetBool("Idle", false);
			testing.Movement();
		}
		else
		{
			//animator.SetBool("Walking", false);
			//animator.SetBool("Idle", true);
			Debug.Log ("idle");
		}
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
				Debug.Log("AI script received damage");
			}
		}
	}
	
	IEnumerator Stun()
	{
		testing.animator.SetTrigger("Stun");
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
	}
}
[System.Serializable]
public class BrassiTest : GeneralAI{
	public BrassiTest(){
		range = 20f;

	}
}
