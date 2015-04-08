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
		float distance = Vector3.Distance(testing.GetPlayer().transform.position, testing.GetSelf().transform.position);
		if (distance < testing.GetRange() && testing.GetHealth() > 0f){
			testing.Movement();
		}
	}
	public void FireBullet(){
		testing.FireBullet();
	}
}
[System.Serializable]
public class BrassiTest : GeneralAI{
	public BrassiTest(){
		range = 20f;

	}
}
