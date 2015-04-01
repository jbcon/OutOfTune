using UnityEngine;
using System.Collections;

public class TestTriplets : MonoBehaviour {
	public GameObject player;
	public GameObject projectile;
	public float range;
	TestTripletsAI testing = new TestTripletsAI();
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		testing.SetPos(gameObject.transform.localScale.x);
		testing.CreateBasicStats(gameObject,projectile);
		testing.SetPlayer(player);
	}
	
	// Update is called once per frame
	void Update () {
		testing.Movement();
	}
}

[System.Serializable]
public class TestTripletsAI : GeneralAI{
	public TestTripletsAI(){
		range = 20f;
		
	}
}