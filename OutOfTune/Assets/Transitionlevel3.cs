using UnityEngine;
using System.Collections;

public class Transitionlevel3 : MonoBehaviour {
	public bool cantransition;
	public GameObject statue;
	void Start(){
		cantransition = false;
	}
	void Update(){
		//statue = GameObject.FindGameObjectWithTag("statueparts");
		if(statue.GetComponent<ReneeStatue>().destroyed == true){
			cantransition = true;
		}
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		int playerLayer = LayerMask.NameToLayer("Player");
		
		if (collision.gameObject.layer == playerLayer && cantransition == true)
		{
			GameObject temp = GameObject.FindGameObjectWithTag("Story");
			temp.GetComponent<Story>().delete();
			//Debug.Log("HIT!");
			Application.LoadLevel("Level 3");
		}
	}
}
