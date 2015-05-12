using UnityEngine;
using System.Collections;

public class Transitionlevel4 : MonoBehaviour {
	public bool cantransition;
	void Start(){
		cantransition = true;
	}
	void Update(){
		//statue = GameObject.FindGameObjectWithTag("statueparts");
	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		int playerLayer = LayerMask.NameToLayer("Player");
		
		if (collision.gameObject.layer == playerLayer && cantransition == true)
		{
			GameObject temp = GameObject.FindGameObjectWithTag("Story");
			temp.GetComponent<Story>().delete();
			//Debug.Log("HIT!");
			Application.LoadLevel("Boss Stage");
		}
	}
}
