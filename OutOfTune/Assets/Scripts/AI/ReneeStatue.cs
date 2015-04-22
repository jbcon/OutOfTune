using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ReneeStatue : MonoBehaviour {
	public int partnumber;
	private GameObject statue;
	private GameObject[] parts;
	public float health;
	private float healthcounter;
	// Use this for initialization
	void Start () {
		partnumber = 0;
		health = 13;
		statue = this.gameObject;
		parts = new GameObject[13];
		for(int counter = 0; counter < 13 ; counter ++){
			parts[counter] = GameObject.Find("ReneeStatue Sprite sheet_"+counter);
		}
		//parts = GameObject.FindGameObjectsWithTag("statueparts");
		healthcounter = 12;
		//Debug.Log(parts[0]);
		/*
		for (int i = 0; i < parts.Length; i ++){
			Debug.Log(parts[i]);
		}*/
	}


	// Update is called once per frame
	void Update () {
		//Debug.Log(parts[0]);
		if (health == 0){
			Debug.Log ("dead");
		}else if (health <= healthcounter){
			//GameObject temp = GameObject.Find(parts[partnumber]);
			//Debug.Log ("do something");
			/*parts[partnumber].AddComponent<Rigidbody2D>();
			parts[partnumber].GetComponent<Rigidbody2D>().angularDrag = 100;
			parts[partnumber].AddComponent<BoxCollider2D>();
			partnumber ++;
			healthcounter--;
			*/
			StartCoroutine(Losepart(parts[partnumber]));
		}
		//head is gone
		//as the statue gets smaller the smaller the hitbox is on the statue
		if (health == 6){
			this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(65, 60);
		}else if (health == 4){
			this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(65, 40);
			this.gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(-150, -20);
		}else if (health ==2){
			this.gameObject.GetComponent<BoxCollider2D>().size = new Vector2(65, 20);
		}
	
	}
	IEnumerator Losepart(GameObject losingpart)
	{
		//makes sure rapid fire doesn't miss any parts falling apart
		losingpart.AddComponent<Rigidbody2D>();
		losingpart.GetComponent<Rigidbody2D>().angularDrag = 100;
		losingpart.AddComponent<BoxCollider2D>();
		partnumber ++;
		healthcounter--;
		yield return new WaitForSeconds(0.5f);

	}
	public void OnReceiveDamage(float dmg)
	{     
		health -= dmg;
	}
}
